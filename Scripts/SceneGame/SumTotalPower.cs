using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SumTotalPower : MonoBehaviour
{
    public int total;

    public TextMeshProUGUI totalText;
    // Start is called before the first frame update
    void Start()
    {
        total = 0;
        totalText.text = total.ToString();
        
        UpdateTotalPower();

    }

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
    // Update is called once per frame
    void Update()
    {

    }
}
