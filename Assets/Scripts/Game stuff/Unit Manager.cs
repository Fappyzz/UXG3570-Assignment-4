using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitManager : MonoBehaviour
{
    [SerializeField] List<ShopUnitDisplay> shopUnitDisplays = new List<ShopUnitDisplay>();

    [SerializeField] List<StandingUnitDisplay> benchUnitDisplays = new List<StandingUnitDisplay>();

    [SerializeField] List<StandingUnitDisplay> deployedUnitDisplays = new List<StandingUnitDisplay>();

    [SerializeField] UnitLibrary unitLib;

    [SerializeField] List<GameObject> traitsPopupGameObjList = new List<GameObject>();
    [SerializeField] GameObject traitsPopupGameObjPrefab;
    [SerializeField] GameObject traitsPopupGameObjParent;

    static public int currentUnits = 0;
    static public int maxUnits = 1;

    static public int exp = 0;
    static public int level = 1;

    static public int money = 100;
    int rerollCost = 5;
    int expCost = 5;

    [SerializeField] TextMeshProUGUI unitText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI expText;

    [SerializeField] GameObject exp1;
    [SerializeField] GameObject exp2;
    [SerializeField] GameObject exp3;

    List<UnitData> unitDataList = new List<UnitData>();

    public void Reroll()
    {
        if (money >= rerollCost)
        {
            money -= rerollCost;
            SetupShop();
        }
    }

    public void BuyExp()
    {
        if (money >= expCost)
        {
            money -= expCost;
            exp++;
            if (exp >= 4)
            {
                level++;
                exp = 0;
            }
        }
    }

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
        unitText.text = currentUnits.ToString() + "/" + maxUnits.ToString();
        levelText.text = level.ToString();
        moneyText.text = money.ToString();
        expText.text = exp.ToString() + " / 4";

        if (exp == 0)
        {
            exp1.SetActive(false);
            exp2.SetActive(false);
            exp3.SetActive(false);
        }
        else if (exp == 1)
        {
            exp1.SetActive(true);
            exp2.SetActive(false);
            exp3.SetActive(false);
        }
        else if (exp == 2)
        {
            exp1.SetActive(true);
            exp2.SetActive(true);
            exp3.SetActive(false);
        }
        else if (exp == 3)
        {
            exp1.SetActive(true);
            exp2.SetActive(true);
            exp3.SetActive(true);
        }

        maxUnits = level;

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
        if (CheckCan2Star(unit))
        {
            RemoveIngredients(unit);

            for (int i = 0; i < benchUnitDisplays.Count; i++)
            {
                if (benchUnitDisplays[i].hasUnit == false)
                {
                    unit.unitStar = 2;
                    benchUnitDisplays[i].ResetUnitDisplay();
                    benchUnitDisplays[i].SetUnitData(unit);
                    benchUnitDisplays[i].UpdateUnitDisplayFromShop();

                    unitDataList.Add(benchUnitDisplays[i].unitData);
                    return;
                }
            }
        }
        else
        {
            for (int i = 0; i < benchUnitDisplays.Count; i++)
            {
                if (benchUnitDisplays[i].hasUnit == false)
                {
                    benchUnitDisplays[i].ResetUnitDisplay();
                    benchUnitDisplays[i].SetUnitData(unit);
                    benchUnitDisplays[i].UpdateUnitDisplayFromShop();

                    unitDataList.Add(benchUnitDisplays[i].unitData);
                    return;
                }
            }
        }
        
    }

    public void SellUnit(UnitData unit)
    {
        unitDataList.Remove(unit);
        money += unit.unitCost;
    }

    bool CheckCan2Star(UnitData unit)
    {
        int count = 0;

        foreach (UnitData uD in unitDataList)
        {
            if (uD.unitName == unit.unitName)
            {
                count++;
            }
        }

        if (count >= 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void RemoveIngredients(UnitData unit)
    {
        foreach (UnitData uD in unitDataList)
        {
            if (uD.unitName == unit.unitName)
            {
                uD.unitSUD.ResetUnitDisplay();
            }
        }

        unitDataList.RemoveAll(x => x.unitName == unit.unitName);
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
