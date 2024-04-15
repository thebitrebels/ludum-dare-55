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

    public bool IntersectsPlayer()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var player = Physics2D.Raycast(pos, Vector2.down);
        return player;
    }

    public abstract void PerformSummon(Vector3Int worldToCellVector, Tilemap tilemap);

    public abstract bool CanPerformSummonAt(Vector3Int worldToCellVector, Tilemap tilemap);

    public abstract bool CanPerformSummon();
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
