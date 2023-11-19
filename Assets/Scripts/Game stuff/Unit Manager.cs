using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] List<ShopUnitDisplay> shopUnitDisplays = new List<ShopUnitDisplay>();

    [SerializeField] List<StandingUnitDisplay> benchUnitDisplays = new List<StandingUnitDisplay>();

    [SerializeField] List<StandingUnitDisplay> deployedUnitDisplays = new List<StandingUnitDisplay>();

    [SerializeField] UnitLibrary unitLib;

    [SerializeField] List<GameObject> traitsPopupGameObjList = new List<GameObject>();
    [SerializeField] GameObject traitsPopupGameObjPrefab;
    [SerializeField] GameObject traitsPopupGameObjParent;

    void Start()
    {
        for (int i = 0; i < shopUnitDisplays.Count; i++)
        {
            shopUnitDisplays[i].SetUnitMan(this);
        }
        
        for (int i = 0; i < benchUnitDisplays.Count; i++)
        {
            benchUnitDisplays[i].SetUnitMan(this);
        }
        
        for (int i = 0; i < deployedUnitDisplays.Count; i++)
        {
            deployedUnitDisplays[i].SetUnitMan(this);
        }

        SetupShop();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

        }

        if (Input.GetKeyDown(KeyCode.W))
        {

        }

        if (Input.GetKeyDown(KeyCode.E))
        {

        }
    }

    public void SetupShop()
    {
        for (int i = 0; i < shopUnitDisplays.Count; i++)
        {
            shopUnitDisplays[i].gameObject.SetActive(true);
            shopUnitDisplays[i].SetUnitData(GenerateFromLib());
            shopUnitDisplays[i].UpdateUnitDisplay();
        }
    }

    public void PurchaseUnit(UnitData unit)
    {
        for (int i = 0; i < benchUnitDisplays.Count; i++)
        {
            if (benchUnitDisplays[i].hasUnit == false)
            {
                benchUnitDisplays[i].ResetUnitDisplay();
                benchUnitDisplays[i].SetUnitData(unit);
                benchUnitDisplays[i].UpdateUnitDisplay(true);
                return;
            }
        }
    }

    public void DeployUnit(UnitData unit)
    {
        for (int i = 0; i < unit.unitTraits.Count; i++)
        {
            CheckForTraitDeploy(unit.unitTraits[i]);
        }
    }

    public void BenchUnit(UnitData unit)
    {
        for (int i = 0; i < unit.unitTraits.Count; i++)
        {
            CheckForTraitRemove(unit.unitTraits[i]);
        }
    }

    public void SellUnitFromBench(UnitData unit)
    {
        for (int i = 0; i < unit.unitTraits.Count; i++)
        {
            CheckForTraitRemove(unit.unitTraits[i]);
        }

        //gain money
    }

    public void SellUnitFromDeployed(UnitData unit)
    {
        for (int i = 0; i < unit.unitTraits.Count; i++)
        {
            CheckForTraitRemove(unit.unitTraits[i]);
        }

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

    public UnitData GenerateFromLib()
    {
        return unitLib.unitLib[Random.Range(0, unitLib.unitLib.Count)];
    }

    public bool DoesBenchHaveSpace()
    {
        for (int i = 0; i < benchUnitDisplays.Count; i++)
        {
            if (benchUnitDisplays[i].hasUnit == false)
            {
                return true;
            }
        }

        return false;
    }
}
