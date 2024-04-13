using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LateralMovementBase : MonoBehaviour
{

    public int leftPositionMax;
    public int rightPositionMax;

    public int leftRightFactor = 1;
    public float speed = 0.01f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
       
    }

    protected void RecalculateLeftRightFactor()
    {
        if(gameObject.transform.localPosition.x <= leftPositionMax)
        {
            leftRightFactor = 1;
        } else if (gameObject.transform.localPosition.x >= rightPositionMax)
        {
            leftRightFactor = -1;
        }
    }

    protected Vector2 CreateLateralMovementVector()
    {
        Vector2 lateralMovement = Vector2.zero;
        lateralMovement.x = speed * leftRightFactor;
        return lateralMovement;
    }
}
