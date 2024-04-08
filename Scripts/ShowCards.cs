using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowCards : MonoBehaviour
{
    public GameObject CardPrefab;
    public TextMeshProUGUI typeCardCollection;
    public TextMeshProUGUI typeCardDeck;
    public bool FactionGryffindor;
    public bool FactionSlytherin;
    public Deck availableDeckCollection = null;
    public GameObject GridCollection;
    public GameObject GridDeck;
    public GameObject Leader;
    public Deck newDeck = null;
    public void ShowTypeCollection()
    {
        Button buttonEvent = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        ClearGridCollection();

        if (FactionGryffindor)
        {
            availableDeckCollection = GetComponentInParent<Canvas>().GetComponent<CardDataBase>().Gryffindor;
        }
        else if (FactionSlytherin)
        {
            availableDeckCollection = GetComponentInParent<Canvas>().GetComponent<CardDataBase>().Slytherin;
        }


        if (availableDeckCollection != null)
        {
            List<Card> toShow = new List<Card>();
            if (buttonEvent.name == "Melee")
            {
                typeCardCollection.text = "Cuerpo a cuerpo";
                toShow = Filter("Melee", availableDeckCollection);
            }
            else if (buttonEvent.name == "Ranged")
            {
                typeCardCollection.text = "Ataque a distancia";
                toShow = Filter("Ranged", availableDeckCollection);
            }
            else if (buttonEvent.name == "Siege")
            {
                typeCardCollection.text = "Asedio";
                toShow = Filter("Siege", availableDeckCollection);
            }
            else if (buttonEvent.name == "Decoy")
            {
                typeCardCollection.text = "Señuelo";
                toShow = Filter("Decoy", availableDeckCollection);
            }
            else if (buttonEvent.name == "Boost")
            {
                typeCardCollection.text = "Aumento";
                toShow = Filter("Boost", availableDeckCollection);
            }
            else if (buttonEvent.name == "Weather")
            {
                typeCardCollection.text = "Clima";
                toShow = Filter("Weather", availableDeckCollection);
            }

            for (int i = 0; i < toShow.Count; i++)
            {
                GameObject newCard = Instantiate(CardPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                newCard.GetComponent<ThisCard>().PrintCard(toShow[i]);
                newCard.transform.SetParent(GridCollection.transform.GetChild(i).transform);
                newCard.transform.position = GridCollection.transform.GetChild(i).position;
                newCard.transform.localScale = new Vector3(1f, 1f, 1f);
            }

        }

    }

    public void ShowCardsDeck()
    {
        Button buttonEvent = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        List<Card> toShow = new List<Card>();
        ClearGridDeck();
        if(newDeck != null)
        {

        if (buttonEvent.name == "Melee")
        {
            typeCardDeck.text = "Cuerpo a cuerpo";
            toShow = Filter("Melee", newDeck);
        }
        else if (buttonEvent.name == "Ranged")
        {
            typeCardDeck.text = "Ataque a distancia";
            toShow = Filter("Ranged", newDeck);
        }
        else if (buttonEvent.name == "Siege")
        {
            typeCardDeck.text = "Asedio";
            toShow = Filter("Siege", newDeck);
        }
        else if (buttonEvent.name == "Decoy")
        {
            typeCardDeck.text = "Señuelo";
            toShow = Filter("Decoy", newDeck);
        }
        else if (buttonEvent.name == "Boost")
        {
            typeCardDeck.text = "Aumento";
            toShow = Filter("Boost", newDeck);
        }
        else if (buttonEvent.name == "Weather")
        {
            typeCardDeck.text = "Clima";
            toShow = Filter("Weather", newDeck);
        }
        else if (buttonEvent.name == "All")
        {
            typeCardDeck.text = "Todas las cartas";
            toShow = newDeck.cards;
        }
        toShow = withoutRepettition(toShow);
        for (int i = 0; i < toShow.Count; i++)
        {
            GameObject newCard = Instantiate(CardPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            newCard.GetComponent<ThisCard>().PrintCard(toShow[i]);
            newCard.transform.SetParent(GridDeck.transform.GetChild(i).transform);
            newCard.transform.position = GridDeck.transform.GetChild(i).position;
            newCard.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        }

    }

    public void ClearGridCollection()
    {
        GridLayoutGroup gridLayout = GridCollection.GetComponent<GridLayoutGroup>();

        foreach (Transform child in gridLayout.transform)
        {
            if (child.childCount > 0)
            {
                Destroy(child.GetChild(0).gameObject);
            }
        }
    }

    public void ClearLeader()
    {
        GridLayoutGroup gridLayout = Leader.GetComponent<GridLayoutGroup>();
        int childCount = gridLayout.transform.childCount;
        if (childCount > 0)
        {
            Destroy(gridLayout.transform.GetChild(0).gameObject);
        }

    }

    public void ClearGridDeck()
    {
        GridLayoutGroup gridLayout = GridDeck.GetComponent<GridLayoutGroup>();

        foreach (Transform child in gridLayout.transform)
        {
            if (child.childCount > 0)
            {
                Destroy(child.GetChild(0).gameObject);
            }
        }
    }

    private List<Card> Filter(string type, Deck deck)
    {
        List<Card> toShow = new List<Card>();
        foreach (var card in deck.cards)
        {
            if (type == "Melee")
            {
                if (card is UnitCard)
                {
                    var unit = (UnitCard)card;
                    if (unit.AttackTypes.Contains(Global.AttackModes.Melee))
                    {
                        toShow.Add(unit);
                    }
                }
            }
            if (type == "Ranged")
            {
                if (card is UnitCard)
                {
                    var unit = (UnitCard)card;
                    if (unit.AttackTypes.Contains(Global.AttackModes.Ranged))
                    {
                        toShow.Add(unit);
                    }
                }
            }
            if (type == "Siege")
            {
                if (card is UnitCard)
                {
                    var unit = (UnitCard)card;
                    if (unit.AttackTypes.Contains(Global.AttackModes.Siege))
                    {
                        toShow.Add(unit);
                    }
                }
            }
            if (type == "Decoy")
            {
                if (card is DecoyUnit)
                {
                    toShow.Add(card);
                }
            }
            if (type == "Boost")
            {
                if (card is Boost)
                {
                    toShow.Add(card);
                }
            }
            if (type == "Weather")
            {
                if (card is Weather || card is Clear)
                {
                    toShow.Add(card);
                }
            }
        }
        return toShow;
    }

    private List<Card> withoutRepettition (List<Card> cards)
    {
        List<Card> newList  = new List<Card>();
        foreach (var card in cards)
        {
            if(!newList.Contains(card))
            {
                newList.Add(card);
            }
        }
        return newList;
    }

    public void UpdateFaction()
    {
        Button buttonEvent = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        if (buttonEvent.name == "Gryffindor")
        {
            FactionGryffindor = true;
            FactionSlytherin = false;
            ClearGridDeck();
            ClearLeader();
            GameObject newCard = Instantiate(CardPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            newCard.GetComponent<ThisCard>().PrintCard(GetComponentInParent<Canvas>().GetComponent<CardDataBase>().Leaders[Global.Factions.Gryffindor]);
            newCard.transform.SetParent(Leader.transform);
            newCard.transform.localScale = new Vector3(2f, 2f, 0f);
            newDeck = new Deck(GetComponentInParent<Canvas>().GetComponent<CardDataBase>().Leaders[Global.Factions.Gryffindor]);

        }
        else if (buttonEvent.name == "Slytherin")
        {
            FactionGryffindor = false;
            FactionSlytherin = true;
            ClearGridDeck();
            ClearLeader();
            GameObject newCard = Instantiate(CardPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            newCard.GetComponent<ThisCard>().PrintCard(GetComponentInParent<Canvas>().GetComponent<CardDataBase>().Leaders[Global.Factions.Slytherin]);
            newCard.transform.SetParent(Leader.transform);
            newCard.transform.localScale = new Vector3(2f, 2f, 0f);
            newDeck = new Deck(GetComponentInParent<Canvas>().GetComponent<CardDataBase>().Leaders[Global.Factions.Slytherin]);
        }

    }

}
