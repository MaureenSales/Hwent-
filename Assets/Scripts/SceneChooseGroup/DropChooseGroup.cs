using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropChooseGroup : MonoBehaviour, IDropHandler
{
    public GameObject CardPrefab; //Prefab de las cartas
    public TextMeshProUGUI countCopies = null; //copias de las cartas en el mazo en creación
    public GameObject contentShowCards; //GameObject que contiene como componente el script ShowCards
    public GameObject DeckInfo; //GameObject que contiene los objetos que muestran la información del mazo del jugador

    public void OnDrop(PointerEventData eventData)
    {
        DragChooseGroup card = eventData.pointerDrag.GetComponent<DragChooseGroup>();

        if (card != null)
        {
            if (this.transform.childCount == 0 && this.transform.parent.name == "Grid" && (card.parentToReturnTo.parent != this.transform.parent))
            {
                if (contentShowCards.GetComponent<ShowCards>().newDeck.countCopies(eventData.pointerDrag.GetComponent<ThisCard>().thisCard) >= 1 && !(eventData.pointerDrag.GetComponent<ThisCard>().thisCard is HeroUnit))
                {
                    contentShowCards.GetComponent<ShowCards>().newDeck.AddCard((Card)eventData.pointerDrag.GetComponent<ThisCard>().thisCard.Clone());
                    card.originalScale = new Vector3( 1f, 1f, 1f);
                }
                else if (!contentShowCards.GetComponent<ShowCards>().newDeck.cards.Contains(eventData.pointerDrag.GetComponent<ThisCard>().thisCard))
                {
                    GameObject newCard = Instantiate(CardPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                    newCard.GetComponent<ThisCard>().PrintCard(eventData.pointerDrag.GetComponent<ThisCard>().thisCard);
                    newCard.transform.SetParent(card.parentToReturnTo);
                    newCard.transform.localScale = new Vector3(1f, 1f, 1f);
                    newCard.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    card.parentToReturnTo = this.transform;
                    card.originalScale = new Vector3(1f, 1f, 1f);
                    contentShowCards.GetComponent<ShowCards>().newDeck.AddCard((Card)eventData.pointerDrag.GetComponent<ThisCard>().thisCard.Clone());
                }
            }
            else if (this.name == "Trash")
            {
                if (card.IsRemove)
                {
                    if (eventData.pointerDrag.GetComponent<ThisCard>().thisCard is HeroUnit || (contentShowCards.GetComponent<ShowCards>().newDeck.countCopies(eventData.pointerDrag.GetComponent<ThisCard>().thisCard) == 0))
                    {
                        Destroy(eventData.pointerDrag.gameObject);
                        GameObject.Find("Trash").GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        contentShowCards.GetComponent<ShowCards>().newDeck.RemoveCard(eventData.pointerDrag.GetComponent<ThisCard>().thisCard);
                        GameObject.Find("Trash").GetComponent<AudioSource>().Play();
                    }

                }
                card.originalScale = new Vector3( 1f, 1f, 1f);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (countCopies != null)
        {
            if (this.transform.childCount > 0)
            {
                countCopies.text = contentShowCards.GetComponent<ShowCards>().newDeck.countCopies(this.transform.GetChild(0).GetComponent<ThisCard>().thisCard).ToString();

            }
            else
            {
                countCopies.text = "0";
            }
        }

    }
}
