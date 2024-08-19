using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Transactions;
public class Draw : MonoBehaviour
{
    public GameObject Hand; //mano del jugador
    public GameObject CardPrefab; //Prefab de las  cartas
    public Deck deck; //mazo del jugador
    public Hand hand;
    // Start is called before the first frame update
    async void Start()
    {
        if (Hand.transform.parent.name == "PlayerBoard")
        {
            deck = GameData.playerDeck;
        }
        else
        {
            deck = GameData.enemyDeck;
        }
        hand = Hand.GetComponent<Hand>();
        for (int i = 0; i < 10; i++)
        {
            DrawCard();
            await Task.Delay(400);
        }
    }

    /// <summary>
    /// Método para robar una carta
    /// </summary>
    public void DrawCard()
    {
        GameObject drawCard = Instantiate(CardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        int index = UnityEngine.Random.Range(0, deck.cards.Count - 1);
        drawCard.GetComponent<ThisCard>().PrintCard(deck.cards[index]);
        deck.cards.Remove(deck.cards[index]);
        drawCard.transform.SetParent(Hand.transform, false);
        hand = Hand.GetComponent<Hand>();
        hand.CardsObject.Add(drawCard);
        hand.Cards.Add(drawCard.GetComponent<ThisCard>().thisCard);
        GameObject owner = GetComponentInParent<Canvas>().GetComponent<GameController>().currentTurn;
        if (!owner.transform.Find(owner.name + "Info").GetComponent<PlayerController>().ChangeAllowed)
        {
            drawCard.GetComponent<ChangeCards>().enabled = false;
        }

    }

}
