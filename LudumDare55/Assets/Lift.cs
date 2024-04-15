using System;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public Vector3 axis;
    public float speed = 0.04f;
    public float maxDistance;

    private float _distanceTravelled;
    
    void FixedUpdate()
    {
        
        transform.Translate(speed * axis);
        _distanceTravelled += speed;
        if (_distanceTravelled > maxDistance || _distanceTravelled <= 0f)
        {
            speed *= -1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, maxDistance * axis);
        Gizmos.DrawCube(transform.position + (maxDistance * axis), new Vector3(2f,0.1f,0.1f));
    }
}
