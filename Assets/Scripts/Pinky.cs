using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinky : Ghost
{
    void Update()
    {
        if (isActive && timeToEnterMaze < 2f)
        {
            timeToEnterMaze += Time.deltaTime;
            if (timeToEnterMaze >= 2f)
            {
                EnterMaze();
            }
        }
        else if (isActive)
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

    }

    void PickNewDirection()
    {
        //Pinky tries to get in front of PacMan to ambush him
        Vector2 choice = Vector2.zero;
        Vector2 ghostPosition = GetPosition();
        target = gameController.GetPlayerPosition() + gameController.GetPlayerDirection();
        Vector2 vectorToTarget = target - ghostPosition;

        Vector2 targetHorizontal = new Vector2(vectorToTarget.x, 0f).normalized;
        Vector2 targetVertical = new Vector2(0f, vectorToTarget.y).normalized;


        if (CanMove(targetHorizontal) && IsValidNewDirection(targetHorizontal))
        {
            choice = targetHorizontal;
        }
        else if (CanMove(targetVertical) && IsValidNewDirection(targetVertical))
        {
            choice = targetVertical;
        }
        else if (CanMove(-targetVertical) && IsValidNewDirection(-targetVertical))
        {
            choice = -targetVertical;
        }
        else
        {
            choice = -targetHorizontal;
        }

        UpdateDirection(choice);
    }
}
