using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseDecksController : MonoBehaviour
{
    public GameObject contentGameData;
    public GameObject contentShowCards;
    public TextMeshProUGUI cardsInDeck;
    public GameObject buttonNext;
    // Start is called before the first frame update
    void Start()
    {
        buttonNext.GetComponent<Button>().interactable = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (contentShowCards.GetComponent<ShowCards>().newDeck != null)
        {

            if (contentShowCards.GetComponent<ShowCards>().newDeck.cards.Count >= 25)
            {
                buttonNext.GetComponent<Button>().interactable = true;
            }
            else
            {
                buttonNext.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void Next()
    {
        if(GameData.playerDeck != null)
        {
            GameData.enemyDeck = contentShowCards.GetComponent<ShowCards>().newDeck;
        }
        else
        {
            GameData.playerDeck = contentShowCards.GetComponent<ShowCards>().newDeck;
        }
        
        Debug.Log(GameData.playerDeck is null);
        Debug.Log(GameData.playerDeck.cards.Count);
        if (GameData.playerDeck != null && GameData.enemyDeck != null)
        {
            SceneManager.LoadScene("GameInBoard");

        }
        else
        {
            SceneManager.LoadScene("ChooseGroup");
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
