using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager instance;

    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI textComponent2;
    public Image unitSprite;

    private void Awake()
    {
        if (instance != null & instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void SetAndShowToolTip(string message, string message2, Sprite unitArt)
    {
        gameObject.SetActive(true);
        textComponent.text = message;
        textComponent2.text = message2;
        unitSprite.sprite = unitArt;
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
        textComponent.text = string.Empty;
        unitSprite.sprite = null;
    }    
}
