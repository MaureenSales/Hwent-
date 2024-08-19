using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SumPower : MonoBehaviour
{
    public GameObject row; //fila correspondiente al sumador de poder de la zona
    public int power; //poder de la fila

    public TextMeshProUGUI powerText; //poder de la fila 

    // Start is called before the first frame update
    void Start()
    {
        power = 0;
        powerText.text = power.ToString();

    }

    /// <summary>
    /// Método para actualizar poder de la fila
    /// </summary>
    public void UpdatePower()
    {
        int updatePower = 0;
     
        foreach (var unit in row.GetComponent<Row>().unitObjects)
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

    void Update()
    {
        UpdatePower();
    }
}
