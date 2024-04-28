using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NickInput : MonoBehaviour
{
    public string PlayerName = ""; //nombre del jugador actual
    public TMP_InputField tMP_InputField; //GameObject para la entrada del nombre
    public GameObject buttonLogin;
    public AudioClip keySound;
    private string lastText = ""; //texto actual de la entrada

    /// <summary>
    /// Método para guardar el nombre del jugador y continuar a la siguiente escena
    /// </summary>
    public void PlayerNameInput()
    {
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<AudioSource>().Play();
        PlayerName = tMP_InputField.text;
        

        if (GameData.namePlayer != "")
        {
            GameData.nameEnemy = PlayerName;
        }
        else
        {
            GameData.namePlayer = PlayerName;
        }

        Debug.Log(GameData.namePlayer);
        SceneManager.LoadScene("ChooseGroup");

    }
    // Start is called before the first frame update
    void Start()
    {
        buttonLogin.GetComponent<Button>().interactable = false;
        tMP_InputField.placeholder.GetComponent<TMP_Text>().text = "Introduce tu nombre";
    }

    // Update is called once per frame
    void Update()
    {
        if(tMP_InputField.text != "")
        {
            buttonLogin.GetComponent<Button>().interactable = true;
        }
        else
        {
            buttonLogin.GetComponent<Button>().interactable = false;
        }

        if(tMP_InputField.text != lastText)
        {
            tMP_InputField.GetComponent<AudioSource>().PlayOneShot(keySound);

            lastText = tMP_InputField.text;
        }
    }
}
