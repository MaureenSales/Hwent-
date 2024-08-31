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
    public GameObject Board;
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

    public void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (weather[j])
                {
                    for (int k = 0; k < Board.GetComponent<Board>().Fields[i].Rows[j].unitObjects.Count; k++)
                    {
                        int newPower = 2;
                        Board.GetComponent<Board>().Fields[i].Rows[j].unitObjects[k].GetComponent<ThisCard>().powerText.text = newPower.ToString();
                        if (Board.GetComponent<Board>().Fields[i].gameObject.GetComponent<BoostCells>().CellsList[j].transform.childCount > 0)
                        {
                            int power = 4;
                            Board.GetComponent<Board>().Fields[i].Rows[j].unitObjects[k].GetComponent<ThisCard>().powerText.text = power.ToString();
                        }
                    }

                }
            }
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
}