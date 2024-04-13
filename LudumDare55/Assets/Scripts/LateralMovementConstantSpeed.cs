using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateralMovementConstantSpeed : LateralMovementBase
{
    // Start is called before the first frame update
    
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        RecalculateLeftRightFactor();

        Vector2 lateralMovement = CreateLateralMovementVector();

        gameObject.transform.Translate(lateralMovement);
    }
}
