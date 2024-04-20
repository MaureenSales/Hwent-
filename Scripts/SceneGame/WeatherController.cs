using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    //public Transform grid;
    public List<GameObject> WeatherImagesPlayer = new List<GameObject>();
    public List<GameObject> WeatherImagesEnemy = new List<GameObject>();
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
        Debug.Log(unit.transform.parent.parent);
        Debug.Log("EnterWeatherEffect");
        Debug.Log(unit.GetComponent<ThisCard>().powerText.text);
            if (weather[0] && unitTransform.name == "MeleeZone")
            {
                Debug.Log("EnterWeatherFrost");
                int newPower = 2;
                unit.GetComponent<ThisCard>().powerText.text = newPower.ToString();
                if(unit.transform.parent.parent.name == "Enemy")
                {
                    unit.transform.parent.parent.Find("EnemyField").Find("MeleeRow").GetComponentInChildren<SumPower>().UpdatePower();
                }
                else
                {
                    unit.transform.parent.parent.Find("PlayerField").Find("MeleeRow").GetComponentInChildren<SumPower>().UpdatePower();
                }
            }
            else if (weather[1] && unitTransform.name == "RangedZone")
            {
                Debug.Log("EnterWeatherFog");
                Debug.Log(unit.transform.parent.name);
                int newPower = 2;
                unit.GetComponent<ThisCard>().powerText.text = newPower.ToString();
                if(unit.transform.parent.parent.name == "Enemy")
                {
                    unit.transform.parent.parent.Find("EnemyField").Find("RangedRow").GetComponentInChildren<SumPower>().UpdatePower();
                }
                else
                {
                    unit.transform.parent.parent.Find("PlayerField").Find("RangedRow").GetComponentInChildren<SumPower>().UpdatePower();
                }
            }
            else if (weather[2] && unitTransform.name == "SiegeZone")
            {
                Debug.Log("EnterWeatherRain");
                Debug.Log(unit.transform.parent.name);
                int newPower = 2;
                unit.GetComponent<ThisCard>().powerText.text = newPower.ToString();
                if(unit.transform.parent.parent.name == "Enemy")
                {
                    unit.transform.parent.parent.Find("EnemyField").Find("SiegeRow").GetComponentInChildren<SumPower>().UpdatePower();
                }
                else
                {
                    unit.transform.parent.parent.Find("PlayerField").Find("SiegeRow").GetComponentInChildren<SumPower>().UpdatePower();
                }
            }

            Debug.Log(unit.GetComponent<ThisCard>().powerText.text);

    }

    public void WeatherEffect(List<GameObject> units, string owner)
    {
        foreach (var unit in units)
        {
            if (!(unit.GetComponent<ThisCard>().thisCard is HeroUnit) && !(unit.GetComponent<ThisCard>().thisCard is DecoyUnit))
            {
                int newPower = 2;
                unit.GetComponent<ThisCard>().powerText.text = newPower.ToString();
                Debug.Log("EnterWeatherEffectList");
                switch (unit.transform.parent.name)
                {
                    case "MeleeZone": GetComponentInParent<Canvas>().GetComponent<GameController>().ImproveAfterWeather(unit, "Melee", owner); break;
                    case "RangedZone": GetComponentInParent<Canvas>().GetComponent<GameController>().ImproveAfterWeather(unit, "Ranged", owner); break;
                    case "SiegeZone": GetComponentInParent<Canvas>().GetComponent<GameController>().ImproveAfterWeather(unit, "Siege", owner); break;
                }

                unit.transform.parent.parent.GetComponentInChildren<SumPower>().UpdatePower();
            }

        }
    }
}