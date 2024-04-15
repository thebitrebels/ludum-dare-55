using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseController : MonoBehaviour
{
    
    [Header("Cursors")]
    public Texture2D defaultCursor, clickableCursor, summoningCursor;
    public GameObject particleSystem;
    public AudioClip summoningActiveSound;
    public SummoningBase activeSummoning;

    [Header("Overlay")]
    public Tilemap overlayTilemap;
    public Tilemap TileMap;
    
    private AudioSource _audioSource;

    private void Update()
    {
        ShowPlaceholder();

        if (activeSummoning != null)
        {
            if (Camera.main == null) return;
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            if (activeSummoning.IntersectsPlayer(TileMap, TileMap.WorldToCell(mousePosition)))
            {
                return;
            }
            if (Input.GetMouseButtonDown(1))
            {
                activeSummoning = null;
                Default();
            }
            else if (Input.GetMouseButtonDown(0))
            {

                var cellVector3 = TileMap.WorldToCell(mousePosition);
                if (activeSummoning.CanPerformSummonAt(cellVector3, TileMap))
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
        _audioSource = GetComponent<AudioSource>();
        Default();
    }

    public void Clickable()
    {
        if (activeSummoning != null) return;
        Cursor.SetCursor(clickableCursor, Vector2.zero, CursorMode.Auto);
        particleSystem.gameObject.SetActive(false);
        _audioSource.Stop();
    }

    public void Default()
    {
        if (activeSummoning != null) return;
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        particleSystem.gameObject.SetActive(false);
        _audioSource.Stop();
    }

    public void Summoning()
    {
        Cursor.SetCursor(summoningCursor, Vector2.zero, CursorMode.Auto);
        particleSystem.gameObject.SetActive(true);
        _audioSource.clip = summoningActiveSound;
        _audioSource.loop = true;
        _audioSource.Play();
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

        if (activeSummoning.IntersectsPlayer(TileMap, cellVector3))
        {
            return;
        }
        if (!activeSummoning.CanPerformSummonAt(cellVector3, TileMap))
            return;

        overlayTilemap.SetTile(cellVector3, activeSummoning.placeholderTile);
    }
}
