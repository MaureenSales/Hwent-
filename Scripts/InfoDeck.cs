using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoDeck : MonoBehaviour
{
    public TextMeshProUGUI countCardsInDeck;
    public TextMeshProUGUI countUnitCards;
    public TextMeshProUGUI countSpecialsCards;
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

    }
}
