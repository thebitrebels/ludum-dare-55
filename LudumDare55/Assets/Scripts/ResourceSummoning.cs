using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public abstract class ResourceSummoning : MonoBehaviour, IPointerDownHandler
{
    public int costRed = 0;
    public int costYellow = 0;
    public int costBlue = 0;

    protected bool SummoningActive = false;
    public TileBase tileToPlace;
    protected Tilemap TileMap;

    private MouseController mouseController;

    // Start is called before the first frame update
    void Start()
    {
        mouseController = FindObjectOfType<MouseController>();
    }

    // Update is called once per frame
    void Update()
    {

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
                if (ResourceManager.instance.CanPerformSummoning(costRed, costYellow, costBlue))
                {
                    if (TryPerformSummon())
                    {
                        ResourceManager.instance.PerformSummoning(costRed, costYellow, costBlue);
                    };
                }
                DeactivateSummoning();
            }
        }
    }

    protected abstract bool TryPerformSummon();

    protected void DeactivateSummoning()
    {
        mouseController.Default();
        SummoningActive = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ActivateSummoning();
    }

    private void ActivateSummoning()
    {
        if (ResourceManager.instance.RedResource < costRed
            || ResourceManager.instance.YellowResource < costYellow
            || ResourceManager.instance.BlueResource < costBlue)
            return;

        mouseController.Summoning();
        SummoningActive = true;
    }
}
