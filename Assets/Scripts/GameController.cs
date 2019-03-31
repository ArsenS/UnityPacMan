using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int hiScore;
    public Text hiScoreText;
    public Text instructions;

    [SerializeField]
    private Player player;
    [SerializeField]
    private Ghost blinky, pinky, inky, clyde;
    private List<Ghost> ghosts;
    [SerializeField]
    private Teleporter leftTeleporter, rightTeleporter;
    private List<Pellet> pellets;

    private bool gameHasStarted = false;
    private bool gameHasEnded = false;

    private float frightenedStateLength = 10f;
    private bool frightenedStateActive = false;

    private int smallPelletPoints = 10;
    private int powerPelletPoints = 50;

    // Start is called before the first frame update
    void Start()
    {
        hiScore = 0;
        hiScoreText.text = "HI-SCORE: " + hiScore;
        CreatePelletsList();
        ghosts = new List<Ghost>();
        ghosts.Add(blinky);
        ghosts.Add(pinky);
        ghosts.Add(inky);
        ghosts.Add(clyde);
    }

    void Update()
    {
        if (!gameHasStarted && Input.GetKey(KeyCode.Return))
        {
            StartGame();
        }
        if (gameHasEnded && Input.GetKey(KeyCode.Return))
        {
            RestartGame();
        }

        if (frightenedStateActive)
        {
            frightenedStateLength -= Time.deltaTime;
        }
        if (frightenedStateLength <= 0f)
        {
            EndGhostsFrightenedState();
        }
    }

    void StartGame()
    {
        player = Instantiate(player) as Player;
        instructions.gameObject.SetActive(false);
        foreach (Ghost ghost in ghosts)
        {
            ghost.Activate();
        }
        Camera.main.transform.position = new Vector3(0f, 0f, -10f);
        gameHasStarted = true;
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
        UpdateScore(pellet.tag);
        Destroy(pellet);

        if (pellets.Count == 0)
        {
            LevelComplete();
        }
    }

    void UpdateScore(string objectEaten)
    {
        if (objectEaten == "SmallPellet")
        {
            hiScore += smallPelletPoints;
        }
        else if (objectEaten == "PowerPellet")
        {
            hiScore += powerPelletPoints;
        }
        hiScoreText.text = "HI-SCORE: " + hiScore;
    }

    void StartGhostsFrightenedState()
    {
        foreach (Ghost ghost in ghosts)
        {
            ghost.ActivateFrightenedState();
        }
        frightenedStateActive = true;
        frightenedStateLength = 10f;
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
        DeactivateGhosts();
        StartCoroutine(player.Death());
        gameHasEnded = true;
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

    void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}