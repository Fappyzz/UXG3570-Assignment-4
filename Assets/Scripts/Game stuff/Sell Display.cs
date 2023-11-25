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
        hoveringSell = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoveringSell = false;
    }

    private void OnDisable()
    {
        hoveringSell = false;
    }
}
