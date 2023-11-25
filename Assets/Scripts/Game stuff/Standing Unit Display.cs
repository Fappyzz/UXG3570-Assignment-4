using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StandingUnitDisplay : MonoBehaviour
{
    public UnitManager unitMan;

    [SerializeField] public GameObject spawnedUnit;
    [SerializeField] GameObject spawnPos;

    public bool hasUnit = false;

    [Space(10)]

    [SerializeField] public UnitData unitData;


    void Start()
    {

    }


    void Update()
    {

    }

    public void UpdateUnitDisplay(bool setBenched, bool wasBenched)
    {
        unitData.unitSUD = this;
        spawnedUnit = Instantiate(unitData.unitPrefab);
        spawnedUnit.transform.position = spawnPos.transform.position;
        spawnedUnit.GetComponent<StandingUnit>().SetStandingUnitDisplay(this);
        spawnedUnit.GetComponent<StandingUnit>().benched = setBenched;
        hasUnit = true;

        if (setBenched && !wasBenched)
        {
            UnitManager.currentUnits--;
        }
        else if (!setBenched && wasBenched)
        {
            UnitManager.currentUnits++;
        }
    }
    
    public void UpdateUnitDisplayFromShop()
    {
        unitData.unitSUD = this;
        spawnedUnit = Instantiate(unitData.unitPrefab);
        spawnedUnit.transform.position = spawnPos.transform.position;
        spawnedUnit.GetComponent<StandingUnit>().SetStandingUnitDisplay(this);
        spawnedUnit.GetComponent<StandingUnit>().benched = true;
        hasUnit = true;
    }

    public void ResetUnitDisplay()
    {
        if (spawnedUnit != null)
        {
            Destroy(spawnedUnit);
        }

        unitData = null;
        hasUnit = false;
    }

    public void SetUnitMan(UnitManager unitMan)
    {
        this.unitMan = unitMan;
    }

    public void SetUnitData(UnitData unitData)
    {
        this.unitData = CreateNewAndCopyFields(unitData);
    }
    private UnitData CreateNewAndCopyFields(UnitData source)
    {
        UnitData newClone = new UnitData();

        // Use reflection to get all fields from the source class
        System.Reflection.FieldInfo[] fields = typeof(UnitData).GetFields();

        foreach (var field in fields)
        {
            // Copy the value of each field from the source to the destination
            field.SetValue(newClone, field.GetValue(source));
        }

        return newClone;
    }
}
