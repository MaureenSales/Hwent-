using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Row : MonoBehaviour
{
    public Field Owner { get; private set; }
    public List<GameObject> unitObjects {get; private set;} //lista de unidades de la fila como GameObject
    public List<UnitCard> unitsInRow {get; private set;} //lista de unidade de la fila
    public GameObject powerRow {get; private set; } //poder total de la fila
    public Global.AttackModes Type {get; private set;}

    public void Start()
    {
        Owner = this.transform.parent.parent.GetComponent<Field>();
        unitObjects = new();
        unitsInRow = new();
        if (this.name == "MeleeZone")
        {
            powerRow = this.transform.parent.Find("PowerMelee").gameObject;
            Type = Global.AttackModes.Melee;
        }
        else if (this.name == "RangedZone")
        {
            powerRow = this.transform.parent.Find("PowerRanged").gameObject;
            Type = Global.AttackModes.Ranged;
        }
        else if (this.name == "SiegeZone")
        {
            powerRow = this.transform.parent.Find("PowerSiege").gameObject;
            Type = Global.AttackModes.Siege;
        }
    }

    /// <summary>
    /// Método para añadir una unidad a la fila
    /// </summary>
    /// <param name="unit"></param>
    public void AddToRow(GameObject unit)
    {
        unitObjects.Add(unit);
        unitsInRow.Add((UnitCard)unit.GetComponent<ThisCard>().thisCard);
        Owner.AllCardsObjects.Add(unit);
        Owner.AllCards.Add(unit.GetComponent<ThisCard>().thisCard);
        powerRow.GetComponent<SumPower>().UpdatePower();
    }

    /// <summary>
    /// Método para remover una unidad de la fila
    /// </summary>
    /// <param name="unit"></param>
    public void RemoveFromRow(GameObject unit)
    {
        unitObjects.Remove(unit);
        unitsInRow.Remove((UnitCard)unit.GetComponent<ThisCard>().thisCard);
        Owner.AllCardsObjects.Remove(unit);
        Owner.AllCards.Remove(unit.GetComponent<ThisCard>().thisCard);
    }

    public void Clear()
    {
        unitObjects.Clear();
        unitsInRow.Clear();
        Owner.AllCardsObjects.Clear();
        Owner.AllCards.Clear();

    }

}