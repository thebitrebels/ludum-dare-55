using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SeedlingSummoning : SummoningBase
{
    public int maxCharges = 1;
    public int currentCharges = 1;
    public TileBase tileToPlace;

    public override bool CanPerformSummonAt(Vector3Int worldToCellVector, Tilemap tilemap)
    {
        if (!CanPerformSummon())
            return false;

        var clickedTile = tilemap.GetTile(worldToCellVector);
        Vector3Int belowClickedTileVector = new Vector3Int(worldToCellVector.x, worldToCellVector.y - 1);
        var tileBelowClickedTile = tilemap.GetTile(belowClickedTileVector);

        bool canPerformSummonHere = clickedTile == null && tileBelowClickedTile != null;

        return canPerformSummonHere;
    }

    public override void PerformSummon(Vector3Int worldToCellVector, Tilemap tilemap)
    {
        tilemap.SetTile(worldToCellVector, tileToPlace);
        currentCharges--;
    }

    public override bool CanPerformSummon()
    {
        return currentCharges > 0;
    }

    private void Update()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = $"{currentCharges}/{maxCharges}";
    }
}
