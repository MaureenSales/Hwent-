using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData gameData;
    public static string namePlayer = "";
    public static string nameEnemy = "";
    public static Deck playerDeck;
    public static Deck enemyDeck;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        if(gameData == null)
        {
            gameData = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(gameData != this)
        {
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
