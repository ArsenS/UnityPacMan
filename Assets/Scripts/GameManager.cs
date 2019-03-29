﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int hiScore;
    public Text hiScoreText;
    private Player player;
    private Teleporter leftTeleporter;
    private Teleporter rightTeleporter;
    private List<Pellet> pellets;

    // Start is called before the first frame update
    void Start()
    {
        hiScore = 0;
        hiScoreText.text = "HI-SCORE: " + hiScore;
        player = GetComponent<Player>();
        leftTeleporter = GameObject.Find("LeftTeleporter").GetComponent<Teleporter>();
        rightTeleporter = GameObject.Find("RightTeleporter").GetComponent<Teleporter>();
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

    void LevelComplete()
    {
        //TODO
        //end state logic
    }

    public void PelletEaten(GameObject pellet)
    {
        pellets.Remove(pellet.GetComponent<Pellet>());
        UpdateScore(pellet);
        Destroy(pellet);
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
