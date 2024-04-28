using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoDeck : MonoBehaviour
{
    public TextMeshProUGUI countCardsInDeck; //cantidad de cartas actualmente en el mazo del jugador
    public TextMeshProUGUI countUnitCards; //cantidad de unidades actualmente en el mazo del jugador
    public TextMeshProUGUI countSpecialsCards; //cantidad de cartas especiales en el mazo del jugador
    public GameObject contentShowCards; //GameObject que contiene como componente el script ShowCards
    
    // Start is called before the first frame update
    void Start()
    {
        countCardsInDeck.text = "0";
        countUnitCards.text = "0";
        countSpecialsCards.text = "0";
        
    }

    // Update is called once per frame
    void Update()
    {
        if (contentShowCards.GetComponent<ShowCards>().newDeck != null)
        {
            countCardsInDeck.text = contentShowCards.GetComponent<ShowCards>().newDeck.cards.Count.ToString();
            int units = 0;
            for (int i = 0; i < contentShowCards.GetComponent<ShowCards>().newDeck.cards.Count; i++)
            {
                if (contentShowCards.GetComponent<ShowCards>().newDeck.cards[i] is UnitCard)
                {
                    units++;
                }
            }
            countUnitCards.text = units.ToString();
            int specials = 0;
            for (int i = 0; i < contentShowCards.GetComponent<ShowCards>().newDeck.cards.Count; i++)
            {
                if (contentShowCards.GetComponent<ShowCards>().newDeck.cards[i] is SpecialCard)
                {
                    specials++;
                }
            }
            countSpecialsCards.text = specials.ToString();
        }
    }
}
