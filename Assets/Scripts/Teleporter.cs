using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO
        //refactor to go through GameManager

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        print(gameObject.name);
        if (gameObject.name == "LeftTeleporter")
        {
            player.TeleportRight();
        }
        if (gameObject.name == "RightTeleporter")
        {
            player.TeleportLeft();
        }
    }

}
