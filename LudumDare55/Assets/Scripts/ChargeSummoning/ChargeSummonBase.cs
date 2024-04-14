using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public abstract class ChargeSummonBase : MonoBehaviour, IPointerDownHandler
{
    public int maxCharges = 1;
    public int currentCharges = 1;

    protected bool ChangeText = false;

    protected bool SummoningActive = false;
    public TileBase tileToPlace;
    protected Tilemap TileMap;

    [Header("Overlay")]
    protected Tilemap overlayTilemap;
    public TileBase placeholderTile;

    protected void PerformStart()
    {
        TileMap = GameObject.FindGameObjectWithTag("ground").GetComponent<Tilemap>();
        overlayTilemap = GameObject.FindGameObjectWithTag("OverlayTilemap").GetComponent<Tilemap>();
        ChangeText = true;

        UpdateText();
    }

    protected void PerformUpdate()
    {
        if (SummoningActive)
        {
            // Check for right click to switch back to normal cursor
            if (Input.GetMouseButtonDown(1))
            {
                DeactivateSummoning();
            }
            // Check for left click
            else if (Input.GetMouseButtonDown(0))
            {
                PerformSummon();
                // DeactivateSummoning();
            }
        }

        UpdateText();
        ShowPlaceholder();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ActivateSummoning();
    }

    private void UpdateText()
    {
        if (!ChangeText) return;
        var text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = $"{currentCharges}/{maxCharges}";
        ChangeText = false;
    }

    protected void DeactivateSummoning()
    {
        MouseController.instance.Default();
        SummoningActive = false;
    }

    private void ActivateSummoning()
    {
        if (currentCharges <= 0)
            return;

        MouseController.instance.Summoning();
        SummoningActive = true;
    }

    private void ShowPlaceholder()
    {
        return;
        overlayTilemap.ClearAllTiles();
        if (SummoningActive && placeholderTile)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            var cellVector3 = TileMap.WorldToCell(mousePosition);

            overlayTilemap.SetTile(cellVector3, placeholderTile);
        }
    }

    protected abstract void PerformSummon();
}
