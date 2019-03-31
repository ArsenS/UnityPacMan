using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinky : Ghost
{
    void Start()
    {
        base.Start();
        EnterMaze();
        UpdateDirection(Vector2.right);
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
        Vector2 choice = Vector2.zero;
        Vector2 ghostPosition = GetPosition();
        target = gameManager.GetPlayerPosition() + gameManager.GetPlayerDirection();
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
