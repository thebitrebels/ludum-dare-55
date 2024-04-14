using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ElephantSummoning : SummoningBase
{
    public int costRed = 0;
    public int costYellow = 1;
    public int costBlue = 1;
    public TileBase tileToPlace;
    
    public override bool CanPerformSummon()
    {
        return ResourceManager.instance.CanPerformSummoning(costRed, costYellow, costBlue);
    }

    public override bool CanPerformSummonAt(Vector3Int worldToCellVector, Tilemap tilemap)
    {
        if (!CanPerformSummon())
            return false;

        var clickedTile = tilemap.GetTile(worldToCellVector);
        return clickedTile == null;
    }

    public override void PerformSummon(Vector3Int worldToCellVector, Tilemap tilemap)
    {
        // Drains Water
        tilemap.SetTile(worldToCellVector, tileToPlace);
        ResourceManager.instance.PerformSummoning(costRed, costYellow, costBlue);
        StartCoroutine(ClearTileWithDelay(tilemap, worldToCellVector));
    }

    static IEnumerator ClearTileWithDelay(Tilemap tilemap, Vector3Int pos)
    {
        yield return new WaitForSeconds(3);
        tilemap.SetTile(pos, null);
    }
}
