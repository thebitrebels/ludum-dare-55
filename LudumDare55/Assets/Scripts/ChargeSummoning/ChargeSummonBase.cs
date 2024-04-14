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

    protected void PerformStart()
    {
        TileMap = FindObjectOfType<Tilemap>();
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
                return;
            }

            // Check for left click
            if (Input.GetMouseButtonDown(0))
            {
                PerformSummon();
            }
        }

        UpdateText();
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

    protected abstract void PerformSummon();
}
