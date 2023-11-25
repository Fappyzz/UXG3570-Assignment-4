using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public string message;
    public Sprite unitArt;

    private void OnMouseEnter()
    {
        TooltipManager.instance.SetAndShowToolTip(message, unitArt);
    }

    private void OnMouseExit()
    {
        TooltipManager.instance.HideToolTip();
    }
}
