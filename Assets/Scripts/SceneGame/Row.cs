using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Row : MonoBehaviour
{
    public List<GameObject> unitObjects; //lista de unidades de la fila como GameObject
    public List<UnitCard> unitsInRow; //lista de unidade de la fila
    private GameObject powerRow = null; //poder total de la fila

    public void Start()
    {
        unitObjects = new();
        unitsInRow = new();
        if (this.name == "MeleeZone")
        {
            powerRow = this.transform.parent.Find("PowerMelee").gameObject;
        }
        else if (this.name == "RangedZone")
        {
            powerRow = this.transform.parent.Find("PowerRanged").gameObject;
        }
        else if (this.name == "SiegeZone")
        {
            powerRow = this.transform.parent.Find("PowerSiege").gameObject;
        }
    }

    /// <summary>
    /// Método para añadir una unidad a la fila
    /// </summary>
    /// <param name="unit"></param>
    public void AddToRow(GameObject unit)
    {
        unitObjects.Add(unit);
        Debug.Log(unitObjects.Count);
        Debug.Log(this.name);
        Debug.Log(unit.GetComponent<ThisCard>().thisCard.Name);
        if (unit.GetComponent<ThisCard>().thisCard is UnitCard)
        {
            Debug.Log(unit.GetComponent<ThisCard>().thisCard is null);
            unitsInRow.Add((UnitCard)unit.GetComponent<ThisCard>().thisCard);
        }

        powerRow.GetComponent<SumPower>().UpdatePower();
    }

    /// <summary>
    /// Método para remover una unidad de la fila
    /// </summary>
    /// <param name="unit"></param>
    public void RemoveFromRow(GameObject unit)
    {
        unitObjects.Remove(unit);
        if (unit.GetComponent<ThisCard>().thisCard is UnitCard)
        {
            unitsInRow.Remove((UnitCard)unit.GetComponent<ThisCard>().thisCard);
        }
    }

}

