using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public string Nick { get; set; } //nombre del jugador
    //public Sprite Avatar;
    public int CardsInHand = 10; //cantidad de cartas en la mano del jugador
    public bool IsYourTurn = false; //turno del jugador
    public bool Pass = false; //pase del jugador

    //public Image avatar;
    public TextMeshProUGUI countCards; //contador de cartas actuales en la mano
    public TextMeshProUGUI nick;
    public TextMeshProUGUI countCardsInDeck; //contador de cartas actuales en el mazo
    public GameObject Gems; //gemas del jugador
    public GameObject Turn; //indicador de turno del jugador
    public GameObject PlayerBoard; //objeto que conntiene la mano, el botón de pase e intercambio del jugador
    public GameObject WinnerIndicator; //indicador de ganador del jugador
    public GameObject CardPrefab; //Prefab de las cartas
    public GameObject LeaderPlayer; //líder del jugador asignado al objeto Player
    public GameObject LeaderEnemy; //líder del jugador asignado al objeto Enemy
    public bool ChangeAllowed = true; //disponibilidad para intercambio de cartas
    public List<GameObject> cardsToChange = new(); //lista de cartas a intercambiar
    public GameObject buttonChange;
    public GameObject buttonNotChange;
    public GameObject buttonPass;
    public GameObject PanelField; //Panel para no jugar si no has seleccionado si cambiar cartas o no
    // Start is called before the first frame update
    void Start()
    {
        //Asignación de los líderes a cada jugador
        if (this.name.StartsWith("Enemy"))
        {
            Nick = GameData.nameEnemy;
            Debug.Log(Nick);
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

        //Inicializar información del jugador
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

    /// <summary>
    /// Método para pasar turno
    /// </summary>
    public void UpdatePass()
    {
        if(PanelField.gameObject.activeSelf) notChange();
        buttonPass.GetComponent<AudioSource>().Play();
        Pass = true;
        GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn();
    }

    /// <summary>
    /// Método para no intercambiar cartas
    /// </summary>
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

    /// <summary>
    /// Método para intercambiar cartas
    /// </summary>
    public void Change()
    {
        buttonChange.GetComponent<AudioSource>().Play();
        ChangeAllowed = false;

        for (int i = 0; i < cardsToChange.Count; i++)
        {
            GetComponentInParent<Canvas>().GetComponent<GameController>().currentTurn.GetComponent<Player>().MyDeck.AddCard(cardsToChange[i].GetComponent<ThisCard>().thisCard);
            GetComponentInParent<Canvas>().GetComponent<GameController>().currentTurn.GetComponent<Player>().MyHand.CardsObject.Remove(cardsToChange[i]);
            GetComponentInParent<Canvas>().GetComponent<GameController>().currentTurn.GetComponent<Player>().MyHand.Cards.Remove(cardsToChange[i].GetComponent<ThisCard>().thisCard);
            Destroy(cardsToChange[i]);
            GetComponentInParent<Canvas>().GetComponent<GameController>().currentTurn.transform.Find("Deck").GetComponent<Draw>().DrawCard();
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
