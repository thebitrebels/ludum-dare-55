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

    public abstract void PerformSummon(Vector3Int worldToCellVector, Tilemap tilemap);

    public abstract bool CanPerformSummonAt(Vector3Int worldToCellVector, Tilemap tilemap);

    public abstract bool CanPerformSummon();
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
