using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData gameData;
    public static Dictionary<string, Effect> Effects;
    public static string namePlayer = "";
    public static string nameEnemy = "";
    public static Deck playerDeck;
    public static Deck enemyDeck;
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

/// <summary>
/// Poner en valores iniciales la información de los jugadores
/// </summary>
    public static void Reset()
    {
        namePlayer = "";
        nameEnemy = "";
        playerDeck = null;
        enemyDeck = null;
    }

}
