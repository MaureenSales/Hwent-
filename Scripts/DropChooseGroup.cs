using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropChooseGroup : MonoBehaviour, IDropHandler
{
    public GameObject CardPrefab;
    public TextMeshProUGUI countCopies = null;
    public GameObject contentShowCards;
    public GameObject DeckInfo;

    public void OnDrop(PointerEventData eventData)
    {
        DragChooseGroup card = eventData.pointerDrag.GetComponent<DragChooseGroup>();

        if (card != null)
        {
            if (this.transform.childCount == 0 && this.transform.parent.name == "Grid" && (card.parentToReturnTo.parent != this.transform.parent))
            {
                if (contentShowCards.GetComponent<ShowCards>().newDeck.countCopies(eventData.pointerDrag.GetComponent<ThisCard>().thisCard) >= 1 && !(eventData.pointerDrag.GetComponent<ThisCard>().thisCard is HeroUnit))
                {
                    contentShowCards.GetComponent<ShowCards>().newDeck.AddCard(eventData.pointerDrag.GetComponent<ThisCard>().thisCard);
                    Debug.Log(contentShowCards.GetComponent<ShowCards>().newDeck.cards.Count);
                }
                else if (!contentShowCards.GetComponent<ShowCards>().newDeck.cards.Contains(eventData.pointerDrag.GetComponent<ThisCard>().thisCard))
                {
                    GameObject newCard = Instantiate(CardPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                    newCard.GetComponent<ThisCard>().PrintCard(eventData.pointerDrag.GetComponent<ThisCard>().thisCard);
                    newCard.transform.SetParent(card.parentToReturnTo);
                    newCard.transform.localScale = new Vector3(1f, 1f, 1f);
                    newCard.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    card.parentToReturnTo = this.transform;
                    contentShowCards.GetComponent<ShowCards>().newDeck.AddCard(eventData.pointerDrag.GetComponent<ThisCard>().thisCard);
                    Debug.Log(contentShowCards.GetComponent<ShowCards>().newDeck.cards.Count);
                }
            }
            else if (this.name == "Trash")
            {
                Debug.Log(eventData.pointerDrag.GetComponent<ThisCard>().cardName);
                Debug.Log(card.IsRemove);
                Debug.Log(contentShowCards.GetComponent<ShowCards>().newDeck.countCopies(eventData.pointerDrag.GetComponent<ThisCard>().thisCard));
                if (card.IsRemove)
                {
                    if (eventData.pointerDrag.GetComponent<ThisCard>().thisCard is HeroUnit || (contentShowCards.GetComponent<ShowCards>().newDeck.countCopies(eventData.pointerDrag.GetComponent<ThisCard>().thisCard) == 0))
                    {
                        Destroy(eventData.pointerDrag.gameObject);
                        GameObject.Find("Trash").GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        Debug.Log(contentShowCards.GetComponent<ShowCards>().newDeck.cards.Count);
                        contentShowCards.GetComponent<ShowCards>().newDeck.RemoveCard(eventData.pointerDrag.GetComponent<ThisCard>().thisCard);
                        Debug.Log(contentShowCards.GetComponent<ShowCards>().newDeck.cards.Count);
                    }

                }
                Debug.Log(contentShowCards.GetComponent<ShowCards>().newDeck.cards.Count);
            }
            Debug.Log(contentShowCards.GetComponent<ShowCards>().newDeck.cards.Count);

        }
    }

    // Start is called before the first frame update
    void Start()
    {

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


        if (contentShowCards.GetComponent<ShowCards>().newDeck != null)
        {
            DeckInfo.GetComponent<InfoDeck>().countCardsInDeck.text = contentShowCards.GetComponent<ShowCards>().newDeck.cards.Count.ToString();
            int units = 0;
            for (int i = 0; i < contentShowCards.GetComponent<ShowCards>().newDeck.cards.Count; i++)
            {
                if (contentShowCards.GetComponent<ShowCards>().newDeck.cards[i] is UnitCard)
                {
                    units++;
                }
            }
            DeckInfo.GetComponent<InfoDeck>().countUnitCards.text = units.ToString();
            int specials = 0;
            for (int i = 0; i < contentShowCards.GetComponent<ShowCards>().newDeck.cards.Count; i++)
            {
                if (contentShowCards.GetComponent<ShowCards>().newDeck.cards[i] is SpecialCard)
                {
                    specials++;
                }
            }
            DeckInfo.GetComponent<InfoDeck>().countSpecialsCards.text = specials.ToString();
        }

    }
}
