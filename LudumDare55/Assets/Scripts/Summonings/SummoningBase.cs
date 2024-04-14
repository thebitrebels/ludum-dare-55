using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public abstract class SummoningBase : MonoBehaviour, IPointerDownHandler
{

    public TileBase placeholderTile;

    public void OnPointerDown(PointerEventData eventData)
    {
        MouseController.instance.SetActiveSummon(this);
    }

    public abstract void PerformSummon(Vector3Int worldToCellVector, Tilemap tilemap);

    public abstract bool CanPerformSummonAt(Vector3Int worldToCellVector, Tilemap tilemap);

    public abstract bool CanPerformSummon();
}
