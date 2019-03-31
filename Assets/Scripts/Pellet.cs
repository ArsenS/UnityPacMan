using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    [SerializeField]
    private GameController gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "PacMan")
        {
            gameManager.PelletEaten(gameObject);
        }
    }
}
