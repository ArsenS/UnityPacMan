using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinky : Ghost
{
    void Start()
    {
        base.Start();
        EnterMaze();
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
        float randVal = Random.Range(0f, 1f);
        Vector2 choice;
        if (randVal < 0.25f)
        {
            choice = Vector2.up;
        } 
        else if (randVal >= 0.25f && randVal < 0.5f)
        {
            choice = Vector2.down;
        }
        else if (randVal >= 0.5f && randVal < 0.75f)
        {
            choice = Vector2.left;
        }
        else
        {
            choice = Vector2.right;
        }
        if (IsValidNewDirection(choice))
        {
            UpdateDirection(choice);
        }
        else
        {
            PickNewDirection();
        }
    }
}
