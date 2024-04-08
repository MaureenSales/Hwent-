using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Row : MonoBehaviour
{
    public List<GameObject> unitObjects = new();
    public List<UnitCard> unitsInRow = new();
    public GameObject BoostRow = null;
    public SpecialCard boostCard;
    private GameObject powerRow = null;

    public void Start()
    {
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
        if (unit.GetComponent<ThisCard>().thisCard is UnitCard)
        {
            unitsInRow.Add((UnitCard)unit.GetComponent<ThisCard>().thisCard);
        }
        else
        {

            //decoy
        }

        powerRow.GetComponent<SumPower>().UpdatePower();
    }

    public void BoostCard(GameObject boost)
    {
        BoostRow = boost;
        boostCard = (Boost)boost.GetComponent<ThisCard>().thisCard;
        //
    }
    public void RemoveFromRow(GameObject unit)
    {
        unitObjects.Remove(unit);
        if (unit.GetComponent<ThisCard>().thisCard is UnitCard)
        {
            unitsInRow.Remove((UnitCard)unit.GetComponent<ThisCard>().thisCard);
        }
        else
        {
            //decoy
        }
    }

}

