using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject[] spaceship_hazards;
    public Vector2 spawnOffsets;
    private int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public bool disableSpawning;

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
        score = 0;
        //StartCoroutine(SpawnWaves());
        hazardCount = 0;
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

    //// <summary>
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
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            if (hazardCount < 3 && Random.value >= 0.5 && !disableSpawning)
            {
                GameObject hazard = spaceship_hazards[Random.Range(0, spaceship_hazards.Length)];
                Vector2 spawnPosition = getRandomSpawnPoint();
                //-----------------------------------------------

                //float x = player.transform.position.x + Screen.width; 
                // float y = player.transform.position.y + Screen.height;
                // Vector2 spawnPosition = new Vector2(x, y);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                hazardCount++;
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
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
        gameOver = true;
    }

    public bool IsGameOver()
    {
        return gameOver;
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

    public Vector2 getRandomSpawnPoint()
    {
        float x = 0, y = 0;
        if (Random.Range(1.0f, -1.0f) > 0)
        {
            // 50% chance we start with x randomly assigned first
            x = Random.Range(-0.1f, 1.1f);
            if (x > 0.0f && x < 1.0f)
            {
                // within screen bounds, must make sure the other side is outside screen
                y = Random.Range(-0.1f, 0.1f);
                if (y > 0)
                {
                    y++;
                }
            }
        }

        else
        {
            // the other 50% means we start with y randomly assigned first
            y = Random.Range(-0.1f, 1.1f);
            if (y > 0.0f && y < 1.0f)
            {
                x = Random.Range(-0.1f, 0.1f);
                if (x > 0)
                {
                    x++;
                }
            }
        }

        Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector2(x, y));
        return spawnPosition;
    }
}