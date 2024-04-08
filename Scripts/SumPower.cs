using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SumPower : MonoBehaviour
{
    public GameObject row;
    public int power;

    public TextMeshProUGUI powerText;
    // Start is called before the first frame update
    void Start()
    {
        power = 0;
        powerText.text = power.ToString();

    }

    // Update is called once per frame
    public void UpdatePower()
    {
        Debug.Log("UpdatePower enter");
        int updatePower = 0;
        Debug.Log(this.name);
        Debug.Log(row.GetComponent<Row>().unitObjects.Count);
        Debug.Log(row.name);
        foreach (var unit in row.GetComponent<Row>().unitObjects)
        {
            Debug.Log("sitiiene");
            if (unit.GetComponent<ThisCard>().thisCard is UnitCard)
            {
                Debug.Log(unit.GetComponent<ThisCard>().powerText.text);
                updatePower += int.Parse(unit.GetComponent<ThisCard>().powerText.text);

            }
        }
        power = updatePower;
        Debug.Log(power);
        Debug.Log(powerText.text);
        powerText.text = power.ToString();
        Debug.Log(powerText.text);
        this.transform.parent.parent.Find("SumTotalPower").GetComponent<SumTotalPower>().UpdateTotalPower();
    }
}
