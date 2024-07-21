using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    public string Id {get; private set;}
    public Hand MyHand {get; private set;}
    public Deck MyDeck {get; private set;}
    public Graveyard MyGraveyard {get; private set;}
    public Field MyField {get; private set;}
    public GameObject Deck;
    // Start is called before the first frame update
    void Start()
    {
        Draw draw = Deck.GetComponent<Draw>();
        MyDeck = draw.deck;
        MyHand = draw.hand;
        MyGraveyard = this.GetComponentInChildren<Graveyard>();
        MyField = this.GetComponentInChildren<Field>();
        if(this.name == "Player") Id = GameData.namePlayer;
        else Id = GameData.nameEnemy;
        AssingOwner(Id, MyDeck.cards);
        MyDeck.Leader.AssignOwner(Id);
    }

    private void AssingOwner(string owner, List<Card> cards)
    {
        foreach (Card card in cards) card.AssignOwner(owner);
    }

    
}
