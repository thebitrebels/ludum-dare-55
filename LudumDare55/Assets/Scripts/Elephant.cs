using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Elephant : MonoBehaviour
{
    public LayerMask waterLayer;
    public float drainSpeed = 0.01f;
    private List<Tilemap> _waterTilemaps;

    private Tilemap _drainingMap;
    private float _drained;
    
    private void Start()
    {
        _waterTilemaps = GameObject.FindGameObjectsWithTag("Water").Select(x => x.GetComponent<Tilemap>()).ToList();
        var closestWater = _waterTilemaps.OrderBy((map) => -Vector3.Distance(map.transform.position, transform.position)).FirstOrDefault();
        if (Vector3.Distance(closestWater.transform.position, transform.position) > 15f)
        {
            return;
        }
        Drain(closestWater);
    }

    private void Drain(Tilemap closestWater)
    {
        _drained = 0f;
        _drainingMap = closestWater;
    }

    private void FixedUpdate()
    {
        if (_drainingMap != null)
        {
            _drainingMap.transform.Translate(drainSpeed * Vector3.down);
            _drained += drainSpeed;
            if (_drained >= 1f)
            {
                _drainingMap.GetComponent<BoxCollider2D>().enabled = false;
                _drainingMap = null;
                
            }
        }
    }
}
