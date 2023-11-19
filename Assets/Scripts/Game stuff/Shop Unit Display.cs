using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUnitDisplay : MonoBehaviour
{
    UnitManager unitMan;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] Image unitArt;

    [SerializeField] List<GameObject> traitsGameObjList = new List<GameObject>();
    [SerializeField] GameObject traitsGameObjPrefab;
    [SerializeField] GameObject traitsGameObjParent;

    [Space(10)]

    [SerializeField] public UnitData unitData;


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(unitData.unitTraits[0].traitName);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log(unitData.unitName);

        }

        
    }

    public void UpdateUnitDisplay()
    {
        nameText.text = unitData.unitName;
        costText.text = unitData.unitCost.ToString();
        unitArt.sprite = unitData.unitArt;
        UpdateTraitsDisplay();
    }

    public void UpdateTraitsDisplay()
    {
        for (int i = 0; i < traitsGameObjList.Count; i++)
        {
            Destroy(traitsGameObjList[i]);
        }
        traitsGameObjList.Clear();

        for (int i = 0; i < unitData.unitTraits.Count; i++)
        {
            GameObject newObject = Instantiate(traitsGameObjPrefab);
            newObject.transform.SetParent(traitsGameObjParent.transform);
            traitsGameObjList.Add(newObject);
            newObject.GetComponent<Image>().sprite = unitData.unitTraits[i].traitArt;
        }
    }

    public void PurchaseThisUnit()
    {
        if (unitMan.DoesBenchHaveSpace())
        {
            unitMan.PurchaseUnit(unitData);
            this.gameObject.SetActive(false);
        }
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
