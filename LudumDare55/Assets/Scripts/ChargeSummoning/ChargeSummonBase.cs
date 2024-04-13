using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public abstract class ChargeSummonBase : MonoBehaviour, IPointerDownHandler
{
    public int maxCharges = 1;
    public int currentCharges = 1;

    protected bool changeText = false;

    protected bool summoningActive = false;
    public TileBase tileToPlace;
    protected Tilemap tileMap;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void PerformStart()
    {
        tileMap = FindObjectOfType<Tilemap>();
        UpdateText();
    }

    protected void PerformUpdate()
    {
        if (summoningActive)
        {
            // Check for right click to switch back to normal cursor
            if (Input.GetMouseButtonDown(1))
            {
                DeactivateSummoning();
                return;
            }

            // Check for left click
            if (Input.GetMouseButtonDown(0))
            {
                PerformSummon();
            }
        }

        UpdateText();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ActivateSummoning();
    }

    void UpdateText()
    {
        if(changeText)
        {
            var text = GetComponentInChildren<TextMeshProUGUI>();
            text.text = currentCharges + "/" + maxCharges;
            changeText = false;
        }
    }

    protected void DeactivateSummoning()
    {
        MouseController.instance.Default();
        summoningActive = false;
        Debug.Log("DEACTIVATE");
    }

    void ActivateSummoning()
    {
        if (currentCharges <= 0)
            return;

        MouseController.instance.Summoning();
        summoningActive = true;
        Debug.Log("ACTIVATE");
    }

    protected abstract void PerformSummon();
}
