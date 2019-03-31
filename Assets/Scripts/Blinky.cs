using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinky : Ghost
{

    void Start()
    {
        base.Start();
        target = gameManager.GetPlayerPosition();
        isActive = true;
}
    
    void Update()
    {
        
        if (CanChangeDirection())
        {
            //print("right: " + CanMove(Vector2.right));
            //print("up: " + CanMove(Vector2.up));
            //print("left: " + CanMove(Vector2.left));
           // print("down: " + CanMove(Vector2.down));
        }
        
        
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
        Vector2 choice = Vector2.zero;
        Vector2 ghostPosition = GetPosition();
        target = gameManager.GetPlayerPosition();
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
