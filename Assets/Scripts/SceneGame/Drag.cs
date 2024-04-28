using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Back;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentToReturnTo = null; //Transform del objeto padre al que devolver la carta arrastrada
    public Transform placeHolderParent = null; //Transform del objeto temporal para mover las cartas al organizarlas
    public Vector3 originalScale;
    GameObject placeHolder = null;
    private AudioSource audioSource;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

        placeHolder = new GameObject();
        placeHolder.transform.SetParent(this.transform.parent);

        LayoutElement layout = placeHolder.AddComponent<LayoutElement>();
        layout.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        layout.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        layout.flexibleHeight = 0;
        layout.flexibleWidth = 0;

        placeHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        parentToReturnTo = this.transform.parent;
        placeHolderParent = parentToReturnTo;
        originalScale = this.transform.localScale;
        this.transform.SetParent(this.transform.parent.parent);
        this.transform.localScale = new Vector3(1.2f, 1.2f, 0f);

        GetComponent<CanvasGroup>().blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");

        this.transform.position = eventData.position;
        if (placeHolder.transform.parent != placeHolderParent)
        { 
            placeHolder.transform.SetParent(placeHolderParent); 
        }

        int newSiblingIndex = placeHolderParent.childCount;
        
        for (int i = 0; i < placeHolderParent.childCount; i++)
        {
            if (this.transform.position.x < placeHolderParent.GetChild(i).position.x)
            {
                newSiblingIndex = i;
                if (placeHolder.transform.GetSiblingIndex() < newSiblingIndex)
                {
                    newSiblingIndex--;
                }
                break;
            }
        }

        placeHolder.transform.SetSiblingIndex(newSiblingIndex);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        this.transform.localScale = originalScale;
        this.transform.SetParent(parentToReturnTo); 
        this.transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(placeHolder);
        audioSource.Play();

    }

    private void Start() 
    {
        audioSource = this.GetComponent<AudioSource>();
    }

}
