using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class Controller : MonoBehaviour
{
    public string source;
    public TMP_InputField tMP_Editor;
    public GameObject buttonAccept;
    public TextMeshProUGUI error;
    public GameObject windowError;
    public GameObject messageSuccessfully;
    public Button buttonCancel;

    public async void RunInterprete()
    {
        Debug.Log("ready");

        source = tMP_Editor.text;
        Debug.Log(source);
        try
        {
            Debug.Log("enter try");
            Lexer lexer = new Lexer(source);
            foreach (Token token in lexer.Tokens)
            {
                token.ToString();
            }
            Debug.Log("Successfully completed " + "🎊");

            Parser parser = new Parser(lexer);
            PrintASTnode printer = new PrintASTnode();

            foreach (var stm in parser.Statements)
            {
                System.Console.WriteLine(printer.Print(stm));
                Debug.Log(printer.Print(stm));
                Debug.Log("");
            }

            Debug.Log("Successfully completed " + "🎊");

            Evaluador evaluador = new Evaluador();
            foreach (var stm in parser.Statements)
            {
                Debug.Log(evaluador.evaluate(stm));
                Debug.Log("");
            }

            messageSuccessfully.SetActive(true);
            tMP_Editor.text = "";
            await Task.Delay(800);
            messageSuccessfully.SetActive(false);
        }
        catch (System.Exception e)
        {
            windowError.SetActive(true);
            error.text = e.Message.ToString();
            Debug.Log(e.Message);
            Debug.Log(e.StackTrace);
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
