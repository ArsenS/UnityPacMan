using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int hiScore;
    public Text hiScoreText;

    [SerializeField]
    private Player player;
    [SerializeField]
    private Ghost[] ghosts;
    [SerializeField]
    private Teleporter leftTeleporter, rightTeleporter;
    private List<Pellet> pellets;

    private float frightenedStateLength = 5f;
    private bool frightenedStateActive = false;

    private int smallPelletPoints = 10;
    private int powerPelletPoints = 50;
    private int ghostPoints = 200;

    // Start is called before the first frame update
    void Start()
    {
        hiScore = 0;
        hiScoreText.text = "HI-SCORE: " + hiScore;
        CreatePelletsList();
    }

    void Update()
    {
        if (frightenedStateActive)
        {
            frightenedStateLength -= Time.deltaTime;
        }
        if (frightenedStateLength <= 0f)
        {
            EndGhostsFrightenedState();
        }
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

    public void PelletEaten(GameObject pellet)
    {
        pellets.Remove(pellet.GetComponent<Pellet>());
        if (pellet.tag == "PowerPellet")
        {
            StartGhostsFrightenedState();
        }
        UpdateScore(pellet);
        Destroy(pellet);

        if (pellets.Count == 0)
        {
            LevelComplete();
        }
    }

    void StartGhostsFrightenedState()
    {
        foreach (Ghost ghost in ghosts)
        {
            ghost.ActivateFrightenedState();
        }
        frightenedStateActive = true;
    }

    void EndGhostsFrightenedState()
    {
        foreach (Ghost ghost in ghosts)
        {
            ghost.DeactivateFrightenedState();
        }
        frightenedStateActive = false;
        frightenedStateLength = 10f;
    }

    public Player GetPlayer()
    {
        return player;
    }

    public Ghost GetGhost(string name)
    {
        Ghost ghostToTeleport = null;
        foreach (Ghost ghost in ghosts)
        {
            if (ghost.name == name)
            {
                ghostToTeleport = ghost;
            }
        }
        return ghostToTeleport;
    }

    public void TeleportGhost(GameObject teleporter, Collider2D ghost)
    {

    }

    public Vector2 GetPlayerPosition()
    {
        return player.transform.position;
    }

    public Vector2 GetPlayerDirection()
    {
        return player.GetDirection();
    }

    public void PlayerHasDied()
    {
        player.Death();
        DeactivateGhosts();
    }

    private void DeactivateGhosts()
    {
        foreach (Ghost ghost in ghosts)
        {
            ghost.Deactivate();
        }
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
            hiScore += smallPelletPoints;
        }
        else if (pellet.tag == "PowerPellet")
        {
            hiScore += powerPelletPoints;
        }
        hiScoreText.text = "HI-SCORE: " + hiScore;
    }
}
