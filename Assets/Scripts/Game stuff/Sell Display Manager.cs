using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellDisplayManager : MonoBehaviour
{
    [SerializeField] GameObject sellUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StandingUnit.isDragging)
        {
            sellUI.SetActive(true);
        }
        else
        {
            sellUI.SetActive(false);
        }
    }
}
