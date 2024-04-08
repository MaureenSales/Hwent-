using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public string Nick{ get; set; }
    //public Sprite Avatar;
    public int CardsInHand = 10;
    public bool IsYourTurn = false;

    //public Image avatar;
    public TextMeshProUGUI countCards;
    public TextMeshProUGUI nick;
    public GameObject Gems;
    public GameObject Turn;
    public GameObject PlayerBoard;
    public GameObject WinnerIndicator;
    // Start is called before the first frame update
    void Start()
    {
        
        if(this.name.StartsWith("Enemy"))
        {
            Nick = "Enemy";
        }
        else
        {
            Nick = "Player";
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
        if(!IsYourTurn)
        {
            Turn.SetActive(false);
            PlayerBoard.SetActive(false);
        }
        else
        {
            Turn.SetActive(true);
            PlayerBoard.SetActive(true);
        }

        if(this.name == "PlayerInfo")
        {
            if(GameObject.Find("PlayerBoard") != null)
            {
            CardsInHand = GameObject.Find("PlayerBoard").GetComponentInChildren<HandCards>().CardsInHandObject.Count;
            countCards.text = CardsInHand.ToString();

            }
        }
        else if(this.name == "EnemyInfo")
        {
            if(GameObject.Find("EnemyBoard") != null)
            {
            CardsInHand = GameObject.Find("EnemyBoard").GetComponentInChildren<HandCards>().CardsInHandObject.Count;
            countCards.text = CardsInHand.ToString();

            }
        }
        
    }
}
