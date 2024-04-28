using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SumTotalPower : MonoBehaviour
{
    public int total; //poder total del campo del jugador

    public TextMeshProUGUI totalText; // poder total del campo del jugador

    // Start is called before the first frame update
    void Start()
    {
        total = 0;
        totalText.text = total.ToString();
        
        UpdateTotalPower();

    }

    /// <summary>
    /// Método para actualizar el poder total del campo del jugador
    /// </summary>
    public void UpdateTotalPower()
    {
        int updatePower = 0;
        List<GameObject> powersRow = new List<GameObject>
        {
            this.transform.parent.Find("MeleeRow").Find("PowerMelee").gameObject,
            this.transform.parent.Find("RangedRow").Find("PowerRanged").gameObject,
            this.transform.parent.Find("SiegeRow").Find("PowerSiege").gameObject
        };
        
        foreach (var powerRow in powersRow)
        {
            updatePower += powerRow.GetComponent<SumPower>().power;
        }
        total = updatePower;
        totalText.text = total.ToString();

    }
 
}
