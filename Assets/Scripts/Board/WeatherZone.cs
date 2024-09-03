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
            if(this.transform.GetChild(i).childCount == 0) continue;
            weathers.Add(this.transform.GetChild(i).GetChild(0).gameObject);
        }
        return weathers;
    }
}
