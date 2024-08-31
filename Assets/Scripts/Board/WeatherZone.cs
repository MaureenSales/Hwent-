using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherZone : MonoBehaviour
{
    public List<GameObject> GetWeathers()
    {
        List<GameObject> weathers = new List<GameObject>();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            weathers.Add(this.transform.GetChild(i).GetChild(0).gameObject);
        }
        return weathers;
    }
}
