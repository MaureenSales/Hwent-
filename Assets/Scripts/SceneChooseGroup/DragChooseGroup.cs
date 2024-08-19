using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragChooseGroup : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentToReturnTo = null; //Transform del objeto padre al cual retornar la carta arrastrado
    public Vector3 originalScale;
    public bool IsRemove = false; //booleano para saber si la carta fue removida del mazo del jugador
    private AudioSource audioSource;
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentToReturnTo = this.transform.parent;
        originalScale = new Vector3( 1f, 1f, 1f);
        this.transform.SetParent(this.transform.parent.parent);
        this.transform.localScale = new Vector3(1.2f, 1.2f, 0f);

        if (parentToReturnTo.parent.parent.name == "MenuCardsDeck")
        {
            IsRemove = true;
            Debug.Log(parentToReturnTo.GetComponent<DropChooseGroup>().contentShowCards.GetComponent<ShowCards>().newDeck.cards.Count);
            parentToReturnTo.GetComponent<DropChooseGroup>().contentShowCards.GetComponent<ShowCards>().newDeck.RemoveCard(eventData.pointerDrag.GetComponent<ThisCard>().thisCard);
            Debug.Log(parentToReturnTo.GetComponent<DropChooseGroup>().contentShowCards.GetComponent<ShowCards>().newDeck.cards.Count);
        }

        GetComponent<CanvasGroup>().blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
        this.transform.SetParent(transform.root);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.localScale = originalScale;
        this.transform.SetParent(parentToReturnTo);

        if (parentToReturnTo.parent.parent.name == "MenuCardsDeck" && IsRemove)
        {
            parentToReturnTo.GetComponent<DropChooseGroup>().contentShowCards.GetComponent<ShowCards>().newDeck.AddCard(eventData.pointerDrag.GetComponent<ThisCard>().thisCard);
            IsRemove = false;
        }
        audioSource.Play();
        GetComponent<CanvasGroup>().blocksRaycasts = true;

    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

}
