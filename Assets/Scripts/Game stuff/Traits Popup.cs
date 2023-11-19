using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TraitsPopup : MonoBehaviour
{
    public UnitTrait unitTrait;

    public int traitCounter = 1;

    [SerializeField] Image traitArt;
    [SerializeField] TextMeshProUGUI traitNameText;
    [SerializeField] TextMeshProUGUI traitCounterText;
    [SerializeField] TextMeshProUGUI traitThresholdText;

    string traitThresholdString = "";

    public void IncreaseCount()
    {
        traitCounter++;
    }
    public void DecreaseCount()
    {
        traitCounter--;
    }

    public void UpdateTraitPopup()
    {
        traitNameText.text = unitTrait.traitName;
        traitCounterText.text = traitCounter.ToString();
        traitThresholdText.text = CreateThresholdString();
        traitArt.sprite = unitTrait.traitArt;
    }

    string CreateThresholdString()
    {
        traitThresholdString = "";

        for (int i = 0; i < unitTrait.traitCountThresholds.Count; i++)
        {
            traitThresholdString += unitTrait.traitCountThresholds[i].ToString();

            if (i != unitTrait.traitCountThresholds.Count - 1)
            {
                traitThresholdString += " / ";
            }
        }

        return traitThresholdString;
    }
}
