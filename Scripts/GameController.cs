using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;
    public GameObject Message;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        Player.GetComponentInChildren<PlayerController>().IsYourTurn = true;
        Message.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWinner(Player.transform.Find("PlayerField").GetComponentInChildren<SumTotalPower>().total, Enemy.transform.Find("EnemyField").GetComponentInChildren<SumTotalPower>().total);
    }

    private void UpdateWinner(int? powerPlayer, int? powerEnemy)
    {
        if (powerPlayer != null && powerEnemy != null)
        {
            if (powerPlayer > powerEnemy)
            {
                Player.GetComponentInChildren<PlayerController>().WinnerIndicator.SetActive(true);
                Enemy.GetComponentInChildren<PlayerController>().WinnerIndicator.SetActive(false);
            }
            else if (powerPlayer < powerEnemy)
            {
                Enemy.GetComponentInChildren<PlayerController>().WinnerIndicator.SetActive(true);
                Player.GetComponentInChildren<PlayerController>().WinnerIndicator.SetActive(false);
            }
            else
            {
                Player.GetComponentInChildren<PlayerController>().WinnerIndicator.SetActive(false);
                Enemy.GetComponentInChildren<PlayerController>().WinnerIndicator.SetActive(false);
            }

        }

    }

    public void ImproveUnits(GameObject unit, string zone)
    {
        Debug.Log("EnterImproveUnit");

        int newPower = int.Parse(unit.GetComponent<ThisCard>().powerText.text) + 2;

        unit.GetComponent<ThisCard>().powerText.text = newPower.ToString();

        switch (zone)
        {
            case "Melee": GameObject.Find("MeleeRow").GetComponentInChildren<SumPower>().UpdatePower(); break;
            case "Ranged": GameObject.Find("RangedRow").GetComponentInChildren<SumPower>().UpdatePower(); break;
            case "Siege": GameObject.Find("SiegeRow").GetComponentInChildren<SumPower>().UpdatePower(); break;
        }

    }

    public void ImproveUnits(List<GameObject> units)
    {
        foreach (var unit in units)
        {
            if (!(unit.GetComponent<ThisCard>().thisCard is HeroUnit))
            {

                int newPower = int.Parse(unit.GetComponent<ThisCard>().powerText.text) + 2;


                unit.GetComponent<ThisCard>().powerText.text = newPower.ToString();
                unit.transform.parent.parent.GetComponentInChildren<SumPower>().UpdatePower();
            }


        }
    }

    public void FinalizedTurn(GameObject thisTurn)
    {
        Debug.Log("FinalizedTurn");
        Debug.Log(thisTurn.transform.GetComponentInChildren<PlayerController>().IsYourTurn);
        StartCoroutine(ChangeTurn(thisTurn));

    }

    private IEnumerator ChangeTurn(GameObject thisPlayerTurn)
    {
        yield return new WaitForSeconds(0.5f);
        thisPlayerTurn.transform.GetComponentInChildren<PlayerController>().IsYourTurn = false;

        if (thisPlayerTurn.name == "Player")
        {
            GameObject.Find("Enemy").GetComponentInChildren<PlayerController>().IsYourTurn = true;
            Message.gameObject.SetActive(true);
            Message.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Turno de " + GameObject.Find("Enemy").GetComponentInChildren<PlayerController>().Nick;
        }
        else if (thisPlayerTurn.name == "Enemy")
        {
            GameObject.Find("Player").GetComponentInChildren<PlayerController>().IsYourTurn = true;
            Message.gameObject.SetActive(true);
            Message.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Turno de " + GameObject.Find("Player").GetComponentInChildren<PlayerController>().Nick;
        }
        yield return new WaitForSeconds(0.5f);
        SwapObjects(thisPlayerTurn);
        yield return new WaitForSeconds(1f);
        Message.gameObject.SetActive(false);
    }

    private void SwapObjects(GameObject thisPlayerTurn)
    {
        Vector3 positionPlayerSumTotalPower = Player.transform.Find("PlayerField").Find("SumTotalPower").position;
        Vector3 positionPlayerMeleeRow = Player.transform.Find("PlayerField").Find("MeleeRow").position;
        Vector3 positionPlayerFrost = Player.transform.Find("PlayerField").Find("MeleeRow").Find("Frost").position;
        Vector3 positionPlayerRangedRow = Player.transform.Find("PlayerField").Find("RangedRow").position;
        Vector3 positionPlayerFog = Player.transform.Find("PlayerField").Find("RangedRow").Find("Fog").position;
        Vector3 positionPlayerSiegeRow = Player.transform.Find("PlayerField").Find("SiegeRow").position;
        Vector3 positionPlayerRain = Player.transform.Find("PlayerField").Find("SiegeRow").Find("Rain").position;

        Vector3 positionEnemySumTotalPower = Enemy.transform.Find("EnemyField").Find("SumTotalPower").position;
        Vector3 positionEnemyMeleeRow = Enemy.transform.Find("EnemyField").Find("MeleeRow").position;
        Vector3 positionEnemyFrost = Enemy.transform.Find("EnemyField").Find("MeleeRow").Find("Frost").position;
        Vector3 positionEnemyRangedRow = Enemy.transform.Find("EnemyField").Find("RangedRow").position;
        Vector3 positionEnemyFog = Enemy.transform.Find("EnemyField").Find("RangedRow").Find("Fog").position;
        Vector3 positionEnemySiegeRow = Enemy.transform.Find("EnemyField").Find("SiegeRow").position;
        Vector3 positionEnemyRain = Enemy.transform.Find("EnemyField").Find("SiegeRow").Find("Rain").position;

        Player.transform.Find("PlayerField").Find("SumTotalPower").position = positionEnemySumTotalPower;
        Player.transform.Find("PlayerField").Find("MeleeRow").position = positionEnemyMeleeRow;
        Player.transform.Find("PlayerField").Find("MeleeRow").Find("Frost").position = positionEnemyFrost;
        Player.transform.Find("PlayerField").Find("RangedRow").position = positionEnemyRangedRow;
        Player.transform.Find("PlayerField").Find("RangedRow").Find("Fog").position = positionEnemyFog;
        Player.transform.Find("PlayerField").Find("SiegeRow").position = positionEnemySiegeRow;
        Player.transform.Find("PlayerField").Find("SiegeRow").Find("Rain").position = positionEnemyRain;

        Enemy.transform.Find("EnemyField").Find("SumTotalPower").position = positionPlayerSumTotalPower;
        Enemy.transform.Find("EnemyField").Find("MeleeRow").position = positionPlayerMeleeRow;
        Enemy.transform.Find("EnemyField").Find("MeleeRow").Find("Frost").position = positionPlayerFrost;
        Enemy.transform.Find("EnemyField").Find("RangedRow").position = positionPlayerRangedRow;
        Enemy.transform.Find("EnemyField").Find("RangedRow").Find("Fog").position = positionPlayerFog;
        Enemy.transform.Find("EnemyField").Find("SiegeRow").position = positionPlayerSiegeRow;
        Enemy.transform.Find("EnemyField").Find("SiegeRow").Find("Rain").position = positionPlayerRain;

        Vector3 positionPlayerInfo = Player.transform.Find("PlayerInfo").position;
        Vector3 positionPlayerNick = Player.transform.Find("PlayerInfo").Find("Nick").position;
        Vector3 positionPlayerAvatar = Player.transform.Find("PlayerInfo").Find("Avatar").position;
        Vector3 positionPlayerTurn = Player.transform.Find("PlayerInfo").Find("Turn").position;
        Vector3 positionPlayerCards = Player.transform.Find("PlayerInfo").Find("Cards").position;
        Vector3 positionPlayerWinnerIndicator = Player.transform.Find("PlayerInfo").Find("WinnerIndicator").position;
        Vector3 positionPlayerRoundWon = Player.transform.Find("PlayerInfo").Find("RoundWon").position;

        Vector3 positionEnemyInfo = Enemy.transform.Find("EnemyInfo").position;
        Vector3 positionEnemyNick = Enemy.transform.Find("EnemyInfo").Find("Nick").position;
        Vector3 positionEnemyAvatar = Enemy.transform.Find("EnemyInfo").Find("Avatar").position;
        Vector3 positionEnemyTurn = Enemy.transform.Find("EnemyInfo").Find("Turn").position;
        Vector3 positionEnemyCards = Enemy.transform.Find("EnemyInfo").Find("Cards").position;
        Vector3 positionEnemyWinnerIndicator = Enemy.transform.Find("EnemyInfo").Find("WinnerIndicator").position;
        Vector3 positionEnemyRoundWon = Enemy.transform.Find("EnemyInfo").Find("RoundWon").position;

        Player.transform.Find("PlayerInfo").position = positionEnemyInfo;
        Player.transform.Find("PlayerInfo").Find("Nick").position = positionEnemyNick;
        Player.transform.Find("PlayerInfo").Find("Avatar").position = positionEnemyAvatar;
        Player.transform.Find("PlayerInfo").Find("Turn").position = positionEnemyTurn;
        Player.transform.Find("PlayerInfo").Find("Cards").position = positionEnemyCards;
        Player.transform.Find("PlayerInfo").Find("WinnerIndicator").position = positionEnemyWinnerIndicator;
        Player.transform.Find("PlayerInfo").Find("RoundWon").position = positionEnemyRoundWon;

        Enemy.transform.Find("EnemyInfo").position = positionPlayerInfo;
        Enemy.transform.Find("EnemyInfo").Find("Nick").position = positionPlayerNick;
        Enemy.transform.Find("EnemyInfo").Find("Avatar").position = positionPlayerAvatar;
        Enemy.transform.Find("EnemyInfo").Find("Turn").position = positionPlayerTurn;
        Enemy.transform.Find("EnemyInfo").Find("Cards").position = positionPlayerCards;
        Enemy.transform.Find("EnemyInfo").Find("WinnerIndicator").position = positionPlayerWinnerIndicator;
        Enemy.transform.Find("EnemyInfo").Find("RoundWon").position = positionPlayerRoundWon;


    }

    private void TransitionChangeTurn()
    {

    }
}
