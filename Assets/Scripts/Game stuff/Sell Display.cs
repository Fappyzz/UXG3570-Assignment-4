using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SellDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    static public bool hoveringSell = false;

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
        Debug.Log("Mouse entered UI element");

        hoveringSell = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exited UI element");

        hoveringSell = false;
    }

    private void OnDisable()
    {
        Debug.Log("Dis");

        hoveringSell = false;
    }
}
