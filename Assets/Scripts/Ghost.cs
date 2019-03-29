using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ghost : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject scatterModeTarget;

    private int timerToEnterMaze, timerToChangeBehavior;
    private Vector2 lastDirection;

    private IChaseBehavior chaseBehavior;
    private IScatterBehavior scatterBehavior;
    private IFrightenedBehavior frightenedBehavior;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "PacMan")
        {
            gameManager.PlayerHasDied();
        }
    }
}

interface IChaseBehavior
{
    void Chase();
}

public class DirectChase : IChaseBehavior
{
    public void Chase()
    {
        //TODO
        //Blinky chases PacMan directly
    }
}

public class AmbushChase : IChaseBehavior
{
    public void Chase()
    {
        //TODO
        //Pinky tries to reach a point ahead of PacMan and cut him off
    }
}

interface IScatterBehavior
{
    void Scatter();
}

public class ScatterPhase : IScatterBehavior
{
    public void Scatter()
    {
        //TODO
        //All ghosts have a "home target" they go back to during scatter phase
    }
}

interface IFrightenedBehavior
{
    void Frightened();
}

public class FrightenedState : IFrightenedBehavior
{
    public void Frightened()
    {
        //TODO
        //All ghosts flee pseudorandomly during frightened state
    }
}