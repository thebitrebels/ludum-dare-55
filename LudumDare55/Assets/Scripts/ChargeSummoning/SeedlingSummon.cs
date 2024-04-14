using UnityEngine;

public class SeedlingSummon : ChargeSummonBase
{
    protected override void PerformSummon()
    {
        if (Camera.main == null) return;
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        var cellVector3 = TileMap.WorldToCell(mousePosition);

        var clickedTile = TileMap.GetTile(cellVector3);

        if (clickedTile != null) return;
        TileMap.SetTile(cellVector3, tileToPlace);

        // Example: Change the leftNum value
        currentCharges--;
        ChangeText = true;
    }

    // Start is called before the first frame update
    private void Start()
    {
        PerformStart();
    }

    // Update is called once per frame
    private void Update()
    {
        PerformUpdate();
    }
}
