using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UnitTrait
{
    public string traitName;
    public string traitDesc;
    public Sprite traitArt;
    public List<int> traitCountThresholds = new List<int>();
}
