using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfoChooseCards : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject CardInfo = null; //carta a mostrar información
    public Vector3 originalScale; 

    public void OnPointerEnter(PointerEventData eventData)
    {
        originalScale = this.transform.localScale;
        this.transform.localScale = new Vector3(originalScale.x + 0.1f, originalScale.y + 0.1f, 0f);
        if (GameObject.Find("Info") != null)
        {
            CardInfo = Instantiate(this.gameObject, GameObject.Find("Info").transform);
            CardInfo.transform.position = GameObject.Find("Info").transform.position;
            CardInfo.transform.localScale = new Vector3(3.6f, 3.6f, 3.6f);
        }


    }
    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale = originalScale;
        if (CardInfo != null) Destroy(CardInfo);
    }

}
