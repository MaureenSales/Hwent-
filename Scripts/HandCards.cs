using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCards : MonoBehaviour
{
    public List<GameObject> CardsInHandObject = new();
    public GameObject Hand;
    public List<Card> CardsInHand = new();

    public void AddCard(GameObject card)
    {
        // CardsInHandObject.Add(card);
        // if (card.name.StartsWith("Unit"))
        // {
        //     CardsInHand.Add(card.GetComponent<ThisUnit>().unit);
        // }
        // else if (card.name.StartsWith("Hero"))
        // {
        //     CardsInHand.Add(card.GetComponent<ThisHero>().hero);
        // }
        // else if (card.name.StartsWith("Weather"))
        // {
        //     CardsInHand.Add(card.GetComponent<ThisSpecial>().special);
        // }

    }

    public void RemoveCard(GameObject card)
    {
        // CardsInHandObject.Remove(card);
        // Debug.Log("Entroooo");
        // Debug.Log(CardsInHand.Count);
        // if (card.name.StartsWith("Unit"))
        // {
        //     CardsInHand.Remove(card.GetComponent<ThisUnit>().unit);
        // }
        // else if (card.name.StartsWith("Hero"))
        // {
        //     CardsInHand.Remove(card.GetComponent<ThisHero>().hero);
        // }
        // else if (card.name.StartsWith("Weather"))
        // {
        //     CardsInHand.Remove(card.GetComponent<ThisSpecial>().special);
        // }
        // else if(card.name.StartsWith("Boost"))
        // {
        //     CardsInHand.Remove(card.GetComponent<ThisSpecial>().special);
        // }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
