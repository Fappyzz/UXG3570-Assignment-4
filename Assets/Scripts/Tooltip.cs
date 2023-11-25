using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public string message;
    public string message2;
    public Sprite unitArt;

    private void OnMouseEnter()
    {
        TooltipManager.instance.SetAndShowToolTip(message, message2, unitArt);
    }

    private void OnMouseExit()
    {
        TooltipManager.instance.HideToolTip();
    }
}
