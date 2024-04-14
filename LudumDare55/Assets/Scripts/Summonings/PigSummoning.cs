using UnityEngine;
using UnityEngine.Tilemaps;

public class PigSummoning : SummoningBase
{
    public int costRed = 1;
    public int costYellow = 0;
    public int costBlue = 1;
    public TileBase tileToPlace;

    public override bool CanPerformSummon()
    {
        Debug.Log("PIGGY SUMMON CAN PERFORM SUMMON CHECK");
        return ResourceManager.instance.CanPerformSummoning(costRed, costYellow, costBlue);
    }

    public override bool CanPerformSummonAt(Vector3Int worldToCellVector, Tilemap tilemap)
    {
        Debug.Log("PIGGY SUMMON CAN PERFORM -AT- SUMMON CHECK");
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
        Debug.Log("PIGGY SUMMON PERFORM");
        tilemap.SetTile(worldToCellVector, tileToPlace);
        ResourceManager.instance.PerformSummoning(costRed, costYellow, costBlue);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
