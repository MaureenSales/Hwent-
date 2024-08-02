using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    public Leader Leader { get; set; } //líder de la facción del mazo
    public List<Card> cards = new List<Card>(); //mazo de cartas

/// <summary>
/// Constructor de un Mazo de Cartas
/// </summary>
/// <param name="leader">líder de la facción</param>
    public Deck(Leader leader)
    {
        Leader = leader;
    }

/// <summary>
/// Añade una carta al mazo según las reglas
/// </summary>
/// <param name="card">carta a añadir</param>
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

/// <summary>
///Contar las copias de una carta en el mazo
/// </summary>
/// <param name="card">carta cuya cantidad de copias se solicita</param>
/// <returns>cantidad de copias actuales de la carta en el mazo</returns>
    public int countCopies(Card card)
    {
        int copies = 0;
        foreach (var item in cards)
        {
            if(card.Equals(item)) copies++;
        }
        return copies;
    }

/// <summary>
/// Eliminar una carta del mazo
/// </summary>
/// <param name="card">carta a eliminar</param>
    public void RemoveCard(Card card)
    {
        cards.Remove(card);
    }

}
