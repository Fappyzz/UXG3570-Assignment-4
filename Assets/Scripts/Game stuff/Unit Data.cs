using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UnitData
{
    public string unitName;
    public int unitCost;
    public string unitDesc;
    public List<UnitTrait> unitTraits;


    public Sprite unitArt;
    public GameObject unitPrefab;


}
