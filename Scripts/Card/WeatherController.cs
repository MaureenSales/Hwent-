using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    //public Transform grid;
    List<GameObject> WeatherImagesPlayer = new List<GameObject>();
    List<GameObject> WeatherImagesEnemy = new List<GameObject>();
    [HideInInspector] public bool[] weather = { false, false, false };

    public void Start()
    {
        WeatherImagesPlayer.Add(GameObject.Find("PlayerField").transform.Find("MeleeRow").Find("Frost").gameObject);
        WeatherImagesPlayer.Add(GameObject.Find("PlayerField").transform.Find("RangedRow").Find("Fog").gameObject);
        WeatherImagesPlayer.Add(GameObject.Find("PlayerField").transform.Find("SiegeRow").Find("Rain").gameObject);

        WeatherImagesEnemy.Add(GameObject.Find("EnemyField").transform.Find("MeleeRow").Find("Frost").gameObject);
        WeatherImagesEnemy.Add(GameObject.Find("EnemyField").transform.Find("RangedRow").Find("Fog").gameObject);
        WeatherImagesEnemy.Add(GameObject.Find("EnemyField").transform.Find("SiegeRow").Find("Rain").gameObject);

        foreach (var image in WeatherImagesPlayer)
        {
            image.SetActive(false);
        }

        foreach (var image in WeatherImagesEnemy)
        {
            image.SetActive(false);
        }
    }
    public void ApplyWeather(string weatherType)
    {
        Debug.Log("EnterApplyWeather");
        foreach (var image in WeatherImagesPlayer)
        {
            if(image.name == weatherType)
            {
                image.SetActive(true);
            }
        }
        foreach (var image in WeatherImagesEnemy)
        {
            if(image.name == weatherType)
            {
                image.SetActive(true);
            }
        }
    }

    public void WeatherEffect(GameObject unit, Transform unitTransform)
    {
        Debug.Log("EnterWeatherEffect");
        Debug.Log(weather[0] + " " + weather[1] + " " + weather[2]);

            if (weather[0] && unitTransform.name == "MeleeZone")
            {
                Debug.Log("EnterWeatherFrost");
                Debug.Log(unit.GetComponent<ThisCard>().cardName);
                int newPower = 2;
                unit.GetComponent<ThisCard>().powerText.text = newPower.ToString();
                unit.transform.parent.parent.GetComponentInChildren<SumPower>().UpdatePower();
            }
            else if (weather[1] && unitTransform.name == "RangedZone")
            {
                int newPower = 2;
                unit.GetComponent<ThisCard>().powerText.text = newPower.ToString();
                unit.transform.parent.parent.GetComponentInChildren<SumPower>().UpdatePower();
            }
            else if (weather[2] && unitTransform.name == "SiegeZone")
            {
                int newPower = 2;
                unit.GetComponent<ThisCard>().powerText.text = newPower.ToString();
                unit.transform.parent.parent.GetComponentInChildren<SumPower>().UpdatePower();
            }

    }

    public void WeatherEffect(List<GameObject> units)
    {
        foreach (var unit in units)
        {
            if (!(unit.GetComponent<ThisCard>().thisCard is HeroUnit))
            {
                int newPower = 2;
                unit.GetComponent<ThisCard>().powerText.text = newPower.ToString();
                Debug.Log(unit.transform.parent.name);

                switch (unit.transform.parent.name)
                {
                    case "MeleeZone": GetComponentInParent<Canvas>().GetComponent<GameController>().ImproveUnits(unit, "Melee"); break;
                    case "RangedZone": GetComponentInParent<Canvas>().GetComponent<GameController>().ImproveUnits(unit, "Ranged"); break;
                    case "SiegeZone": GetComponentInParent<Canvas>().GetComponent<GameController>().ImproveUnits(unit, "Siege"); break;
                }

                unit.transform.parent.parent.GetComponentInChildren<SumPower>().UpdatePower();
                //GetComponentInParent<Canvas>().GetComponent<GameController>().ImproveUnits(unit, )
            }

        }
    }
}