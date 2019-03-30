using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinky : Ghost
{
    private float moveTime = 0f;

    void Start()
    {
        base.Start();
    }
    
    void Update()
    {
        moveTime += Time.deltaTime;
        if ((moveTime >= 2f || (!CanMove(currentDirection)) && CanChangeDirection()))
        {
            PickNewDirection();
            moveTime = 0f;
        }
    }
    
    void PickNewDirection()
    {   
        Vector2 choice = Vector2.zero;
        Vector2 ghostPosition = GetPosition();
        target = gameManager.GetPlayerPosition();
        //print(target);
        Vector2 vectorToTarget = ghostPosition - target;
        
        if (Mathf.Abs(vectorToTarget.x) < Mathf.Abs(vectorToTarget.y))
        {
            choice = new Vector2(vectorToTarget.x, 0f).normalized;
        }
        else
        {
            //moving on the y axis
            choice = new Vector2(0f, vectorToTarget.y).normalized;
        }
        print(choice);
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
