using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Método para continuar a la siguiente escena
    /// </summary>
    async public void Play()
    {
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<AudioSource>().Play();
        await Task.Delay(80);
        SceneManager.LoadScene("Login");
    }
    /// <summary>
    /// Método para salir del juego
    /// </summary>
    async public void Quit()
    {
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<AudioSource>().Play();
        await Task.Delay(80);
        UnityEditor.EditorApplication.isPlaying = false;
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
