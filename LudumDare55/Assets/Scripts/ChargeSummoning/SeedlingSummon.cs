using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SeedlingSummon : ChargeSummonBase
{
    protected override void PerformSummon()
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

    // Start is called before the first frame update
    void Start()
    {
        PerformStart();
    }

    // Update is called once per frame
    void Update()
    {
        PerformUpdate();
    }
}
