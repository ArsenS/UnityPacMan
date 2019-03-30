﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int hiScore;
    public Text hiScoreText;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Teleporter leftTeleporter, rightTeleporter;
    private List<Pellet> pellets;

    // Start is called before the first frame update
    void Start()
    {
        hiScore = 0;
        hiScoreText.text = "HI-SCORE: " + hiScore;
        CreatePelletsList();
    }

    void CreatePelletsList()
    {
        pellets = new List<Pellet>();
        GameObject[] pelletsArray = GameObject.FindGameObjectsWithTag("SmallPellet");
        foreach (GameObject pellet in pelletsArray)
        {
            pellets.Add(pellet.GetComponent<Pellet>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pellets.Count == 0)
        {
            LevelComplete();
        }
    }

    public void PelletEaten(GameObject pellet)
    {
        pellets.Remove(pellet.GetComponent<Pellet>());
        if (pellet.tag == "PowerPellet")
        {
            print("PacManTime!");
        }
        UpdateScore(pellet);
        Destroy(pellet);
    }

    public void TeleportPlayer(GameObject teleporter)
    {
        if (teleporter.name == "LeftTeleporter")
        {
            player.TeleportRight();
        }
        if (teleporter.name == "RightTeleporter")
        {
            player.TeleportLeft();
        }
    }

    public Vector2 GetPlayerPosition()
    {
        return player.transform.position;
    }

    public void PlayerHasDied()
    {
        player.Death();
    }

    void LevelComplete()
    {
        //TODO
        //end state logic
    }

    void UpdateScore(GameObject pellet)
    {
        if (pellet.tag == "SmallPellet")
        {
            hiScore += 10;
        }
        else if (pellet.tag == "PowerPellet")
        {
            hiScore += 50;
        }
        hiScoreText.text = "HI-SCORE: " + hiScore;
    }
}
