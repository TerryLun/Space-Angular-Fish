using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;
    private int score;

    /// <summary>
    /// energy level 0-100
    /// </summary>
    private int energyLevel;
    private int score_spaceship;
    private int score_astronaut;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    /// <summary>
    /// TODO: spawn spaceships
    /// 
    /// A coroutine is like a function that has the ability to pause execution and return control to Unity 
    /// but then to continue where it left off on the following frame.
    /// 
    /// https://docs.unity3d.com/Manual/Coroutines.html
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnWaves()
    {
        // From the GDOC:
        // Control Astronaut spawning
        // Need to be aware that fish isn’t moving
        // Need to stop after fish moves again
        // Control Spaceship spawning
        // Every 10, 15 - ish(whichever number) seconds, initialize with an angle that the ship goes in
        // Random number of astronauts initialized
        // Astronaut starts despawning after fish moves, at a certain rate and spaceship is nearby
        // If fish eats ship, astronauts just stay there
        // Picked up all astronauts, spaceship despawn via warping

        // Ensure there 0-3 ship hazards at any time
        // Everytime the coroutine wakes up, if num_hazards are within bounds, flip a coin to see if we spawn a ship


        // BELOW is the logic from the tutorial project, remove later
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    /// <summary>
    /// TODO: End the game
    /// </summary>
    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }

    /// <summary>
    /// Player takes damage
    /// </summary>
    /// <param name="spaceshipDamage"></param>
    public void TakeDamage(int damage)
    {
        energyLevel -= damage;
        if (energyLevel <= 0)
        {
            GameOver();
        }
    }

    // increment scores
    public void IncreaseScore(int amount)
    {
        score += amount;
    }
}