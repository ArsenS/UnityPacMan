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
    private Ghost[] ghosts;

    [SerializeField]
    AudioController audioController;

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
        audioController.PlayStartMusic();
        hiScore = 0;
        hiScoreText.text = "HI-SCORE: " + hiScore;
        CreatePelletsList();
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
        if (gameHasEnded && Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
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
        instructions.text = "";
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
        GameObject[] smallPelletsArray = GameObject.FindGameObjectsWithTag("SmallPellet");
        foreach (GameObject pellet in smallPelletsArray)
        {
            pellets.Add(pellet.GetComponent<Pellet>());
        }
        GameObject[] powerPelletsArray = GameObject.FindGameObjectsWithTag("PowerPellet");
        foreach (GameObject pellet in powerPelletsArray)
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
        audioController.PlayEatingSound();
        if (pellets.Count == 0)
        {
            DeactivateGhosts();
            player.gameObject.SetActive(false);
            GameOver("You win!");
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
        audioController.PlayDeathSoundEffect();
        GameOver("Game Over!");
    }

    private void DeactivateGhosts()
    {
        foreach (Ghost ghost in ghosts)
        {
            ghost.Deactivate();
        }
    }

    void GameOver(string message)
    {
        gameHasEnded = true;
        instructions.text = message + "\n Press Enter to play again.\n Press Escape to Quit.";
    }

    void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}