using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostCells : MonoBehaviour
{
    public GameObject CellM;
    public GameObject CellR;
    public GameObject CellS;
    public List<GameObject> CellsList;
    public List<GameObject> GetBoosts()
    {
        List<GameObject> list = new List<GameObject>();
        if(CellM.transform.childCount > 0) list.Add(CellM.transform.GetChild(0).gameObject);
        if(CellR.transform.childCount > 0) list.Add(CellR.transform.GetChild(0).gameObject);
        if(CellS.transform.childCount > 0) list.Add(CellS.transform.GetChild(0).gameObject);
        return list;
    }
    // Start is called before the first frame update
    void Start()
    {
        CellsList = new List<GameObject>()
        {
            CellM,
            CellR,
            CellS,
        };
    }

    // Update is called once per frame
    
}
