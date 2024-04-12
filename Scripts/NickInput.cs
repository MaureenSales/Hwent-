using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NickInput : MonoBehaviour
{
    public string PlayerName = "";
    public TMP_InputField tMP_InputField;
    public GameObject buttonLogin;
    public void PlayerNameInput()
    {
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
    }
}
