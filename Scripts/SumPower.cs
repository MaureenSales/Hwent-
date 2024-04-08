using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SumPower : MonoBehaviour
{
    public Row row;
    public int power;

    public TextMeshProUGUI powerText;
    // Start is called before the first frame update
    void Start()
    {
        power = 0;
        powerText.text = power.ToString();

        if (this.name == "PowerMelee")
        {
            row = this.transform.parent.GetComponentInChildren<Row>();
        }
        else if (this.name == "PowerRanged")
        {
            row = this.transform.parent.GetComponentInChildren<Row>();
        }
        else if (this.name == "PowerSiege")
        {
            row = this.transform.parent.GetComponentInChildren<Row>();
        }

    }

    // Update is called once per frame
    public void UpdatePower()
    {
        Debug.Log("UpdatePower enter");
        int updatePower = 0;

        foreach (var unit in row.unitObjects)
        {
            if (unit.GetComponent<ThisCard>().thisCard is UnitCard)
            {
                updatePower += int.Parse(unit.GetComponent<ThisCard>().powerText.text);

            }
        }
        power = updatePower;
        powerText.text = power.ToString();

        this.transform.parent.parent.Find("SumTotalPower").GetComponent<SumTotalPower>().UpdateTotalPower();
    }
}
