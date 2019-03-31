using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinky : Ghost
{

    void Start()
    {
        base.Start();
        isActive = true;
    }
    
    void Update()
    {
        moveTime += Time.deltaTime;
         if (CanChangeDirection())
        {
            if (!CanMove(currentDirection) || moveTime > 1f)
            {
                StopMoving();
                PickNewDirection();
                moveTime = 0f;
            }
        }
    }
    
    void PickNewDirection()
    {
        //Blinky tries to chase PacMan directly
        Vector2 choice = Vector2.zero;
        Vector2 ghostPosition = GetPosition();
        target = gameController.GetPlayerPosition();
        Vector2 vectorToTarget = target - ghostPosition;

        Vector2 targetHorizontal = new Vector2(vectorToTarget.x, 0f).normalized;
        Vector2 targetVertical = new Vector2(0f, vectorToTarget.y).normalized;
        

        if (CanMove(targetVertical) && IsValidNewDirection(targetVertical))
        {
            choice = targetVertical;
        }
        else if (CanMove(targetHorizontal) && IsValidNewDirection(targetHorizontal))
        {
            choice = targetHorizontal;
        }
        else if (CanMove(-targetHorizontal) && IsValidNewDirection(-targetHorizontal))
        {
            choice = -targetHorizontal;
        }
        else
        {
            choice = -targetVertical;
        }

        UpdateDirection(choice);
    }
}
