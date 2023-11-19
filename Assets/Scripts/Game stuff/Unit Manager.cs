using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] List<ShopUnitDisplay> shopUnitDisplays = new List<ShopUnitDisplay>();
    [SerializeField] List<UnitData> shopUnits = new List<UnitData>();

    [SerializeField] List<UnitData> benchUnits = new List<UnitData>();

    [SerializeField] List<UnitData> deployedUnits = new List<UnitData>();

    [SerializeField] UnitLibrary unitLib;

    [SerializeField] List<GameObject> traitsPopupGameObjList = new List<GameObject>();
    [SerializeField] GameObject traitsPopupGameObjPrefab;
    [SerializeField] GameObject traitsPopupGameObjParent;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < shopUnitDisplays.Count; i++)
        {
            shopUnitDisplays[i].SetUnitMan(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

            //AddUnitToShop(unitLib.unitLib[0]);
            SetupShop();
            
        }

        if (Input.GetKeyDown(KeyCode.W))
        {

            CheckForTraitDeploy(shopUnits[0].unitTraits[0]);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {

            CheckForTraitRemove(shopUnits[0].unitTraits[0]);
        }
    }

    public void SetupShop()
    {
        shopUnits.Clear();

        for (int i = 0; i < 5; i++)
        {
            //AddUnitToShop(unitLib.unitLib[Random.Range(0, unitLib.unitLib.Count)]);
            AddUnitToShop(unitLib.unitLib[0]);

            shopUnitDisplays[i].gameObject.SetActive(true);
            shopUnitDisplays[i].SetUnitData(shopUnits[i]);
            shopUnitDisplays[i].UpdateUnitDisplay();
        }
    }

    public void AddUnitToShop(UnitData unit)
    {
        shopUnits.Add(CopyFields(unit));
    }

    public void PurchaseUnit(UnitData unit)
    {
        benchUnits.Add(CopyFields(unit));
    }

    public void DeployUnit(UnitData unit)
    {
        for (int i = 0; i < unit.unitTraits.Count; i++)
        {
            CheckForTraitDeploy(unit.unitTraits[i]);
        }

        deployedUnits.Add(CopyFields(unit));
        benchUnits.Remove(unit);
    }

    public void BenchUnit(UnitData unit)
    {
        for (int i = 0; i < unit.unitTraits.Count; i++)
        {
            CheckForTraitRemove(unit.unitTraits[i]);
        }

        benchUnits.Add(CopyFields(unit));
        deployedUnits.Remove(unit);
    }

    public void SellUnitFromBench(UnitData unit)
    {
        for (int i = 0; i < unit.unitTraits.Count; i++)
        {
            CheckForTraitRemove(unit.unitTraits[i]);
        }

        benchUnits.Remove(unit);
        //gain money
    }

    public void SellUnitFromDeployed(UnitData unit)
    {
        for (int i = 0; i < unit.unitTraits.Count; i++)
        {
            CheckForTraitRemove(unit.unitTraits[i]);
        }

        deployedUnits.Remove(unit);
        //gain money
    }

    public void CheckForTraitDeploy(UnitTrait trait)
    {
        // Check if any GameObject in the list has the same name
        GameObject existingObject = traitsPopupGameObjList.Find(obj => obj.GetComponent<TraitsPopup>().unitTrait.traitName == trait.traitName);

        if (existingObject != null)
        {
            // If a GameObject with the same name exists, increase the count
            existingObject.GetComponent<TraitsPopup>().IncreaseCount();
            existingObject.GetComponent<TraitsPopup>().UpdateTraitPopup();
        }
        else
        {
            // If no GameObject with the same name exists, instantiate a new one
            GameObject newObject = Instantiate(traitsPopupGameObjPrefab);
            newObject.transform.SetParent(traitsPopupGameObjParent.transform);
            traitsPopupGameObjList.Add(newObject);
            newObject.GetComponent<TraitsPopup>().unitTrait = trait;
            newObject.GetComponent<TraitsPopup>().UpdateTraitPopup();
        }
    }
    public void CheckForTraitRemove(UnitTrait trait)
    {
        // Check if any GameObject in the list has the same name
        GameObject existingObject = traitsPopupGameObjList.Find(obj => obj.GetComponent<TraitsPopup>().unitTrait.traitName == trait.traitName);

        if (existingObject != null)
        {
            existingObject.GetComponent<TraitsPopup>().DecreaseCount();
            existingObject.GetComponent<TraitsPopup>().UpdateTraitPopup();

            if (existingObject.GetComponent<TraitsPopup>().traitCounter <= 0)
            {
                traitsPopupGameObjList.Remove(existingObject);
                Destroy(existingObject.gameObject);
            }
        }
    }

    private UnitData CopyFields(UnitData source)
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
