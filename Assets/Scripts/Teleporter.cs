using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    GameController gameController;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            gameController.GetPlayer().TeleportPlayer(gameObject);
        }
        else if (collision.collider.tag == "Enemy")
        {
            print(collision);
        }
    }

}
