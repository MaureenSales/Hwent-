using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UIElements;
public class GameController : MonoBehaviour
{
    public GameObject Message; //Mensaje para actualizar estado del juego
    public GameObject DeckPlayer; //Mazo del objeto jugador en la jerarquía de la escena
    public GameObject DeckEnemy; //Mazo del objeto enemigo en la jerarquía de la escena
    public GameObject GraveyardPlayer;
    public GameObject GraveyardEnemy;
    public GameObject currentTurn; //Jugador actual
    public GameObject notCurrentTurn; //Jugador no actual
    public GameObject CardPrefab; //Prefab de las cartas
    public GameObject Panel; //Panel para no jugar cartas en ese estado del juego
    private GameObject DecoyActive = null; //Señuelo activo en el juego
    public GameObject buttonMainMenu;
    public GameObject ClearImages; //Imagenes de rayos de sol de despeje


    // Start is called before the first frame update
    void Start()
    {
        Context.gameController = this.gameObject;
        currentTurn.GetComponentInChildren<PlayerController>().IsYourTurn = true;

        ClearImages.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        UpdateWinner(currentTurn.transform.Find(currentTurn.name + "Field").GetComponentInChildren<SumTotalPower>().total, notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetComponentInChildren<SumTotalPower>().total);
    }

    /// <summary>
    /// Regresar al Menú Principal
    /// </summary>
    async public void MainMenu()
    {
        if (!(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject is null))
        {
            buttonMainMenu.GetComponent<AudioSource>().Play();
        }
        await Task.Delay(80);
        GameData.Reset();
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Método para verificar si una unidad posee efecto y si es así activarlo
    /// </summary>
    /// <param name="unit">unidad a verificar efecto</param>
    public void Effects(GameObject unit)
    {
        Card unitCard = unit.GetComponent<ThisCard>().thisCard;
        Evaluador evaluador = new Evaluador();
        foreach (var effect in unitCard.Skills)
        {
            switch (effect.Name)
            {
                case "DrawCard":
                    DrawCard();
                    break;
                case "PutBoost":
                    PutBoost();
                    break;
                case "PutWeather":
                    PutWeather();
                    break;
                case "PowerfulCard":
                    PowerfulCard(unit);
                    break;
                case "LessPowerCard":
                    LessPowerCard();
                    break;
                case "Average":
                    Average(unitCard, unit);
                    break;
                case "ClearRow":
                    ClearRow();
                    break;
                case "MultiplyPower":
                    MultiplyPower(unit, unitCard);
                    break;

                default:
                    try
                    {
                        if (!(effect.Selector is null))
                        {
                            evaluador.evaluate(effect.Selector);
                            Debug.Log(evaluador.SelectorsList.Count);
                        }
                        evaluador.Evaluate(Global.EffectsCreated[effect.Name], effect, evaluador.SelectorsList);
                    }
                    catch (System.Exception e)
                    {
                        ShowMessage("Error en el efecto");
                        Debug.Log(e.ToString());
                        Debug.Log(e.StackTrace);
                    }
                    break;
            }
        }
    }

    async private void DrawCard()
    {
        bool destroy = false;

        if (!(currentTurn.transform.Find(currentTurn.name + "Board").Find("Hand").childCount < 11))
        {
            destroy = true;
        }

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

        if (destroy)
        {
            GameObject toDestroy = currentTurn.transform.Find(currentTurn.name + "Board").Find("Hand").GetChild(currentTurn.transform.Find(currentTurn.name + "Board").Find("Hand").childCount - 1).gameObject;
            LeanTweenMoveGraveyard(toDestroy, currentTurn.transform.Find("Graveyard").transform);
        }
    }

    private void PutBoost()
    {
        List<Card> cards;
        Boost boost = null;

        cards = currentTurn.GetComponent<Player>().MyDeck.cards;

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
                if (currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetComponentInChildren<Row>().unitObjects.Count > countUnit && currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(2).childCount == 0)
                {
                    indexRow = i;
                    countUnit = currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetComponentInChildren<Row>().unitObjects.Count;
                }
            }

            GameObject boostCard = Instantiate(CardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            boostCard.GetComponent<ThisCard>().PrintCard(boost);
            boostCard.transform.SetParent(currentTurn.transform.Find(currentTurn.name + "Field").GetChild(indexRow).GetChild(2), false);
            boostCard.GetComponent<Drag>().enabled = false;
            List<GameObject> units = new();
            for (int i = 0; i < currentTurn.transform.Find(currentTurn.name + "Field").GetChild(indexRow).GetComponentInChildren<Row>().unitObjects.Count; i++)
            {
                if (currentTurn.transform.Find(currentTurn.name + "Field").GetChild(indexRow).GetComponentInChildren<Row>().unitObjects[i].transform.parent.name != currentTurn.name + "Board")
                {
                    units.Add(currentTurn.transform.Find(currentTurn.name + "Field").GetChild(indexRow).GetComponentInChildren<Row>().unitObjects[i]);
                }
            }

            ImproveUnits(units, boostCard.transform.parent.parent);
            currentTurn.GetComponent<Player>().MyDeck.cards.Remove(boost);
        }
    }

    private void PutWeather()
    {
        List<Card> cards;
        Weather weather = null;
        cards = currentTurn.GetComponent<Player>().MyDeck.cards;

        bool putWeather = false;
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] is Weather)
            {
                weather = (Weather)cards[i];
                switch (weather.Skills[0].Name)
                {
                    case "WeatherMelee":
                        if (!GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[0])
                        {
                            putWeather = true;
                            GameObject weatherCard = Instantiate(CardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                            weatherCard.GetComponent<ThisCard>().PrintCard(weather);
                            weatherCard.GetComponent<Drag>().enabled = false;
                            weatherCard.transform.SetParent(GameObject.Find("WeatherZone").transform.GetChild(0));
                            weatherCard.transform.localScale = new Vector3(0.8f, 0.8f, 0f);
                            GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[0] = true;
                            GameObject.Find("WeatherZone").GetComponent<WeatherController>().ApplyWeather("Frost");
                        }
                        break;
                    case "WeatherRanged":
                        if (!GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[1])
                        {
                            putWeather = true;
                            GameObject weatherCard = Instantiate(CardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                            weatherCard.GetComponent<ThisCard>().PrintCard(weather);
                            weatherCard.GetComponent<Drag>().enabled = false;
                            weatherCard.transform.SetParent(GameObject.Find("WeatherZone").transform.GetChild(1));
                            weatherCard.transform.localScale = new Vector3(0.8f, 0.8f, 0f);
                            GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[1] = true;
                            GameObject.Find("WeatherZone").GetComponent<WeatherController>().ApplyWeather("Fog");
                        }
                        break;
                    case "WeatherSiege":
                        if (!GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[2])
                        {
                            putWeather = true;
                            GameObject weatherCard = Instantiate(CardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                            weatherCard.GetComponent<ThisCard>().PrintCard(weather);
                            weatherCard.GetComponent<Drag>().enabled = false;
                            weatherCard.transform.SetParent(GameObject.Find("WeatherZone").transform.GetChild(2));
                            weatherCard.transform.localScale = new Vector3(0.8f, 0.8f, 0f);
                            GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[2] = true;
                            GameObject.Find("WeatherZone").GetComponent<WeatherController>().ApplyWeather("Rain");
                        }
                        break;
                }
                if (putWeather)
                {
                    if (weather != null) currentTurn.GetComponent<Player>().MyDeck.cards.Remove(weather);
                    break;

                }
            }
        }
    }

    async private void PowerfulCard(GameObject unit)
    {
        int maxPower = int.MinValue;
        int indexRow = -1;
        int indexInRowZone = -1;
        GameObject owner = null;
        bool thereCards = false;

        for (int i = 1; i < currentTurn.transform.Find(currentTurn.name + "Field").childCount; i++)
        {
            for (int j = 0; j < currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).childCount; j++)
            {
                thereCards = true;
                if (int.Parse(currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().powerText.text) > maxPower)
                {
                    maxPower = int.Parse(currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().powerText.text);
                    indexRow = i;
                    indexInRowZone = j;
                    owner = currentTurn;
                }


            }
        }

        for (int i = 1; i < notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").childCount; i++)
        {
            for (int j = 0; j < notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).childCount; j++)
            {
                thereCards = true;
                if (int.Parse(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().powerText.text) > maxPower)
                {
                    maxPower = int.Parse(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().powerText.text);
                    indexRow = i;
                    indexInRowZone = j;
                    owner = notCurrentTurn;
                }

            }
        }
        if (indexRow != -1)
        {

            if (!(owner.transform.Find(owner.name + "Field").GetChild(indexRow).GetChild(0).GetChild(indexInRowZone).GetComponent<ThisCard>().thisCard is HeroUnit))
            {
                if (int.Parse(owner.transform.Find(owner.name + "Field").GetChild(indexRow).GetChild(0).GetChild(indexInRowZone).GetComponent<ThisCard>().powerText.text) > int.Parse(unit.GetComponent<ThisCard>().powerText.text))
                {
                    owner.transform.Find(owner.name + "Field").GetChild(indexRow).GetChild(0).GetComponent<Row>().RemoveFromRow(owner.transform.Find(owner.name + "Field").GetChild(indexRow).GetChild(0).GetChild(indexInRowZone).gameObject);
                    GameObject toDestroy = owner.transform.Find(owner.name + "Field").GetChild(indexRow).GetChild(0).GetChild(indexInRowZone).gameObject;
                    LeanTweenMoveGraveyard(toDestroy, owner.transform.Find("Graveyard").transform);
                }
                else
                {
                    unit.GetComponent<Drag>().parentToReturnTo.GetComponent<Row>().RemoveFromRow(unit);
                    LeanTweenMoveGraveyard(unit, currentTurn.transform.Find("Graveyard").transform);
                }

            }

        }
        else if (!thereCards)
        {
            unit.GetComponent<Drag>().parentToReturnTo.GetComponent<Row>().RemoveFromRow(unit);
            await Task.Delay(800);
            LeanTweenMoveGraveyard(unit, currentTurn.transform.Find("Graveyard").transform);
        }
    }

    private void LessPowerCard()
    {
        int minPower = int.MaxValue;
        int indexRow = -1;
        int indexInRowZone = -1;
        for (int i = 1; i < notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").childCount; i++)
        {
            for (int j = 0; j < notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).childCount; j++)
            {
                if (int.Parse(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().powerText.text) < minPower)
                {
                    minPower = int.Parse(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().powerText.text);
                    indexRow = i;
                    indexInRowZone = j;
                }

            }
        }

        if (indexRow != -1 && !(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).GetChild(0).GetChild(indexInRowZone).GetComponent<ThisCard>().thisCard is HeroUnit))
        {
            //->
            notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).GetChild(0).GetComponent<Row>().RemoveFromRow(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).GetChild(0).GetChild(indexInRowZone).gameObject);
            GameObject toDestroy = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).GetChild(0).GetChild(indexInRowZone).gameObject;
            LeanTweenMoveGraveyard(toDestroy, notCurrentTurn.transform.Find("Graveyard").transform);
        }
    }

    private void Average(Card unitCard, GameObject unit)
    {
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
       
        for (int i = 1; i < currentTurn.transform.Find(currentTurn.name + "Field").childCount; i++)
        {
            for (int j = 0; j < currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).childCount; j++)
            {
                if (!(currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().thisCard is HeroUnit) && !(currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().thisCard is DecoyUnit))
                {
                    currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().powerText.text = ApproximateAverage.ToString();
                }
            }
            if (currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(2).childCount != 0)
            {
                ImproveUnits(currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetComponentInChildren<Row>().unitObjects, currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).transform);
            }
        }


        for (int i = 1; i < notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").childCount; i++)
        {
            for (int j = 0; j < notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).childCount; j++)
            {
                if (!(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().thisCard is HeroUnit) && !(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().thisCard is DecoyUnit))
                {
                    notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().powerText.text = ApproximateAverage.ToString();
                }

            }
            if (notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(2).childCount != 0)
            {
                ImproveUnits(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetComponentInChildren<Row>().unitObjects, notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).transform);
            }
        }

        if (unitCard is Unit)
        {
            unit.GetComponent<ThisCard>().powerText.text = ApproximateAverage.ToString();
            switch (unit.GetComponent<Drag>().parentToReturnTo.name)
            {
                case "MeleeZone": Improve(unit, "Melee"); break;
                case "RangedZone": Improve(unit, "Ranged"); break;
                case "SiegeZone": Improve(unit, "Siege"); break;
            }
        }
    }

    async private void ClearRow()
    {
        int indexRow = -1;
        int countUnit = 14;
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
            int n = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).GetComponentInChildren<Row>().unitObjects.Count;
            for (int i = 0; i < n; i++)
            {
                if (!(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).GetChild(0).GetChild(i).GetComponent<ThisCard>().thisCard is HeroUnit))
                {
                    GameObject toDestroy = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).GetChild(0).GetChild(i).gameObject;
                    notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(indexRow).GetChild(0).GetComponent<Row>().RemoveFromRow(toDestroy);
                    toDestroy.GetComponent<ThisCard>().borderFire.gameObject.SetActive(true);
                    await Task.Delay(200);
                    LeanTweenMoveGraveyard(toDestroy, notCurrentTurn.transform.Find("Graveyard").transform);
                }
            }
        }
    }
    private void MultiplyPower(GameObject unit, Card unitCard)
    {
        int count = 1;
        for (int i = 1; i < currentTurn.transform.Find(currentTurn.name + "Field").childCount; i++)
        {
            for (int j = 0; j < currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).childCount; j++)
            {
                if (currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().thisCard.Equals(unitCard))
                {
                    count++;
                }
            }
        }

        if (count != 1)
        {
            unit.GetComponent<ThisCard>().powerText.text = (int.Parse(unit.GetComponent<ThisCard>().powerText.text) * count).ToString();
        }
    }

    /// <summary>
    /// Método para la segunda variante del efecto limpiar una fila del rival
    /// </summary>
    /// <param name="units">lista de unidades de la fila</param>
    /// <returns>si posee al menos una unidad de plata</returns>
    private bool UnitSilverInRow(List<UnitCard> units)
    {
        for (int i = 0; i < units.Count; i++)
        {
            if (units[i] is Unit || units[i] is DecoyUnit)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Método para activar efecto de la facción Gryffindor
    /// </summary>
    public void GryffindorEffect()
    {
        int countMelee = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("MeleeRow").Find("MeleeZone").childCount;
        if (countMelee > 0)
        {
            for (int i = 0; i < currentTurn.transform.Find(currentTurn.name + "Field").Find("MeleeRow").Find("MeleeZone").childCount; i++)
            {
                if (currentTurn.transform.Find(currentTurn.name + "Field").Find("MeleeRow").Find("MeleeZone").GetChild(i).GetComponent<ThisCard>().thisCard is Unit)
                {
                    currentTurn.transform.Find(currentTurn.name + "Field").Find("MeleeRow").Find("MeleeZone").GetChild(i).GetComponent<ThisCard>().powerText.text = (int.Parse(currentTurn.transform.Find(currentTurn.name + "Field").Find("MeleeRow").Find("MeleeZone").GetChild(i).GetComponent<ThisCard>().powerText.text) + countMelee).ToString();
                }
            }

        }
    }

    /// <summary>
    /// Método para activar efecto de la facción Slytherin
    /// </summary>
    public void SlytherinEffect()
    {
        currentTurn.transform.Find("Deck").GetComponent<Draw>().DrawCard();
        currentTurn.transform.Find("Deck").GetComponent<Draw>().DrawCard();
    }

    /// <summary>
    /// Método para verificar si hay aumento en la fila donde es soltada la unidad
    /// </summary>
    /// <param name="eventData">unidad soltada</param>
    /// <param name="zone">nombre de la zona de ataque</param>
    public void Improve(GameObject eventData, string zone)
    {
        GameObject boost = GameObject.Find(currentTurn.name + "Field").transform.Find(zone + "Row").Find("Boost" + zone).gameObject;
        if (boost.transform.childCount != 0)
        {
            ImproveUnits(eventData.gameObject);
        }
    }

    /// <summary>
    /// Método para bonificar a la unidad soltada en una fila donde hay aumento
    /// </summary>
    /// <param name="unit">unidad a bonificar</param>
    /// <param name="zone">zona de ataque donde fue soltada</param>
    public void ImproveUnits(GameObject unit)
    {
        int newPower = int.Parse(unit.GetComponent<ThisCard>().powerText.text) + 2;
        unit.GetComponent<ThisCard>().powerText.text = newPower.ToString();
    }

    /// <summary>
    /// Método para bonificar a una lista de unidades al colocar un aumento
    /// </summary>
    /// <param name="units">lista de unidades a bonificar</param>
    public void ImproveUnits(List<GameObject> units, Transform row)
    {
        foreach (var unit in units)
        {
            if (unit.GetComponent<ThisCard>().thisCard is Unit && unit.transform.parent.parent == row)
            {
                int newPower = int.Parse(unit.GetComponent<ThisCard>().powerText.text) + 2;
                unit.GetComponent<ThisCard>().powerText.text = newPower.ToString();
            }
        }
    }

    /// <summary>
    /// Método para iluminar cartas del campo que se pueden cambiar con el señuelo
    /// </summary>
    /// <param name="decoyActive">objeto causante del evento (señuelo activo)</param>
    public void DecoyFirstPart(GameObject decoyActive)
    {
        DecoyActive = decoyActive;
        bool thereCards = false;
        for (int i = 1; i < currentTurn.transform.Find(currentTurn.name + "Field").childCount; i++)
        {
            for (int j = 0; j < currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).childCount; j++)
            {
                if (currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().thisCard is Unit)
                {
                    thereCards = true;
                    currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().borderLight.gameObject.SetActive(true);
                }
            }
        }

        if (!thereCards)
        {
            Panel.SetActive(false);
        }
    }

    /// <summary>
    /// Método para intercambiar el señuelo activo con la unidad seleccionada del campo
    /// </summary>
    /// <param name="unit">unidad seleccionada</param>
    public void DecoySecondPart(GameObject unit)
    {
        for (int i = 1; i < currentTurn.transform.Find(currentTurn.name + "Field").childCount; i++)
        {
            for (int j = 0; j < currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).childCount; j++)
            {

                if (currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().thisCard is Unit)
                {
                    currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).GetComponent<ThisCard>().borderLight.gameObject.SetActive(false);
                }
            }
        }
        unit.GetComponent<Drag>().enabled = true;
        DecoyActive.GetComponent<Drag>().enabled = false;
        Vector3 positionUnit = unit.transform.position;
        Transform parentUnit = unit.transform.parent;
        Vector3 positionDecoy = DecoyActive.transform.position;
        Transform parentDecoy = DecoyActive.transform.parent;
        unit.transform.position = positionDecoy;
        unit.transform.SetParent(parentDecoy);
        DecoyActive.transform.position = positionUnit;
        DecoyActive.transform.SetParent(parentUnit);
        currentTurn.GetComponent<Player>().MyHand.CardsObject.Remove(DecoyActive);
        currentTurn.GetComponent<Player>().MyHand.Cards.Remove(DecoyActive.GetComponent<ThisCard>().thisCard);
        currentTurn.GetComponent<Player>().MyHand.CardsObject.Add(unit);
        currentTurn.GetComponent<Player>().MyHand.Cards.Add(unit.GetComponent<ThisCard>().thisCard);
        unit.transform.GetComponent<ThisCard>().powerText.text = unit.transform.GetComponent<ThisCard>().power.ToString();
        parentUnit.GetComponent<Row>().RemoveFromRow(unit);
        parentUnit.GetComponent<Row>().AddToRow(DecoyActive);
    }

    /// <summary>
    /// Método para limpiar la zona de clima
    /// </summary>
    public void ClearWeatherZone(int start, int end)
    {
        for (int i = start; i < end; i++)
        {
            if (GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[i] == true)
            {
                GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[i] = false;
                GameObject toDestroy = GameObject.Find("WeatherZone").transform.GetChild(i).GetChild(0).gameObject;
                LeanTweenMoveGraveyard(toDestroy, currentTurn.transform.Find("Graveyard").transform);
                switch (i)
                {
                    case 0:
                        GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherImagesPlayer[0].SetActive(false);
                        GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherImagesEnemy[0].SetActive(false);
                        ClearPower(currentTurn.transform.Find(currentTurn.name + "Field").Find("MeleeRow").GetComponentInChildren<Row>().unitObjects);
                        ClearPower(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("MeleeRow").GetComponentInChildren<Row>().unitObjects);
                        if (currentTurn.transform.Find(currentTurn.name + "Field").Find("MeleeRow").GetChild(2).childCount > 0)
                        {
                            ImproveUnits(currentTurn.transform.Find(currentTurn.name + "Field").Find("MeleeRow").GetComponentInChildren<Row>().unitObjects, currentTurn.transform.Find(currentTurn.name + "Field").Find("MeleeRow").transform);
                        }
                        if (notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("MeleeRow").GetChild(2).childCount > 0)
                        {
                            ImproveUnits(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("MeleeRow").GetComponentInChildren<Row>().unitObjects, notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("MeleeRow").transform);
                        }
                        break;
                    case 1:
                        GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherImagesPlayer[1].SetActive(false);
                        GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherImagesEnemy[1].SetActive(false);
                        ClearPower(currentTurn.transform.Find(currentTurn.name + "Field").Find("RangedRow").GetComponentInChildren<Row>().unitObjects);
                        ClearPower(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("RangedRow").GetComponentInChildren<Row>().unitObjects);
                        if (currentTurn.transform.Find(currentTurn.name + "Field").Find("RangedRow").GetChild(2).childCount > 0)
                        {
                            ImproveUnits(currentTurn.transform.Find(currentTurn.name + "Field").Find("RangedRow").GetComponentInChildren<Row>().unitObjects, currentTurn.transform.Find(currentTurn.name + "Field").Find("RangedRow").transform);
                        }
                        if (notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("RangedRow").GetChild(2).childCount > 0)
                        {
                            ImproveUnits(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("RangedRow").GetComponentInChildren<Row>().unitObjects, notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("RangedRow").transform);
                        }
                        break;
                    case 2:
                        GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherImagesPlayer[2].SetActive(false);
                        GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherImagesEnemy[2].SetActive(false);
                        ClearPower(currentTurn.transform.Find(currentTurn.name + "Field").Find("SiegeRow").GetComponentInChildren<Row>().unitObjects);
                        ClearPower(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("SiegeRow").GetComponentInChildren<Row>().unitObjects);
                        if (currentTurn.transform.Find(currentTurn.name + "Field").Find("SiegeRow").GetChild(2).childCount > 0)
                        {
                            ImproveUnits(currentTurn.transform.Find(currentTurn.name + "Field").Find("SiegeRow").GetComponentInChildren<Row>().unitObjects, currentTurn.transform.Find(currentTurn.name + "Field").Find("SiegeRow").transform);
                        }
                        if (notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("SiegeRow").GetChild(2).childCount > 0)
                        {
                            ImproveUnits(notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("SiegeRow").GetComponentInChildren<Row>().unitObjects, notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("SiegeRow").transform);
                        }
                        break;
                }
            }

        }
    }

    /// <summary>
    /// Método para limpiar poder al despejar climas
    /// </summary>
    /// <param name="units"> lista de unidades de una fila</param>
    /// <param name="owner">jugador al que pertenece la fila</param>
    /// <param name="zone">zona de ataque</param>
    private void ClearPower(List<GameObject> units)
    {
        for (int i = 0; i < units.Count; i++)
        {
            units[i].GetComponent<ThisCard>().powerText.text = units[i].GetComponent<ThisCard>().power.ToString();
        }
    }

    /// <summary>
    /// Método para actualizar el ganador actual
    /// </summary>
    /// <param name="powerCurrentTurn">poder total en campo del jugador actual</param>
    /// <param name="powerNotCurrentTurn">poder total en campo del jugador no actual</param>
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

    /// <summary>
    /// Método para finalizar turno
    /// </summary>
    public void FinalizedTurn()
    {
        StartCoroutine(ChangeTurn());

    }

    /// <summary>
    /// Método para mostrar mensaje de estado del juego
    /// </summary>
    /// <param name="message">estado actual del juego</param>
    private void ShowMessage(string message)
    {
        Message.transform.localScale = new Vector3(0f, 0f, 0f);
        Message.gameObject.SetActive(true);
        LeanTween.scale(Message, new Vector3(1f, 1f, 1f), 0.3f);
        Message.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = message;
    }

    /// <summary>
    /// Método para robar cartas al inicio de ronda
    /// </summary>
    private void DrawCardsStartRound()
    {
        DeckPlayer.GetComponent<Draw>().DrawCard();
        DeckPlayer.GetComponent<Draw>().DrawCard();
        DeckEnemy.GetComponent<Draw>().DrawCard();
        DeckEnemy.GetComponent<Draw>().DrawCard();
    }

    /// <summary>
    /// Método para limpiar la información de los jugadores al terminar el juego
    /// </summary>
    private void ClearInfoPlayers()
    {
        notCurrentTurn.GetComponentInChildren<PlayerController>().Pass = false;
        currentTurn.GetComponentInChildren<PlayerController>().Pass = false;
        currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.SetActive(false);
        currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.SetActive(false);
        notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.SetActive(false);
        notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.SetActive(false);
    }

    /// <summary>
    /// Corutina para cambiar turno
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeTurn()
    {
        yield return new WaitForSeconds(2f);
        yield return new WaitForEndOfFrame();

        if (currentTurn.transform.GetComponentInChildren<PlayerController>().Pass && !notCurrentTurn.transform.GetComponentInChildren<PlayerController>().Pass)
        {
            ShowMessage(currentTurn.GetComponentInChildren<PlayerController>().Nick + " ha pasado turno");
            yield return new WaitForSeconds(1.2f);
            Message.gameObject.SetActive(false);
        }
        else if (currentTurn.transform.GetComponentInChildren<PlayerController>().Pass && notCurrentTurn.transform.GetComponentInChildren<PlayerController>().Pass)
        {
            ShowMessage(currentTurn.GetComponentInChildren<PlayerController>().Nick + " ha pasado turno");
            yield return new WaitForSeconds(1.2f);
            Message.gameObject.SetActive(false);

            if (currentTurn.GetComponentInChildren<PlayerController>().WinnerIndicator.activeSelf)
            {
                ShowMessage(currentTurn.GetComponentInChildren<PlayerController>().Nick + " ha ganado la ronda");
                yield return new WaitForSeconds(1.2f);
                Message.gameObject.SetActive(false);

                if (!currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.activeSelf)
                {
                    currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.SetActive(true);

                }
                else if (!currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.activeSelf)
                {
                    currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.SetActive(true);

                    ShowMessage(currentTurn.GetComponentInChildren<PlayerController>().Nick + " ha ganado el juego");
                    yield return new WaitForSeconds(1.2f);
                    Message.gameObject.SetActive(false);
                    ClearField();
                    ClearInfoPlayers();
                    yield return new WaitForSeconds(2f);
                    MainMenu();
                    yield break;
                }

                ClearField();
                notCurrentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                currentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                ShowMessage("Turno de " + currentTurn.GetComponentInChildren<PlayerController>().Nick);
                yield return new WaitForSeconds(1.2f);
                Message.gameObject.SetActive(false);
                DrawCardsStartRound();
                yield break;

            }
            else if (notCurrentTurn.GetComponentInChildren<PlayerController>().WinnerIndicator.activeSelf)
            {
                ShowMessage(notCurrentTurn.GetComponentInChildren<PlayerController>().Nick + " ha ganado la ronda");
                yield return new WaitForSeconds(1.2f);
                Message.gameObject.SetActive(false);
                if (!notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.activeSelf)
                {
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (!notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.activeSelf)
                {
                    notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.SetActive(true);

                    ShowMessage(notCurrentTurn.GetComponentInChildren<PlayerController>().Nick + " ha ganado el juego");
                    yield return new WaitForSeconds(1.2f);
                    Message.gameObject.SetActive(false);
                    ClearField();
                    ClearInfoPlayers();
                    yield return new WaitForSeconds(2f);
                    MainMenu();
                    yield break;

                }

                ClearField();
                notCurrentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                currentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                DrawCardsStartRound();

            }
            else
            {
                ShowMessage("Ha ocurrido un empate");
                yield return new WaitForSeconds(1.2f);
                Message.gameObject.SetActive(false);
                if (currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.activeSelf &&
                notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.activeSelf)
                {
                    ShowMessage(currentTurn.GetComponentInChildren<PlayerController>().Nick + " y " + notCurrentTurn.GetComponentInChildren<PlayerController>().Nick + " han ganado el juego");
                    yield return new WaitForSeconds(1.2f);
                    Message.gameObject.SetActive(false);
                    ClearField();
                    ClearInfoPlayers();
                    yield return new WaitForSeconds(2f);
                    MainMenu();
                    yield break;
                }
                else
                {
                    if (!currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.activeSelf)
                    {
                        currentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    else
                    {
                        currentTurn.GetComponentInChildren<PlayerController>().transform.GetChild(1).gameObject.SetActive(true);
                        ShowMessage(currentTurn.GetComponentInChildren<PlayerController>().Nick + " ha ganado el juego");
                        yield return new WaitForSeconds(1.2f);
                        Message.gameObject.SetActive(false);
                        ClearField();
                        ClearInfoPlayers();
                        yield return new WaitForSeconds(2f);
                        MainMenu();
                        yield break;
                    }

                    if (!notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.activeSelf)
                    {
                        notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    else
                    {
                        notCurrentTurn.GetComponentInChildren<PlayerController>().Gems.transform.GetChild(1).gameObject.SetActive(true);

                        ShowMessage(notCurrentTurn.GetComponentInChildren<PlayerController>().Nick + " ha ganado el juego");

                        ClearField();
                        ClearInfoPlayers();
                        yield return new WaitForSeconds(2f);
                        MainMenu();
                        yield break;
                    }

                }
                ClearField();
                notCurrentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                currentTurn.GetComponentInChildren<PlayerController>().Pass = false;
                ShowMessage("Turno de " + currentTurn.GetComponentInChildren<PlayerController>().Nick);
                yield return new WaitForSeconds(1.2f);
                Message.gameObject.SetActive(false);
                DrawCardsStartRound();
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
            ShowMessage("Turno de " + currentTurn.GetComponentInChildren<PlayerController>().Nick);
            yield return new WaitForSeconds(0.5f);
            SwapObjects();
            yield return new WaitForSeconds(2f);
            Message.gameObject.SetActive(false);
        }
    }

    private void LeanTweenMoveGraveyard(GameObject toDestroy, Transform graveyard)
    {
        if (toDestroy.GetComponent<ThisCard>().thisCard is UnitCard unit)
        {
            toDestroy.GetComponent<ThisCard>().power = unit.Power.ToString();
            toDestroy.GetComponent<ThisCard>().powerText.text = unit.Power.ToString();
        }
        graveyard.GetComponent<Graveyard>().CardsObject.Add(toDestroy);
        graveyard.GetComponent<Graveyard>().Cards.Add(toDestroy.GetComponent<ThisCard>().thisCard);
        LeanTween.move(toDestroy, graveyard.position, 1f).setOnComplete(() => toDestroy.transform.SetParent(graveyard));
    }

    /// <summary>
    /// Método para limpiar el tablero
    /// </summary>
    private void ClearField()
    {
        currentTurn.transform.Find(currentTurn.name + "Field").GetComponentInChildren<SumTotalPower>().total = 0;
        currentTurn.transform.Find(currentTurn.name + "Field").GetComponentInChildren<SumTotalPower>().totalText.text = "0";

        for (int i = 1; i < 4; i++)
        {
            for (int j = currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).childCount - 1; j >= 0; j--)
            {
                GameObject toDestroy = currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).gameObject;
                LeanTweenMoveGraveyard(toDestroy, currentTurn.transform.Find("Graveyard").transform);
            }
            currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetComponentInChildren<Row>().Clear();
            currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetComponentInChildren<SumPower>().power = 0;
            currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetComponentInChildren<SumPower>().powerText.text = "0";
            if (currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(2).childCount > 0)
            {
                GameObject toDestroy = currentTurn.transform.Find(currentTurn.name + "Field").GetChild(i).GetChild(2).GetChild(0).gameObject;
                LeanTweenMoveGraveyard(toDestroy, currentTurn.transform.Find("Graveyard").transform);
            }

        }

        notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetComponentInChildren<SumTotalPower>().total = 0;
        notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetComponentInChildren<SumTotalPower>().totalText.text = "0";

        for (int i = 1; i < 4; i++)
        {
            for (int j = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).childCount - 1; j >= 0; j--)
            {
                GameObject toDestroy = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(0).GetChild(j).gameObject;
                LeanTweenMoveGraveyard(toDestroy, notCurrentTurn.transform.Find("Graveyard").transform);
            }
            notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetComponentInChildren<Row>().Clear();
            notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetComponentInChildren<SumPower>().power = 0;
            notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetComponentInChildren<SumPower>().powerText.text = "0";
            if (notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(2).childCount > 0)
            {
                GameObject toDestroy = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").GetChild(i).GetChild(2).GetChild(0).gameObject;
                LeanTweenMoveGraveyard(toDestroy, notCurrentTurn.transform.Find("Graveyard").transform);
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
                LeanTweenMoveGraveyard(toDestroy, currentTurn.transform.Find("Graveyard").transform);
            }
        }


    }

    /// <summary>
    /// Método para intercambiar la posición de los jugadores
    /// </summary>
    private void SwapObjects()
    {
        Vector3 positionCurrentTurnSumTotalPower = currentTurn.transform.Find(currentTurn.name + "Field").Find("SumTotalPower").position;
        Vector3 positionCurrentTurnMeleeRow = currentTurn.transform.Find(currentTurn.name + "Field").Find("MeleeRow").position;
        Vector3 positionCurrentTurnFrost = currentTurn.transform.Find(currentTurn.name + "Field").Find("MeleeRow").Find("Frost").position;
        Vector3 positionCurrentTurnRangedRow = currentTurn.transform.Find(currentTurn.name + "Field").Find("RangedRow").position;
        Vector3 positionCurrentTurnFog = currentTurn.transform.Find(currentTurn.name + "Field").Find("RangedRow").Find("Fog").position;
        Vector3 positionCurrentTurnSiegeRow = currentTurn.transform.Find(currentTurn.name + "Field").Find("SiegeRow").position;
        Vector3 positionCurrentTurnRain = currentTurn.transform.Find(currentTurn.name + "Field").Find("SiegeRow").Find("Rain").position;
        Vector3 positionCurrentTurnLeader = currentTurn.transform.Find("Leader").transform.position;
        Vector3 positionCurrentTurnDeck = currentTurn.transform.Find("Deck").transform.position;
        Vector3 positionCurrentTurnGraveyard = currentTurn.transform.Find("Graveyard").transform.position;


        Vector3 positionNotCurrentTurnSumTotalPower = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("SumTotalPower").position;
        Vector3 positionNotCurrentTurnMeleeRow = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("MeleeRow").position;
        Vector3 positionNotCurrentTurnFrost = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("MeleeRow").Find("Frost").position;
        Vector3 positionNotCurrentTurnRangedRow = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("RangedRow").position;
        Vector3 positionNotCurrentTurnFog = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("RangedRow").Find("Fog").position;
        Vector3 positionNotCurrentTurnSiegeRow = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("SiegeRow").position;
        Vector3 positionNotCurrentTurnRain = notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("SiegeRow").Find("Rain").position;
        Vector3 positionNotCurrentTurnLeader = notCurrentTurn.transform.Find("Leader").transform.position;
        Vector3 positionNotCurrentTurDeck = notCurrentTurn.transform.Find("Deck").transform.position;
        Vector3 positionNotCurrentTurnGraveyard = notCurrentTurn.transform.Find("Graveyard").transform.position;


        currentTurn.transform.Find(currentTurn.name + "Field").Find("SumTotalPower").position = positionNotCurrentTurnSumTotalPower;
        currentTurn.transform.Find(currentTurn.name + "Field").Find("MeleeRow").position = positionNotCurrentTurnMeleeRow;
        currentTurn.transform.Find(currentTurn.name + "Field").Find("MeleeRow").Find("Frost").position = positionNotCurrentTurnFrost;
        currentTurn.transform.Find(currentTurn.name + "Field").Find("RangedRow").position = positionNotCurrentTurnRangedRow;
        currentTurn.transform.Find(currentTurn.name + "Field").Find("RangedRow").Find("Fog").position = positionNotCurrentTurnFog;
        currentTurn.transform.Find(currentTurn.name + "Field").Find("SiegeRow").position = positionNotCurrentTurnSiegeRow;
        currentTurn.transform.Find(currentTurn.name + "Field").Find("SiegeRow").Find("Rain").position = positionNotCurrentTurnRain;
        currentTurn.transform.Find("Leader").transform.position = positionNotCurrentTurnLeader;
        currentTurn.transform.Find("Deck").transform.position = positionNotCurrentTurDeck;
        currentTurn.transform.Find("Graveyard").transform.position = positionNotCurrentTurnGraveyard;

        notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("SumTotalPower").position = positionCurrentTurnSumTotalPower;
        notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("MeleeRow").position = positionCurrentTurnMeleeRow;
        notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("MeleeRow").Find("Frost").position = positionCurrentTurnFrost;
        notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("RangedRow").position = positionCurrentTurnRangedRow;
        notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("RangedRow").Find("Fog").position = positionCurrentTurnFog;
        notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("SiegeRow").position = positionCurrentTurnSiegeRow;
        notCurrentTurn.transform.Find(notCurrentTurn.name + "Field").Find("SiegeRow").Find("Rain").position = positionCurrentTurnRain;
        notCurrentTurn.transform.Find("Leader").transform.position = positionCurrentTurnLeader;
        notCurrentTurn.transform.Find("Deck").transform.position = positionCurrentTurnDeck;
        notCurrentTurn.transform.Find("Graveyard").transform.position = positionCurrentTurnGraveyard;

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
}
