using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    //public Transform grid;
    public List<GameObject> WeatherImagesPlayer = new List<GameObject>(); //lista con las imagenes de los efectos clima del objeto jugador
    public List<GameObject> WeatherImagesEnemy = new List<GameObject>(); //lista con las imagenes de los efectos clima del objeto enemigo
    [HideInInspector] public bool[] weather = { false, false, false }; //lista para verificar estado de la zona de climas

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

    /// <summary>
    /// Método para aplicar gráficamente el clima
    /// </summary>
    /// <param name="weatherType"></param>
    public void ApplyWeather(string weatherType)
    {
        foreach (var image in WeatherImagesPlayer)
        {
            if (image.name == weatherType)
            {
                image.SetActive(true);
            }
        }
        foreach (var image in WeatherImagesEnemy)
        {
            if (image.name == weatherType)
            {
                image.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Método para aplicar el efecto de un clima a una unidad
    /// </summary>
    /// <param name="unit">unidad a aplicar clima</param>
    /// <param name="unitTransform">Transform de la unidad</param>
    public void WeatherEffect(GameObject unit, Transform unitTransform)
    {
        if (weather[0] && unitTransform.name == "MeleeZone")
        {
            int newPower = 2;
            unit.GetComponent<ThisCard>().powerText.text = newPower.ToString();
        }
        else if (weather[1] && unitTransform.name == "RangedZone")
        {
            int newPower = 2;
            unit.GetComponent<ThisCard>().powerText.text = newPower.ToString();
        }
        else if (weather[2] && unitTransform.name == "SiegeZone")
        {
            int newPower = 2;
            unit.GetComponent<ThisCard>().powerText.text = newPower.ToString();
        }

    }

    /// <summary>
    /// Método para aplicar el efecto de un clima a una lista de unidades
    /// </summary>
    /// <param name="units">lista de unidades a aplicar clima</param>
    /// <param name="owner">jugador dueño de las unidades</param>
    public void WeatherEffect(List<GameObject> units, string owner)
    {
        foreach (var unit in units)
        {
            if (unit.GetComponent<ThisCard>().thisCard is Unit)
            {
                int newPower = 2;
                unit.GetComponent<ThisCard>().powerText.text = newPower.ToString();
                switch (unit.transform.parent.name)
                {
                    case "MeleeZone": GetComponentInParent<Canvas>().GetComponent<GameController>().ImproveAfterWeather(unit, "Melee", owner); break;
                    case "RangedZone": GetComponentInParent<Canvas>().GetComponent<GameController>().ImproveAfterWeather(unit, "Ranged", owner); break;
                    case "SiegeZone": GetComponentInParent<Canvas>().GetComponent<GameController>().ImproveAfterWeather(unit, "Siege", owner); break;
                }
            }

        }
    }
}