using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inky : Ghost
{
    void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (isActive && timeToEnterMaze < 10f)
        {
            timeToEnterMaze += Time.deltaTime;
            if (timeToEnterMaze >= 10f)
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
        //Inky alternates between chase and ambush
        
        Vector2 choice = Vector2.zero;
        Vector2 ghostPosition = GetPosition();
        float randVal = Random.Range(0f, 1f);
        if (randVal <= 0.5f)
        {
            target = gameController.GetPlayerPosition();
        } else
        {
            target = gameController.GetPlayerPosition() + gameController.GetPlayerDirection();
        }
        
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
