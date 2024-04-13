using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class ChargeSummon : MonoBehaviour, IPointerDownHandler
{
    public Texture2D customCursorTexture;
    public int maxCharges = 1;
    public int currentCharges = 1;

    private bool changeText = false;

    private bool customCursorActive = false;
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
        if (customCursorActive)
        {

            
            // Check for right click to switch back to normal cursor
            if (Input.GetMouseButtonDown(1))
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                customCursorActive = false;
                Debug.Log("DEACTIVATE");
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
                    Debug.Log("Charges changed: " + currentCharges + " gameObject = ");
                }
                else
                {
                    Debug.Log("CLICKED TILE: " + clickedTile.ToString());
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Cursor.SetCursor(customCursorTexture, Vector2.zero, CursorMode.Auto);
        customCursorActive = true;
        Debug.Log("ACTIVATE");
    }


}
