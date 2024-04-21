using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public string Nick { get; set; }
    //public Sprite Avatar;
    public int CardsInHand = 10;
    public bool IsYourTurn = false;
    public bool Pass = false;

    //public Image avatar;
    public TextMeshProUGUI countCards;
    public TextMeshProUGUI nick;
    public TextMeshProUGUI countCardsInDeck;
    public GameObject Gems;
    public GameObject Turn;
    public GameObject PlayerBoard;
    public GameObject WinnerIndicator;
    public GameObject CardPrefab;
    public GameObject LeaderPlayer;
    public GameObject LeaderEnemy;
    public bool ChangeAllowed = true;
    public List<GameObject> cardsToChange = new();
    public GameObject buttonChange;
    public GameObject buttonNotChange;
    public GameObject buttonPass;
    public GameObject PanelField;
    // Start is called before the first frame update
    void Start()
    {

        if (this.name.StartsWith("Enemy"))
        {
            Nick = GameData.nameEnemy;
            GameObject leaderEnemy = Instantiate(CardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            leaderEnemy.GetComponent<ThisCard>().PrintCard(GameData.enemyDeck.Leader);
            leaderEnemy.transform.SetParent(LeaderEnemy.transform);
            leaderEnemy.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            leaderEnemy.GetComponent<Drag>().enabled = false;
        }
        else
        {
            Nick = GameData.namePlayer;
            GameObject leaderPlayer = Instantiate(CardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            leaderPlayer.GetComponent<ThisCard>().PrintCard(GameData.playerDeck.Leader);
            leaderPlayer.transform.SetParent(LeaderPlayer.transform);
            leaderPlayer.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            leaderPlayer.GetComponent<Drag>().enabled = false;

        }

        countCards.text = CardsInHand.ToString();
        //avatar.sprite = Avatar;
        nick.text = Nick;
        Gems.transform.GetChild(0).gameObject.SetActive(false);
        Gems.transform.GetChild(1).gameObject.SetActive(false);
        WinnerIndicator.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsYourTurn)
        {
            Turn.SetActive(false);
            PlayerBoard.SetActive(false);
        }
        else
        {
            Turn.SetActive(true);
            PlayerBoard.SetActive(true);
        }

        if (this.name == "PlayerInfo")
        {
            if (GameObject.Find("PlayerBoard") != null)
            {
                CardsInHand = GameObject.Find("PlayerBoard").transform.Find("Hand").childCount;
                countCards.text = CardsInHand.ToString();
            }

            countCardsInDeck.text = GameData.playerDeck.cards.Count.ToString();
        }
        else if (this.name == "EnemyInfo")
        {
            if (GameObject.Find("EnemyBoard") != null)
            {
                CardsInHand = GameObject.Find("EnemyBoard").transform.Find("Hand").childCount;
                countCards.text = CardsInHand.ToString();

            }

            countCardsInDeck.text = GameData.enemyDeck.cards.Count.ToString();
        }



    }

    public void UpdatePass()
    {
        buttonPass.GetComponent<AudioSource>().Play();
        Pass = true;
        GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn();
    }

    public void notChange()
    {
        buttonNotChange.GetComponent<AudioSource>().Play();
        PanelField.SetActive(false);
        buttonChange.SetActive(false);
        buttonNotChange.SetActive(false);
        GameObject player = GetComponentInParent<Canvas>().GetComponent<GameController>().currentTurn;
        GameObject hand = player.transform.Find(player.name + "Board").Find("Hand").gameObject;

        for (int i = 0; i < hand.transform.childCount; i++)
        {
            hand.transform.GetChild(i).GetComponent<ChangeCards>().enabled = false;
        }

        ChangeAllowed = false;

    }

    public void Change()
    {
        buttonChange.GetComponent<AudioSource>().Play();
        ChangeAllowed = false;

        if (GetComponentInParent<Canvas>().GetComponent<GameController>().currentTurn.name == "Player")
        {
            for (int i = 0; i < cardsToChange.Count; i++)
            {
                GameData.playerDeck.AddCard(cardsToChange[i].GetComponent<ThisCard>().thisCard);
                Destroy(cardsToChange[i]);
                GetComponentInParent<Canvas>().GetComponent<GameController>().currentTurn.transform.Find("Deck").GetComponent<Draw>().DrawCard();
            }

        }
        else
        {
            for (int i = 0; i < cardsToChange.Count; i++)
            {
                GameData.enemyDeck.AddCard(cardsToChange[i].GetComponent<ThisCard>().thisCard);
                Destroy(cardsToChange[i]);
                GetComponentInParent<Canvas>().GetComponent<GameController>().currentTurn.transform.Find("Deck").GetComponent<Draw>().DrawCard();
            }
        }

        PanelField.SetActive(false);
        buttonChange.SetActive(false);
        buttonNotChange.SetActive(false);
        GameObject player = GetComponentInParent<Canvas>().GetComponent<GameController>().currentTurn;
        GameObject hand = player.transform.Find(player.name + "Board").Find("Hand").gameObject;

        for (int i = 0; i < hand.transform.childCount; i++)
        {
            hand.transform.GetChild(i).GetComponent<ChangeCards>().enabled = false;
        }
    }

}
