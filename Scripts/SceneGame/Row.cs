using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Row : MonoBehaviour
{
    public List<GameObject> unitObjects;
    public List<UnitCard> unitsInRow;
    public GameObject BoostRow = null;
    public SpecialCard boostCard;
    private GameObject powerRow = null;

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

    public void AddToRow(GameObject unit)
    {
        unitObjects.Add(unit);
        Debug.Log(unitObjects.Count);
        Debug.Log(this.name);
        if (unit.GetComponent<ThisCard>().thisCard is UnitCard)
        {
            unitsInRow.Add((UnitCard)unit.GetComponent<ThisCard>().thisCard);
        }

        powerRow.GetComponent<SumPower>().UpdatePower();
    }

    public void RemoveFromRow(GameObject unit)
    {
        unitObjects.Remove(unit);
        if (unit.GetComponent<ThisCard>().thisCard is UnitCard)
        {
            unitsInRow.Remove((UnitCard)unit.GetComponent<ThisCard>().thisCard);
        }
    }

}

