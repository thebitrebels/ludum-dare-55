using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SeedlingSummoning : SummoningBase
{
    public TileBase tileToPlace;
    public float lifespan = 10f;
    private float cooldown = 0f;
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
        cooldown = lifespan;
        StartCoroutine(DespawnAfterTimerElapsed(tilemap, worldToCellVector));
    }

    public override bool CanPerformSummon()
    {
        return cooldown <= 0f;
    }

    private IEnumerator DespawnAfterTimerElapsed(Tilemap tilemap, Vector3Int worldToCellVector)
    {
        yield return new WaitForSeconds(lifespan);
        tilemap.SetTile(worldToCellVector, null);
    }

    private void Update()
    {
        cooldown = Mathf.Max(0f, cooldown - Time.deltaTime);
        var timer = cooldown == 0f ? "" : $"{Mathf.Round(cooldown)}";
        GetComponentInChildren<TextMeshProUGUI>().text = $"{timer}";
    }
}
