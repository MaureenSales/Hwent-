using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Draw : MonoBehaviour
{
    public GameObject Card;
    public GameObject Hand;
    public GameObject Deck;
    public GameObject CardPrefab;
    public Deck deck = null;
    public GameObject contentGameData;
    private List<GameObject> CardsInHand { get; set; }

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


    public void DrawCard()
    {
        GameObject drawCard = Instantiate(CardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        int index = Random.Range(0, deck.cards.Count - 1);
        drawCard.GetComponent<ThisCard>().PrintCard(deck.cards[index]);
        deck.cards.Remove(deck.cards[index]);
        drawCard.transform.SetParent(Hand.transform, false);
        CardsInHand.Add(drawCard);


    }

}
