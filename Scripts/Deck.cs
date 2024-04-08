using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    public Leader Leader { get; private set; }
    public List<Card> cards = new List<Card>();

    public Deck(Leader leader)
    {
        Leader = leader;
    }

    public void AddCard(Card card)
    {
        if (card.Faction == Leader.Faction || card.Faction == Global.Factions.Neutral)
        {
            if (cards.Contains(card))
            {
                if (!(card is HeroUnit))
                {
                    int copies = countCopies(card);
                    
                    if (copies < 3)
                    {
                        cards.Add(card);
                    }
                }

            }
            else
            {
                cards.Add(card);
            }
        }

    }

    public int countCopies(Card card)
    {
        int copies = 0;
        foreach (var item in cards)
        {
            if(item == card) copies++;
        }
        return copies;
    }

    public void RemoveCard(Card card)
    {
        cards.Remove(card);
    }

}
