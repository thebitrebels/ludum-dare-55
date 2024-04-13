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

public class ChargeSummon : MonoBehaviour, IPointerDownHandler
{
    public Texture2D customCursorTexture;
    public int maxCharges = 1;
    public int currentCharges = 1;

    private bool changeText = false;

    private bool summoningActive = false;
    public TileBase tileToPlace;
    private Tilemap tileMap;

    // Start is called before the first frame update
    void Start()
    {
        tileMap = FindObjectOfType<Tilemap>();
    }

    // Update is called once per frame
    void Update()
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
                Debug.Log("CLICK DETECTED");
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;

                var cellVector3 = tileMap.WorldToCell(mousePosition);
                Debug.Log("CELLVECTOR: " + cellVector3.ToString());

                var clickedTile = tileMap.GetTile(cellVector3);
                
                if (clickedTile == null)
                {
                    tileMap.SetTile(cellVector3, tileToPlace);

                    // Example: Change the leftNum value
                    currentCharges--;
                    changeText = true;
                    Debug.Log("Charges changed: " + currentCharges + " gameObject = ");

                    DeactivateSummoning();
                }
                else
                {
                    Debug.Log("CLICKED TILE: " + clickedTile.ToString());
                }
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

    void DeactivateSummoning()
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
}
