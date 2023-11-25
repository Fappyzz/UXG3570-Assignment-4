using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class StupidShopBatteryThing : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    float onHoverTimer = 1f;
    [SerializeField] UnitManager unitManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("hi");
        unitManager.exp1.SetActive(true);
        bool fadeAway = true;
        if (UnitManager.exp == 0)
        {
            
            if (fadeAway && onHoverTimer >= 1f)
            {
                for (float i = 1; i >= 0; i -= Time.deltaTime)
                {
                    unitManager.exp1.GetComponent<Image>().color = new Color(0, 0, 0, i);
                    onHoverTimer = i;
                }

            }
            else
            {
                for (float i = 0; i <= 1; i += Time.deltaTime)
                {
                    unitManager.exp1.GetComponent<Image>().color = new Color(0, 0, 0, i);
                    onHoverTimer = i;
                }
            }


        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
