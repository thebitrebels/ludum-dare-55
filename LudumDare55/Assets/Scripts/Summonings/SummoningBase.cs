using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public abstract class SummoningBase : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    public TileBase placeholderTile;

    public void OnPointerUp(PointerEventData eventData)
    {
        FindObjectOfType<MouseController>().SetActiveSummon(this);
    }

    public bool IntersectsPlayer(Tilemap tilemap, Vector3Int worldToCellVector)
    {
        var playerPosition = FindObjectOfType<PlayerController>().transform.position;
        return Vector3.Distance(worldToCellVector, tilemap.WorldToCell(playerPosition)) <= 1f;
    }

    public abstract void PerformSummon(Vector3Int worldToCellVector, Tilemap tilemap);

    public abstract bool CanPerformSummonAt(Vector3Int worldToCellVector, Tilemap tilemap);

    public abstract bool CanPerformSummon();
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
