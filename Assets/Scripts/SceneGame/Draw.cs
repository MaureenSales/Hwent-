using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Transactions;

public class Draw : MonoBehaviour
{
    public GameObject Hand; //mano del jugador
    public GameObject CardPrefab; //Prefab de las  cartas
    public Deck deck = null; //mazo del jugador
    private List<GameObject> CardsInHand { get; set; } //lista de cartas en la mano

    // Start is called before the first frame update
    async void Start()
    {
        CardsInHand = new();
        Debug.Log(GameData.playerDeck is null);
        if (Hand.transform.parent.name == "PlayerBoard")
        {
            deck = GameData.playerDeck;
        }
        else
        {
            deck = GameData.enemyDeck;
        }

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
        int index = Random.Range(0, deck.cards.Count - 1);
        drawCard.GetComponent<ThisCard>().PrintCard(deck.cards[index]);
        deck.cards.Remove(deck.cards[index]);
        drawCard.transform.SetParent(Hand.transform, false);
        CardsInHand.Add(drawCard);
        GameObject owner = GetComponentInParent<Canvas>().GetComponent<GameController>().currentTurn;
        if (!owner.transform.Find(owner.name + "Info").GetComponent<PlayerController>().ChangeAllowed)
        {
            drawCard.GetComponent<ChangeCards>().enabled = false;
        }

    }

}
