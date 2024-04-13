using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChargeSummon : MonoBehaviour, IPointerDownHandler
{
    public int MaxCharges = 1;
    public int CurrentCharges = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("MOUSEDOWN!");
    }
}
