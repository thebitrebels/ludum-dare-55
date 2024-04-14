using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseController : MonoBehaviour
{
    public static MouseController instance;

    public Texture2D defaultCursor, clickableCursor, summoningCursor;
    public GameObject particleSystem;
    
    public SummoningBase activeSummoning;

    [Header("Overlay")]
    public Tilemap overlayTilemap;
    public Tilemap TileMap;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        ShowPlaceholder();

        if (activeSummoning != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                activeSummoning = null;
                Default();
            }
            else if (Input.GetMouseButtonDown(0))
            {
                if (Camera.main == null) return;
                var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;

                var cellVector3 = TileMap.WorldToCell(mousePosition);
                if(activeSummoning.CanPerformSummonAt(cellVector3, TileMap))
                {
                    activeSummoning.PerformSummon(cellVector3, TileMap);
                    activeSummoning = null;
                    Default();
                }
            }
        }
    }

    private void Start()
    {
        Default();
    }

    public void Clickable()
    {
        Cursor.SetCursor(clickableCursor, Vector2.zero, CursorMode.Auto);
        particleSystem.gameObject.SetActive(false);
    }

    public void Default()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        particleSystem.gameObject.SetActive(false);
    }

    public void Summoning()
    {
        Cursor.SetCursor(summoningCursor, Vector2.zero, CursorMode.Auto);
        particleSystem.gameObject.SetActive(true);
    }

    public void SetActiveSummon(SummoningBase summon)
    {
        if (summon == null || !summon.CanPerformSummon())
            return;

        activeSummoning = summon;
        Summoning();
    }

    private void ShowPlaceholder()
    {
        overlayTilemap.ClearAllTiles();

        if (activeSummoning == null || !activeSummoning.placeholderTile)
            return;

        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        var cellVector3 = TileMap.WorldToCell(mousePosition);

        if (!activeSummoning.CanPerformSummonAt(cellVector3, TileMap))
            return;

        overlayTilemap.SetTile(cellVector3, activeSummoning.placeholderTile);
    }
}
