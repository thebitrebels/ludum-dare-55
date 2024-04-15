using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateralMoveAlterSpeed : LateralMovementBase
{
    public int leftBorderForHighspeed = 0;
    public int rightBorderForHighspeed = 0;
    public int highspeedFactor = 5;

    // Update is called once per frame
    private new void Update()
    {
        RecalculateLeftRightFactor();

        Vector2 lateralMovement = CreateLateralMovementVector();

        if(gameObject.transform.localPosition.x > leftBorderForHighspeed && gameObject.transform.localPosition.x < rightBorderForHighspeed)
        {
            lateralMovement.x = lateralMovement.x * highspeedFactor;
        }

        gameObject.transform.Translate(lateralMovement);
    }
}
