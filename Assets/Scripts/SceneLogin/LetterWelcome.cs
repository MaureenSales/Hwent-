using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LetterWelcome : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string path = "/home/maureensb/Documentos/cardgame/gwent-pro-2d-template-main/Assets/Scripts/letter.txt";
        using (StreamReader reader = new StreamReader(path))
        {
            string content = reader.ReadToEnd();
            StartCoroutine(WritterTurn(content));
        }


    }

    private IEnumerator WritterTurn(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            this.GetComponent<TextMeshProUGUI>().text += text[i];
            yield return new WaitForSeconds(0.06f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
