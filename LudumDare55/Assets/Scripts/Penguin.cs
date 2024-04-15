using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Penguin : MonoBehaviour
{
    public LayerMask waterLayer;
    public TileBase iceTile;
    private List<Tilemap> _waterTilemaps;

    private void Start()
    {
        _waterTilemaps = GameObject.FindGameObjectsWithTag("Water").Select(x => x.GetComponent<Tilemap>()).ToList();
        var closestWater = _waterTilemaps.OrderBy((map) => Vector3.Distance(map.transform.position, transform.position)).FirstOrDefault();
        if (Vector3.Distance(closestWater.transform.position, transform.position) > 15f)
        {
            return;
        }

        StartCoroutine(Freeze(closestWater));
    }

    private IEnumerator Freeze(Tilemap tilemap)
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int abovePos = new Vector3Int(pos.x, pos.y + 1, pos.z);
            if (tilemap.HasTile(pos) && !tilemap.HasTile(abovePos))
            {
                TileBase tile = tilemap.GetTile(pos);
                tilemap.SetTile(pos, iceTile);
                yield return new WaitForSeconds(0.2f);
            }
        }


        tilemap.GetComponent<BoxCollider2D>().enabled = false;
        tilemap.tag = "ground";
        tilemap.gameObject.layer = LayerMask.NameToLayer("Ground");
    }
}
