using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class ChooseDecksController : MonoBehaviour
{
    public GameObject contentGameData; //GameObject que contiene como componente el script GameData
    public GameObject contentShowCards; //GameObject que contiene como componente el script ShowCards
    public GameObject buttonNext;
    public GameObject buttonBack;
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

/// <summary>
/// Método para actualizar los datos del jugador y pasar a la escena requerida
/// </summary>
    public void Next()
    {
        buttonNext.GetComponent<AudioSource>().Play();
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
            SceneManager.LoadScene("Login");
        }
    }

/// <summary>
/// Método para regresar al Menú Principal
/// </summary>
    async public void Back()
    {
        if(GameData.nameEnemy != "") GameData.nameEnemy = "";
        else GameData.namePlayer = "";
        buttonBack.GetComponent<AudioSource>().Play();
        await Task.Delay(80);
        SceneManager.LoadScene("MainMenu");
    }

}
