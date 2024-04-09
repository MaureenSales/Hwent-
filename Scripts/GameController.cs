using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using JetBrains.Annotations;

public class GameController : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;
    public GameObject Message;
    public GameObject HandPlayer;
    public GameObject HandEnemy;
    public GameObject DeckPlayer;
    public GameObject DeckEnemy;
    public GameObject currentTurn;
    public GameObject notCurrentTurn;
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

    async public void Effects(GameObject unit)
    {
        Debug.Log("EnterEffect");
        Card unitCard = unit.GetComponent<ThisCard>().thisCard;

        if (unitCard.Skill == Global.Effects["DrawCard"])
        {
            Debug.Log("EnterDrawCard");
            if (currentTurn.name == "Player")
            {
                await Task.Delay(800);
                DeckPlayer.GetComponent<Draw>().DrawCard();
            }
            else
            {
                await Task.Delay(800);
                DeckEnemy.GetComponent<Draw>().DrawCard();
            }
        }
        else if (unitCard.Skill == Global.Effects["PutBoost"])
        {
            Debug.Log("EnterPutBoost");
            if (currentTurn.name == "Player")
            {

            }
            else
            {

            }
        }
        else if (unitCard.Skill == Global.Effects["PutWeather"])
        {
            Debug.Log("EnterPutWeather");
            if (currentTurn.name == "Player")
            {

            }
            else
            {

            }
        }
        else if (unitCard.Skill == Global.Effects["PowerfulCard"])
        {
            Debug.Log("PowerfulCard");
            if (currentTurn.name == "Player")
            {

            }
            else
            {

            }
        }
        else if (unitCard.Skill == Global.Effects["LessPowerCard"])
        {
            Debug.Log("LessPowerCard");
            if (currentTurn.name == "Player")
            {

            }
            else
            {

            }
        }
        else if (unitCard.Skill == Global.Effects["Average"])
        {
            Debug.Log("Average");
            if (currentTurn.name == "Player")
            {

            }
            else
            {

            }
        }
        else if (unitCard.Skill == Global.Effects["ClearRow"])
        {
            Debug.Log("ClearRow");
            Debug.Log(notCurrentTurn.name);

            int indexRow = -1;
            int countUnit = 10;
            for (int i = 1; i < notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").childCount; i++)
            {
                if (notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetComponentInChildren<Row>().unitObjects.Count != 0 && notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetComponentInChildren<Row>().unitObjects.Count < countUnit)
                {
                    countUnit = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetComponentInChildren<Row>().unitObjects.Count;
                    indexRow = i;
                }
            }
            Debug.Log(indexRow);
            Debug.Log(countUnit);
            Debug.Log(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).name);

            if (indexRow != -1)
            {
                for (int i = 0; i < notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).GetComponentInChildren<Row>().unitObjects.Count; i++)
                {
                    if (notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).name == "MeleeRow")
                    {
                        if (!(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).Find("MeleeZone").GetChild(i).GetComponent<ThisCard>().thisCard is HeroUnit))
                        {
                            Destroy(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).Find("MeleeZone").GetChild(i).gameObject);

                        }

                    }
                    else if (notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).name == "RangedRow")
                    {
                        if (!(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).Find("RangeZone").GetChild(i).GetComponent<ThisCard>().thisCard is HeroUnit))
                        {
                            Destroy(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).Find("RangedZone").GetChild(i).gameObject);
                        }
                    }
                    else
                    {
                        if (!(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).Find("SiegeZone").GetChild(i).GetComponent<ThisCard>().thisCard is HeroUnit))
                        {
                            Debug.Log("EnterDestroySiege");
                            Destroy(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).Find("SiegeZone").GetChild(i).gameObject);
                        }
                    }
                }
                Debug.Log(notCurrentTurn);
                Debug.Log(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).GetComponentInChildren<Row>().unitObjects.Count);
                notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).GetComponentInChildren<Row>().unitObjects = new List<GameObject>();
                notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).GetComponentInChildren<Row>().unitsInRow = new List<UnitCard>();
            }

        }
        else if (unitCard.Skill == Global.Effects["MultiplyPower"])
        {
            Debug.Log("MultiplyPower");
            if (currentTurn.name == "Player")
            {

            }
            else
            {

            }
        }

    }

    public void Improve(GameObject eventData, string zone)
    {
        Debug.Log("EnterImprove");
        GameObject boost = null;

        boost = GameObject.Find(currentTurn.name + "Field").transform.Find(zone + "Row").Find("Boost" + zone).gameObject;

        if ((boost.transform.childCount != 0) && (boost.name == "BoostMelee"))
        {
            Debug.Log("EnterBoostMelee");
            GetComponentInParent<Canvas>().GetComponent<GameController>().ImproveUnits(eventData.gameObject, zone);
        }
        else if ((boost.transform.childCount != 0) && (boost.name == "BoostRanged"))
        {
            Debug.Log("EnterBoostRanged");
            GetComponentInParent<Canvas>().GetComponent<GameController>().ImproveUnits(eventData.gameObject, zone);
        }
        else if ((boost.transform.childCount != 0) && (boost.name == "BoostSiege"))
        {
            Debug.Log("EnterBoostSiege");
            GetComponentInParent<Canvas>().GetComponent<GameController>().ImproveUnits(eventData.gameObject, zone);
        }
    }


    public void ImproveUnits(GameObject unit, string zone)
    {
        Debug.Log("EnterImproveUnit");

        int newPower = int.Parse(unit.GetComponent<ThisCard>().powerText.text) + 2;

        unit.GetComponent<ThisCard>().powerText.text = newPower.ToString();

        switch (zone)
        {
            case "Melee": currentTurn.transform.Find(currentTurn.name + "Field").Find("MeleeRow").GetComponentInChildren<SumPower>().UpdatePower(); break;
            case "Ranged": currentTurn.transform.Find(currentTurn.name + "Field").Find("RangedRow").GetComponentInChildren<SumPower>().UpdatePower(); break;
            case "Siege": currentTurn.transform.Find(currentTurn.name + "Field").Find("SiegeRow").GetComponentInChildren<SumPower>().UpdatePower(); break;
        }



    }

    public void ImproveUnits(List<GameObject> units)
    {
        Debug.Log("EnterImproveUnitsList");

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
    public void FinalizedTurn(GameObject thisTurn)
    {
        Debug.Log("FinalizedTurn");
        StartCoroutine(ChangeTurn(thisTurn));

    }

    private IEnumerator ChangeTurn(GameObject thisPlayerTurn)
    {
        yield return new WaitForSeconds(2f);
        yield return new WaitForEndOfFrame();
        thisPlayerTurn.transform.GetComponentInChildren<PlayerController>().IsYourTurn = false;

        if (thisPlayerTurn.name == "Player")
        {
            currentTurn = Enemy;
            notCurrentTurn = Player;
            GameObject.Find("Enemy").GetComponentInChildren<PlayerController>().IsYourTurn = true;
            Message.gameObject.SetActive(true);
            Message.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Turno de " + GameObject.Find("Enemy").GetComponentInChildren<PlayerController>().Nick;
        }
        else if (thisPlayerTurn.name == "Enemy")
        {
            currentTurn = Player;
            notCurrentTurn = Enemy;
            GameObject.Find("Player").GetComponentInChildren<PlayerController>().IsYourTurn = true;
            Message.gameObject.SetActive(true);
            Message.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Turno de " + GameObject.Find("Player").GetComponentInChildren<PlayerController>().Nick;
        }
        yield return new WaitForSeconds(0.5f);
        SwapObjects();
        yield return new WaitForSeconds(1.2f);
        Message.gameObject.SetActive(false);
    }

    private void SwapObjects()
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
