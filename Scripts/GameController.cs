using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using JetBrains.Annotations;
using System;

public class GameController : MonoBehaviour
{
    public GameObject Message;
    public GameObject HandPlayer;
    public GameObject HandEnemy;
    public GameObject DeckPlayer;
    public GameObject DeckEnemy;
    public GameObject GraveyardPlayer;
    public GameObject currentTurn;
    public GameObject notCurrentTurn;
    public GameObject CardPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        currentTurn.GetComponentInChildren<PlayerController>().IsYourTurn = true;
        Message.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWinner(currentTurn.transform.Find(currentTurn.name + "Field").GetComponentInChildren<SumTotalPower>().total, notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetComponentInChildren<SumTotalPower>().total);
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
            List<Card> cards;
            Boost boost = null;

            if (currentTurn.name == "Player")
            {
                cards = GameData.playerDeck.cards;
            }
            else
            {
                cards = GameData.enemyDeck.cards;
            }

            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i] is Boost)
                {
                    boost = (Boost)cards[i];
                    break;
                }
            }

            if (boost != null)
            {
                int indexRow = -1;
                int countUnit = -1;
                for (int i = 1; i < currentTurn.transform.Find(currentTurn.name + "Field").childCount; i++)
                {
                    Debug.Log(currentTurn.name);
                    Debug.Log(currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).name);
                    Debug.Log(currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetComponentInChildren<Row>().unitObjects.Count);

                    if (currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetComponentInChildren<Row>().unitObjects.Count > countUnit && currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(2).childCount == 0)
                    {
                        indexRow = i;
                        countUnit = currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetComponentInChildren<Row>().unitObjects.Count;
                    }
                }

                GameObject boostCard = Instantiate(CardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                boostCard.GetComponent<ThisCard>().PrintCard(boost);
                boostCard.transform.SetParent(currentTurn.transform.Find(currentTurn.name + "Field").GetChild(indexRow).GetChild(2), false);
                List<GameObject> units = new();
                for (int i = 0; i < currentTurn.transform.Find(currentTurn.name + "Field").GetChild(indexRow).GetComponentInChildren<Row>().unitObjects.Count - 1; i++)
                {
                    units.Add(currentTurn.transform.Find(currentTurn.name + "Field").GetChild(indexRow).GetComponentInChildren<Row>().unitObjects[i]);
                }
                ImproveUnits(units);

                if (currentTurn.name == "Player")
                {
                    Debug.Log(GameData.playerDeck.cards.Contains(boost));
                    GameData.playerDeck.cards.Remove(boost);
                    Debug.Log(GameData.playerDeck.cards.Contains(boost));
                }
                else
                {
                    Debug.Log(GameData.enemyDeck.cards.Contains(boost));
                    GameData.enemyDeck.cards.Remove(boost);
                    Debug.Log(GameData.enemyDeck.cards.Contains(boost));
                }
            }
        }
        else if (unitCard.Skill == Global.Effects["PutWeather"])
        {
            Debug.Log("EnterPutWeather");
            List<Card> cards;
            Weather weather = null;

            if (currentTurn.name == "Player")
            {
                cards = GameData.playerDeck.cards;
            }
            else
            {
                cards = GameData.enemyDeck.cards;
            }

            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i] is Weather)
                {
                    weather = (Weather)cards[i];
                    break;
                }
            }

            if (weather != null)
            {
                Debug.Log(weather.Name);
                switch (weather.Name)
                {
                    case "Escarcha Heladora":
                        if (!GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[0])
                        {
                            GameObject weatherCard = Instantiate(CardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                            weatherCard.GetComponent<ThisCard>().PrintCard(weather);
                            weatherCard.transform.SetParent(GameObject.Find("WeatherZone").transform.GetChild(0), false);
                            GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[0] = true;
                            GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(GameObject.Find(currentTurn.name + "Field").transform.Find("MeleeRow").GetComponentInChildren<Row>().unitObjects, currentTurn.name);
                            GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(GameObject.Find(notCurrentTurn.name + "Field").transform.Find("MeleeRow").GetComponentInChildren<Row>().unitObjects, notCurrentTurn.name);
                            GameObject.Find("WeatherZone").GetComponent<WeatherController>().ApplyWeather("Frost");
                        }
                        break;
                    case "Niebla Profunda":
                        if (!GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[1])
                        {
                            GameObject weatherCard = Instantiate(CardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                            weatherCard.GetComponent<ThisCard>().PrintCard(weather);
                            weatherCard.transform.SetParent(GameObject.Find("WeatherZone").transform.GetChild(1), false);
                            GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[1] = true;
                            GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(GameObject.Find(currentTurn.name + "Field").transform.Find("RangedRow").GetComponentInChildren<Row>().unitObjects, currentTurn.name);
                            GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(GameObject.Find(notCurrentTurn.name + "Field").transform.Find("RangedRow").GetComponentInChildren<Row>().unitObjects, notCurrentTurn.name);
                            GameObject.Find("WeatherZone").GetComponent<WeatherController>().ApplyWeather("Fog");
                        }
                        break;
                    case "Diluvio Quidditch":
                        if (!GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[2])
                        {
                            GameObject weatherCard = Instantiate(CardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                            weatherCard.GetComponent<ThisCard>().PrintCard(weather);
                            weatherCard.transform.SetParent(GameObject.Find("WeatherZone").transform.GetChild(2), false);
                            GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[2] = true;
                            GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(GameObject.Find(currentTurn.name + "Field").transform.Find("SiegeRow").GetComponentInChildren<Row>().unitObjects, currentTurn.name);
                            GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(GameObject.Find(notCurrentTurn.name + "Field").transform.Find("SiegeRow").GetComponentInChildren<Row>().unitObjects, notCurrentTurn.name);
                            GameObject.Find("WeatherZone").GetComponent<WeatherController>().ApplyWeather("Rain");
                        }
                        break;
                }

                if (currentTurn.name == "Player")
                {
                    Debug.Log(GameData.playerDeck.cards.Contains(weather));
                    GameData.playerDeck.cards.Remove(weather);
                    Debug.Log(GameData.playerDeck.cards.Contains(weather));
                }
                else
                {
                    Debug.Log(GameData.enemyDeck.cards.Contains(weather));
                    GameData.enemyDeck.cards.Remove(weather);
                    Debug.Log(GameData.enemyDeck.cards.Contains(weather));
                }
            }
        }
        else if (unitCard.Skill == Global.Effects["PowerfulCard"])
        {
            Debug.Log("PowerfulCard");
            int maxPower = int.MinValue;
            int indexRow = -1;
            int indexInRowZone = -1;
            string owner = "";

            for (int i = 1; i < currentTurn.transform.Find(currentTurn.name + "Field").childCount; i++)
            {
                for (int j = 0; j < currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).childCount; j++)
                {
                    Debug.Log("i=" + i);
                    Debug.Log("j=" + j);
                    Debug.Log(currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).name);

                    if (int.Parse(currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().powerText.text) > maxPower && !(currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().thisCard is HeroUnit))
                    {
                        maxPower = int.Parse(currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().powerText.text);
                        indexRow = i;
                        indexInRowZone = j;
                        owner = currentTurn.name;
                    }


                }
            }

            for (int i = 1; i < notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").childCount; i++)
            {
                for (int j = 0; j < notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).childCount; j++)
                {
                    if (int.Parse(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().powerText.text) > maxPower && !(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().thisCard is HeroUnit))
                    {
                        maxPower = int.Parse(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().powerText.text);
                        indexRow = i;
                        indexInRowZone = j;
                        owner = notCurrentTurn.name;
                    }

                }
            }
            Debug.Log(indexRow);
            if (indexRow != -1)
            {
                if (owner == notCurrentTurn.name)
                {
                    if (int.Parse(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).GetChild(0).GetChild(indexInRowZone).GetComponent<ThisCard>().powerText.text) > int.Parse(unit.GetComponent<ThisCard>().powerText.text))
                    {
                        notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).GetChild(0).GetComponent<Row>().RemoveFromRow(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).GetChild(0).GetChild(indexInRowZone).gameObject);
                        Destroy(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).GetChild(0).GetChild(indexInRowZone).gameObject);
                        notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).GetComponentInChildren<SumPower>().UpdatePower();
                    }
                    else
                    {
                        unit.GetComponent<Drag>().parentToReturnTo.GetComponent<Row>().RemoveFromRow(unit);
                        Destroy(unit);
                    }

                }
                else
                {
                    if (int.Parse(currentTurn.transform.Find(currentTurn.name + "Field").GetChild(indexRow).GetChild(0).GetChild(indexInRowZone).GetComponent<ThisCard>().powerText.text) > int.Parse(unit.GetComponent<ThisCard>().powerText.text))
                    {
                        currentTurn.transform.Find(currentTurn.name + "Field").GetChild(indexRow).GetChild(0).GetComponent<Row>().RemoveFromRow(currentTurn.transform.Find(currentTurn.name + "Field").GetChild(indexRow).GetChild(0).GetChild(indexInRowZone).gameObject);
                        Destroy(currentTurn.transform.Find(currentTurn.name + "Field").GetChild(indexRow).GetChild(0).GetChild(indexInRowZone).gameObject);
                        currentTurn.transform.Find(currentTurn.name + "Field").GetChild(indexRow).GetComponentInChildren<SumPower>().UpdatePower();
                    }
                    else
                    {
                        unit.GetComponent<Drag>().parentToReturnTo.GetComponent<Row>().RemoveFromRow(unit);
                        await Task.Delay(800);
                        Destroy(unit);
                    }
                }
            }
            else
            {
                Debug.Log("destroy");
                unit.GetComponent<Drag>().parentToReturnTo.GetComponent<Row>().RemoveFromRow(unit);
                await Task.Delay(800);
                Destroy(unit);
            }

        }
        else if (unitCard.Skill == Global.Effects["LessPowerCard"])
        {
            Debug.Log("LessPowerCard");
            int minPower = int.MaxValue;
            int indexRow = -1;
            int indexInRowZone = -1;
            for (int i = 1; i < notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").childCount; i++)
            {
                for (int j = 0; j < notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).childCount; j++)
                {
                    Debug.Log("i=" + i);
                    Debug.Log("j=" + j);
                    Debug.Log(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).name);


                    if (int.Parse(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().powerText.text) < minPower && !(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().thisCard is HeroUnit))
                    {
                        minPower = int.Parse(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().powerText.text);
                        indexRow = i;
                        indexInRowZone = j;
                    }

                }
            }

            if (indexRow != -1)
            {
                notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).GetChild(0).GetComponent<Row>().RemoveFromRow(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).GetChild(0).GetChild(indexInRowZone).gameObject);
                Destroy(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).GetChild(0).GetChild(indexInRowZone).gameObject);
                notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).GetComponentInChildren<SumPower>().UpdatePower();
            }

        }
        else if (unitCard.Skill == Global.Effects["Average"])
        {
            Debug.Log("Average");

            int totalPowerField = 0;
            int countCardInField = 0;

            totalPowerField += currentTurn.transform.Find(currentTurn.name + "Field").GetComponentInChildren<SumTotalPower>().total;
            totalPowerField += notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetComponentInChildren<SumTotalPower>().total;

            countCardInField += currentTurn.transform.Find(currentTurn.name + "Field").Find("MeleeRow").GetChild(0).childCount;
            countCardInField += currentTurn.transform.Find(currentTurn.name + "Field").Find("RangedRow").GetChild(0).childCount;
            countCardInField += currentTurn.transform.Find(currentTurn.name + "Field").Find("SiegeRow").GetChild(0).childCount;

            countCardInField += notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("MeleeRow").GetChild(0).childCount;
            countCardInField += notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("RangedRow").GetChild(0).childCount;
            countCardInField += notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("SiegeRow").GetChild(0).childCount;

            int ApproximateAverage = totalPowerField / (countCardInField + 1);
            Debug.Log(countCardInField);
            Debug.Log(totalPowerField);
            Debug.Log(ApproximateAverage);

            for (int i = 1; i < currentTurn.transform.Find(currentTurn.name + "Field").childCount; i++)
            {
                for (int j = 0; j < currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).childCount; j++)
                {
                    Debug.Log("i=" + i);
                    Debug.Log("j=" + j);
                    Debug.Log(currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).name);

                    if (!(currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().thisCard is HeroUnit))
                    {
                        currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().powerText.text = ApproximateAverage.ToString();
                    }
                }
                currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetComponentInChildren<SumPower>().UpdatePower();
                if (currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(2).childCount != 0)
                {
                    ImproveUnits(currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetComponentInChildren<Row>().unitObjects);
                }
                if (GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[i - 1])
                {
                    GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).GetComponent<Row>().unitObjects, currentTurn.name);
                }
            }


            for (int i = 1; i < notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").childCount; i++)
            {
                for (int j = 0; j < notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).childCount; j++)
                {
                    Debug.Log("i=" + i);
                    Debug.Log("j=" + j);
                    Debug.Log(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).name);
                    if (!(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().thisCard is HeroUnit))
                    {
                        notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().powerText.text = ApproximateAverage.ToString();
                    }

                }
                notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetComponentInChildren<SumPower>().UpdatePower();
                if (notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(2).childCount != 0)
                {
                    ImproveUnits(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetComponentInChildren<Row>().unitObjects);
                }

                if (GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[i - 1])
                {
                    GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).GetComponent<Row>().unitObjects, notCurrentTurn.name);
                }
            }

            if (!(unitCard is HeroUnit))
            {
                unit.GetComponent<ThisCard>().powerText.text = ApproximateAverage.ToString();
                unit.GetComponent<Drag>().parentToReturnTo.parent.GetComponentInChildren<SumPower>().UpdatePower();
                GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(unit, unit.GetComponent<Drag>().parentToReturnTo);
                switch (unit.GetComponent<Drag>().parentToReturnTo.name)
                {
                    case "MeleeZone": Improve(unit, "Melee"); break;
                    case "RangedZone": Improve(unit, "Ranged"); break;
                    case "SiegeZone": Improve(unit, "Siege"); break;
                }
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
                        Debug.Log(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).Find("RangeZone").GetChild(i).name);
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

                    notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).GetComponentInChildren<SumPower>().UpdatePower();
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
            int count = 0;
            for (int i = 1; i < currentTurn.transform.Find(currentTurn.name + "Field").childCount; i++)
            {
                for (int j = 0; j < currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).childCount; j++)
                {
                    Debug.Log("i=" + i);
                    Debug.Log("j=" + j);
                    Debug.Log(currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).name);

                    if (currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().thisCard == unitCard)
                    {
                        count++;
                    }


                }
            }

            if (count != 0)
            {
                Debug.Log(unit.GetComponent<Drag>().parentToReturnTo.parent.name);
                unit.GetComponent<ThisCard>().powerText.text = (int.Parse(unit.GetComponent<ThisCard>().powerText.text) * count).ToString();
                unit.GetComponent<Drag>().parentToReturnTo.parent.GetComponentInChildren<SumPower>().UpdatePower();
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
            ImproveUnits(eventData.gameObject, zone);
        }
        else if ((boost.transform.childCount != 0) && (boost.name == "BoostRanged"))
        {
            Debug.Log("EnterBoostRanged");
            ImproveUnits(eventData.gameObject, zone);
        }
        else if ((boost.transform.childCount != 0) && (boost.name == "BoostSiege"))
        {
            Debug.Log("EnterBoostSiege");
            ImproveUnits(eventData.gameObject, zone);
        }
    }

    public void ImproveAfterWeather(GameObject eventData, string zone, string owner)
    {
        Debug.Log("EnterImprove");
        GameObject boost = null;
        if (owner == currentTurn.name)
        {
            boost = currentTurn.transform.Find(currentTurn.name + "Field").transform.Find(zone + "Row").Find("Boost" + zone).gameObject;

        }
        else
        {
            boost = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").transform.Find(zone + "Row").Find("Boost" + zone).gameObject;
        }

        if ((boost.transform.childCount != 0) && (boost.name == "BoostMelee"))
        {
            Debug.Log("EnterBoostMelee");
            ImproveUnits(eventData.gameObject, zone);
        }
        else if ((boost.transform.childCount != 0) && (boost.name == "BoostRanged"))
        {
            Debug.Log("EnterBoostRanged");
            ImproveUnits(eventData.gameObject, zone);
        }
        else if ((boost.transform.childCount != 0) && (boost.name == "BoostSiege"))
        {
            Debug.Log("EnterBoostSiege");
            ImproveUnits(eventData.gameObject, zone);
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

    public void Clear()
    {
        for (int i = 0; i < 3; i++)
        {
            if (GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[i] == true)
            {
                GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[i] = false;
                GameObject toDestroy = GameObject.Find("WeatherZone").transform.GetChild(i).gameObject;
                LeanTween.move(toDestroy, GraveyardPlayer.transform.position, 1f).setOnComplete(() => Destroy(toDestroy));
                
                switch (i)
                {
                    case 0:
                    GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherImagesPlayer[0].SetActive(false);
                    GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherImagesEnemy[0].SetActive(false);
                    ClearPower(currentTurn.transform.Find(currentTurn.name + "Field").Find("MeleeRow").GetComponentInChildren<Row>().unitObjects, currentTurn, "Melee");
                    ClearPower(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("MeleeRow").GetComponentInChildren<Row>().unitObjects, notCurrentTurn, "Melee");
                    if(currentTurn.transform.Find(currentTurn.name + "Field").Find("MeleeRow").GetChild(2).childCount > 0)
                    {
                        Debug.Log("hay aumento en melee");
                        ImproveUnits(currentTurn.transform.Find(currentTurn.name + "Field").Find("MeleeRow").GetComponentInChildren<Row>().unitObjects);
                    }
                    if(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("MeleeRow").GetChild(2).childCount > 0)
                    {
                        Debug.Log("hay aumento en melee");
                        ImproveUnits(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("MeleeRow").GetComponentInChildren<Row>().unitObjects);
                    }
                    break;
                    case 1: 
                    GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherImagesPlayer[1].SetActive(false);
                    GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherImagesEnemy[1].SetActive(false);
                    ClearPower(currentTurn.transform.Find(currentTurn.name + "Field").Find("RangedRow").GetComponentInChildren<Row>().unitObjects, currentTurn, "Ranged");
                    ClearPower(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("RangedRow").GetComponentInChildren<Row>().unitObjects, notCurrentTurn, "Ranged");
                    if(currentTurn.transform.Find(currentTurn.name + "Field").Find("RangedRow").GetChild(2).childCount > 0)
                    {
                        Debug.Log("hay aumento en ranged");
                        ImproveUnits(currentTurn.transform.Find(currentTurn.name + "Field").Find("RangedRow").GetComponentInChildren<Row>().unitObjects);
                    }
                    if(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("RangedRow").GetChild(2).childCount > 0)
                    {
                        Debug.Log("hay aumento en ranged");
                        ImproveUnits(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("RangedRow").GetComponentInChildren<Row>().unitObjects);
                    }
                    break;
                    case 2:
                    GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherImagesPlayer[2].SetActive(false);
                    GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherImagesEnemy[2].SetActive(false); 
                    ClearPower(currentTurn.transform.Find(currentTurn.name + "Field").Find("SiegeRow").GetComponentInChildren<Row>().unitObjects, currentTurn, "Siege");
                    ClearPower(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("SiegeRow").GetComponentInChildren<Row>().unitObjects, notCurrentTurn, "Siege");
                    if(currentTurn.transform.Find(currentTurn.name + "Field").Find("SiegeRow").GetChild(2).childCount > 0)
                    {
                        Debug.Log("hay aumento en siege");
                        ImproveUnits(currentTurn.transform.Find(currentTurn.name + "Field").Find("SiegeRow").GetComponentInChildren<Row>().unitObjects);
                    }
                    if(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("SiegeRow").GetChild(2).childCount > 0)
                    {
                        Debug.Log("hay aumento en siege");
                        ImproveUnits(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("SiegeRow").GetComponentInChildren<Row>().unitObjects);
                    }
                    break;
                }
            }

        }
    }

    private void ClearPower(List<GameObject> units, GameObject owner, string zone)
    {
        for (int i = 0; i < units.Count; i++)
        {
            units[i].GetComponent<ThisCard>().powerText.text = units[i].GetComponent<ThisCard>().power.ToString();
        }
        owner.transform.Find(owner.name + "Field").Find(zone + "Row").GetComponentInChildren<SumPower>().UpdatePower();
    }

    private void UpdateWinner(int? powerCurrentTurn, int? powerNotCurrentTurn)
    {
        if (powerCurrentTurn != null && powerNotCurrentTurn != null)
        {
            if (powerCurrentTurn > powerNotCurrentTurn)
            {
                currentTurn.GetComponentInChildren<PlayerController>().WinnerIndicator.SetActive(true);
                notCurrentTurn.GetComponentInChildren<PlayerController>().WinnerIndicator.SetActive(false);
            }
            else if (powerCurrentTurn < powerNotCurrentTurn)
            {
                notCurrentTurn.GetComponentInChildren<PlayerController>().WinnerIndicator.SetActive(true);
                currentTurn.GetComponentInChildren<PlayerController>().WinnerIndicator.SetActive(false);
            }
            else
            {
                currentTurn.GetComponentInChildren<PlayerController>().WinnerIndicator.SetActive(false);
                notCurrentTurn.GetComponentInChildren<PlayerController>().WinnerIndicator.SetActive(false);
            }

        }

    }
    public void FinalizedTurn()
    {
        Debug.Log("FinalizedTurn");
        StartCoroutine(ChangeTurn());

    }

    private IEnumerator ChangeTurn()
    {
        yield return new WaitForSeconds(2f);
        yield return new WaitForEndOfFrame();

        if (currentTurn.transform.GetComponentInChildren<PlayerController>().Pass && !notCurrentTurn.transform.GetComponentInChildren<PlayerController>().Pass)
        {
            Message.gameObject.SetActive(true);
            Message.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = currentTurn.GetComponentInChildren<PlayerController>().Nick + " a pasado turno";
            yield return new WaitForSeconds(1f);
            Message.gameObject.SetActive(false);
        }
        else if (currentTurn.transform.GetComponentInChildren<PlayerController>().Pass && notCurrentTurn.transform.GetComponentInChildren<PlayerController>().Pass)
        {
            Message.gameObject.SetActive(true);
            Message.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = currentTurn.GetComponentInChildren<PlayerController>().Nick + " a pasado turno";
            yield return new WaitForSeconds(1f);
            Message.gameObject.SetActive(false);
            if (currentTurn.GetComponentInChildren<PlayerController>().WinnerIndicator.activeSelf)
            {
                Message.gameObject.SetActive(true);
                Message.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = currentTurn.GetComponentInChildren<PlayerController>().Nick + " a ganado la ronda";
                yield return new WaitForSeconds(1f);
                Message.gameObject.SetActive(false);
                if (!currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.activeSelf)
                {
                    currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (!currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.activeSelf)
                {
                    currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.activeSelf && notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.activeSelf)
                {
                    Message.gameObject.SetActive(true);
                    Message.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = currentTurn.GetComponentInChildren<PlayerController>().Nick + " y " + notCurrentTurn.GetComponentInChildren<PlayerController>().Nick + " han ganado el juego";
                    yield return new WaitForSeconds(1f);
                    Message.gameObject.SetActive(false);
                    ClearField();
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                    currentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                    currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.SetActive(false);
                    currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.SetActive(false);
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.SetActive(false);
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.SetActive(false);
                    yield break;
                }
                else
                {
                    Message.gameObject.SetActive(true);
                    Message.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = currentTurn.GetComponentInChildren<PlayerController>().Nick + " a ganado el juego";
                    yield return new WaitForSeconds(1f);
                    Message.gameObject.SetActive(false);
                    ClearField();
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                    currentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                    currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.SetActive(false);
                    currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.SetActive(false);
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.SetActive(false);
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.SetActive(false);
                    yield break;
                }

                ClearField();
                notCurrentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                currentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                Message.gameObject.SetActive(true);
                Message.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Turno de " + currentTurn.GetComponentInChildren<PlayerController>().Nick;
                yield return new WaitForSeconds(1f);
                Message.gameObject.SetActive(false);
                yield break;

            }
            else if (notCurrentTurn.GetComponentInChildren<PlayerController>().WinnerIndicator.activeSelf)
            {
                Message.gameObject.SetActive(true);
                Message.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = notCurrentTurn.GetComponentInChildren<PlayerController>().Nick + " a ganado la ronda";
                yield return new WaitForSeconds(1f);
                Message.gameObject.SetActive(false);
                if (!notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.activeSelf)
                {
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (!notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.activeSelf)
                {
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.activeSelf && currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.activeSelf)
                {
                    Message.gameObject.SetActive(true);
                    Message.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = currentTurn.GetComponentInChildren<PlayerController>().Nick + " y " + notCurrentTurn.GetComponentInChildren<PlayerController>().Nick + " han ganado el juego";
                    yield return new WaitForSeconds(1f);
                    Message.gameObject.SetActive(false);
                    ClearField();
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                    currentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                    currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.SetActive(false);
                    currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.SetActive(false);
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.SetActive(false);
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.SetActive(false);
                    yield break;
                }
                else
                {
                    Message.gameObject.SetActive(true);
                    Message.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = notCurrentTurn.GetComponentInChildren<PlayerController>().Nick + " a ganado el juego";
                    yield return new WaitForSeconds(1f);
                    Message.gameObject.SetActive(false);
                    ClearField();
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                    currentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                    currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.SetActive(false);
                    currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.SetActive(false);
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.SetActive(false);
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.SetActive(false);
                    yield break;
                }

                ClearField();
                notCurrentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                currentTurn.GetComponentInChildren<PlayerController>().Pass = false;

            }
            else
            {
                Message.gameObject.SetActive(true);
                Message.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Ha ocurrido un empate";
                yield return new WaitForSeconds(1f);
                Message.gameObject.SetActive(false);

                if (!currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.activeSelf)
                {
                    currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (!currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1))
                {
                    currentTurn.GetComponentInChildren<PlayerController>().transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.activeSelf && notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.activeSelf)
                {
                    Message.gameObject.SetActive(true);
                    Message.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = currentTurn.GetComponentInChildren<PlayerController>().Nick + " y " + notCurrentTurn.GetComponentInChildren<PlayerController>().Nick + " han ganado el juego";
                    yield return new WaitForSeconds(1f);
                    Message.gameObject.SetActive(false);
                    ClearField();
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                    currentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                    currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.SetActive(false);
                    currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.SetActive(false);
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.SetActive(false);
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.SetActive(false);
                    yield break;
                }
                else
                {
                    Message.gameObject.SetActive(true);
                    Message.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = currentTurn.GetComponentInChildren<PlayerController>().Nick + " ha ganado el juego";
                    yield return new WaitForSeconds(1f);
                    Message.gameObject.SetActive(false);
                    ClearField();
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                    currentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                    currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.SetActive(false);
                    currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.SetActive(false);
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.SetActive(false);
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.SetActive(false);
                    yield break;
                }

                if (!notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.activeSelf)
                {
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (!notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.activeSelf)
                {
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    Message.gameObject.SetActive(true);
                    Message.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = notCurrentTurn.GetComponentInChildren<PlayerController>().Nick + " ha ganado el juego";
                    yield return new WaitForSeconds(1f);
                    Message.gameObject.SetActive(false);
                    ClearField();
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                    currentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                    currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.SetActive(false);
                    currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.SetActive(false);
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.SetActive(false);
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.SetActive(false);
                    yield break;
                }

                ClearField();
                notCurrentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                currentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                Message.gameObject.SetActive(true);
                Message.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Turno de " + currentTurn.GetComponentInChildren<PlayerController>().Nick;
                yield return new WaitForSeconds(1f);
                Message.gameObject.SetActive(false);
                yield break;
            }
        }

        if (!notCurrentTurn.transform.GetComponentInChildren<PlayerController>().Pass)
        {
            currentTurn.transform.GetComponentInChildren<PlayerController>().IsYourTurn = false;
            GameObject temp = currentTurn;
            currentTurn = notCurrentTurn;
            notCurrentTurn = temp;
            currentTurn.GetComponentInChildren<PlayerController>().IsYourTurn = true;
            Message.gameObject.SetActive(true);
            Message.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Turno de " + currentTurn.GetComponentInChildren<PlayerController>().Nick;
            yield return new WaitForSeconds(0.5f);
            SwapObjects();
            yield return new WaitForSeconds(1.2f);
            Message.gameObject.SetActive(false);
        }
    }

    private void ClearField()
    {
        Debug.Log("ClearField");
        currentTurn.transform.Find(currentTurn.name + "Field").GetComponentInChildren<SumTotalPower>().total = 0;
        currentTurn.transform.Find(currentTurn.name + "Field").GetComponentInChildren<SumTotalPower>().totalText.text = "0";

        for (int i = 1; i < 4; i++)
        {
            for (int j = currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).childCount - 1; j >= 0; j--)
            {
                GameObject toDestroy = currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).gameObject;
                Debug.Log(toDestroy.GetComponent<ThisCard>().cardName);
                LeanTween.move(toDestroy, GraveyardPlayer.transform.position, 1f).setOnComplete(() => Destroy(toDestroy));

            }
            currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetComponentInChildren<Row>().unitObjects = new List<GameObject>();
            currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetComponentInChildren<Row>().unitsInRow = new List<UnitCard>();
            currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetComponentInChildren<SumPower>().power = 0;
            currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetComponentInChildren<SumPower>().powerText.text = "0";
            if (currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(2).childCount > 0)
            {
                Debug.Log(currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(2).name);
                GameObject toDestroy = currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(2).GetChild(0).gameObject;
                Debug.Log(toDestroy.GetComponent<ThisCard>().cardName);
                LeanTween.move(toDestroy, GraveyardPlayer.transform.position, 1f).setOnComplete(() => Destroy(toDestroy));

            }

        }

        notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetComponentInChildren<SumTotalPower>().total = 0;
        notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetComponentInChildren<SumTotalPower>().totalText.text = "0";

        for (int i = 1; i < 4; i++)
        {
            for (int j = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).childCount - 1; j >= 0; j--)
            {
                GameObject toDestroy = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).gameObject;
                Debug.Log(toDestroy.GetComponent<ThisCard>().cardName);
                LeanTween.move(toDestroy, GraveyardPlayer.transform.position, 1f).setOnComplete(() => Destroy(toDestroy));
            }
            notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetComponentInChildren<Row>().unitObjects = new List<GameObject>();
            notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetComponentInChildren<Row>().unitsInRow = new List<UnitCard>();
            notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetComponentInChildren<SumPower>().power = 0;
            notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetComponentInChildren<SumPower>().powerText.text = "0";
            if (notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(2).childCount > 0)
            {
                GameObject toDestroy = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(2).GetChild(0).gameObject;
                Debug.Log(toDestroy.GetComponent<ThisCard>().cardName);
                LeanTween.move(toDestroy, GraveyardPlayer.transform.position, 1f).setOnComplete(() => Destroy(toDestroy));
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[i])
            {
                GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherImagesPlayer[i].SetActive(false);
                GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherImagesEnemy[i].SetActive(false);
                GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[i] = false;
                GameObject toDestroy = GameObject.Find("WeatherZone").transform.GetChild(i).GetChild(0).gameObject;
                LeanTween.move(toDestroy, GraveyardPlayer.transform.position, 1f).setOnComplete(() => Destroy(toDestroy));
            }
        }


    }

    private void SwapObjects()
    {
        Vector3 positionCurrentTurnSumTotalPower = currentTurn.transform.Find(currentTurn.name + "Field").Find("SumTotalPower").position;
        Vector3 positionCurrentTurnMeleeRow = currentTurn.transform.Find(currentTurn.name + "Field").Find("MeleeRow").position;
        Vector3 positionCurrentTurnFrost = currentTurn.transform.Find(currentTurn.name + "Field").Find("MeleeRow").Find("Frost").position;
        Vector3 positionCurrentTurnRangedRow = currentTurn.transform.Find(currentTurn.name + "Field").Find("RangedRow").position;
        Vector3 positionCurrentTurnFog = currentTurn.transform.Find(currentTurn.name + "Field").Find("RangedRow").Find("Fog").position;
        Vector3 positionCurrentTurnSiegeRow = currentTurn.transform.Find(currentTurn.name + "Field").Find("SiegeRow").position;
        Vector3 positionCurrentTurnRain = currentTurn.transform.Find(currentTurn.name + "Field").Find("SiegeRow").Find("Rain").position;

        Vector3 positionNotCurrentTurnSumTotalPower = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("SumTotalPower").position;
        Vector3 positionNotCurrentTurnMeleeRow = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("MeleeRow").position;
        Vector3 positionNotCurrentTurnFrost = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("MeleeRow").Find("Frost").position;
        Vector3 positionNotCurrentTurnRangedRow = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("RangedRow").position;
        Vector3 positionNotCurrentTurnFog = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("RangedRow").Find("Fog").position;
        Vector3 positionNotCurrentTurnSiegeRow = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("SiegeRow").position;
        Vector3 positionNotCurrentTurnRain = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("SiegeRow").Find("Rain").position;

        currentTurn.transform.Find(currentTurn.name + "Field").Find("SumTotalPower").position = positionNotCurrentTurnSumTotalPower;
        currentTurn.transform.Find(currentTurn.name + "Field").Find("MeleeRow").position = positionNotCurrentTurnMeleeRow;
        currentTurn.transform.Find(currentTurn.name + "Field").Find("MeleeRow").Find("Frost").position = positionNotCurrentTurnFrost;
        currentTurn.transform.Find(currentTurn.name + "Field").Find("RangedRow").position = positionNotCurrentTurnRangedRow;
        currentTurn.transform.Find(currentTurn.name + "Field").Find("RangedRow").Find("Fog").position = positionNotCurrentTurnFog;
        currentTurn.transform.Find(currentTurn.name + "Field").Find("SiegeRow").position = positionNotCurrentTurnSiegeRow;
        currentTurn.transform.Find(currentTurn.name + "Field").Find("SiegeRow").Find("Rain").position = positionNotCurrentTurnRain;

        notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("SumTotalPower").position = positionCurrentTurnSumTotalPower;
        notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("MeleeRow").position = positionCurrentTurnMeleeRow;
        notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("MeleeRow").Find("Frost").position = positionCurrentTurnFrost;
        notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("RangedRow").position = positionCurrentTurnRangedRow;
        notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("RangedRow").Find("Fog").position = positionCurrentTurnFog;
        notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("SiegeRow").position = positionCurrentTurnSiegeRow;
        notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("SiegeRow").Find("Rain").position = positionCurrentTurnRain;

        Vector3 positionCurrentTurnInfo = currentTurn.transform.Find(currentTurn.name + "Info").position;
        Vector3 positionCurrentTurnNick = currentTurn.transform.Find(currentTurn.name + "Info").Find("Nick").position;
        Vector3 positionCurrentTurnAvatar = currentTurn.transform.Find(currentTurn.name + "Info").Find("Avatar").position;
        Vector3 positionCurrentTurn = currentTurn.transform.Find(currentTurn.name + "Info").Find("Turn").position;
        Vector3 positionCurrentTurnCards = currentTurn.transform.Find(currentTurn.name + "Info").Find("Cards").position;
        Vector3 positionCurrentTurnWinnerIndicator = currentTurn.transform.Find(currentTurn.name + "Info").Find("WinnerIndicator").position;
        Vector3 positionCurrentTurnRoundWon = currentTurn.transform.Find(currentTurn.name + "Info").Find("RoundWon").position;

        Vector3 positionNotCurrentTurnInfo = notCurrentTurn.transform.Find(notCurrentTurn.name + "Info").position;
        Vector3 positionNotCurrentTurnNick = notCurrentTurn.transform.Find(notCurrentTurn.name + "Info").Find("Nick").position;
        Vector3 positionNotCurrentTurnAvatar = notCurrentTurn.transform.Find(notCurrentTurn.name + "Info").Find("Avatar").position;
        Vector3 positionNotCurrentTurn = notCurrentTurn.transform.Find(notCurrentTurn.name + "Info").Find("Turn").position;
        Vector3 positionNotCurrentTurnCards = notCurrentTurn.transform.Find(notCurrentTurn.name + "Info").Find("Cards").position;
        Vector3 positionNotCurrentTurnWinnerIndicator = notCurrentTurn.transform.Find(notCurrentTurn.name + "Info").Find("WinnerIndicator").position;
        Vector3 positionNotCurrentTurnRoundWon = notCurrentTurn.transform.Find(notCurrentTurn.name + "Info").Find("RoundWon").position;

        currentTurn.transform.Find(currentTurn.name + "Info").position = positionNotCurrentTurnInfo;
        currentTurn.transform.Find(currentTurn.name + "Info").Find("Nick").position = positionNotCurrentTurnNick;
        currentTurn.transform.Find(currentTurn.name + "Info").Find("Avatar").position = positionNotCurrentTurnAvatar;
        currentTurn.transform.Find(currentTurn.name + "Info").Find("Turn").position = positionNotCurrentTurn;
        currentTurn.transform.Find(currentTurn.name + "Info").Find("Cards").position = positionNotCurrentTurnCards;
        currentTurn.transform.Find(currentTurn.name + "Info").Find("WinnerIndicator").position = positionNotCurrentTurnWinnerIndicator;
        currentTurn.transform.Find(currentTurn.name + "Info").Find("RoundWon").position = positionNotCurrentTurnRoundWon;

        notCurrentTurn.transform.Find(notCurrentTurn.name + "Info").position = positionCurrentTurnInfo;
        notCurrentTurn.transform.Find(notCurrentTurn.name + "Info").Find("Nick").position = positionCurrentTurnNick;
        notCurrentTurn.transform.Find(notCurrentTurn.name + "Info").Find("Avatar").position = positionCurrentTurnAvatar;
        notCurrentTurn.transform.Find(notCurrentTurn.name + "Info").Find("Turn").position = positionCurrentTurn;
        notCurrentTurn.transform.Find(notCurrentTurn.name + "Info").Find("Cards").position = positionCurrentTurnCards;
        notCurrentTurn.transform.Find(notCurrentTurn.name + "Info").Find("WinnerIndicator").position = positionCurrentTurnWinnerIndicator;
        notCurrentTurn.transform.Find(notCurrentTurn.name + "Info").Find("RoundWon").position = positionCurrentTurnRoundWon;


    }

    private void TransitionChangeTurn()
    {

    }
}
