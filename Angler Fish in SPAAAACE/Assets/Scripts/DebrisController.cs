using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisController : MonoBehaviour
{
    public GameController gameController;
    public GameObject[] debris_hazards;
    public int maxAsteroids;
    private int hazardCount;
    private int spawnRadius;

    public float spawnWait;

    // Start is called before the first frame update
    void Start()
    {
        hazardCount = 0;
        StartCoroutine(SpawnDebris());
        spawnRadius = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnDebris()
    {
        yield return new WaitForSeconds(0.2f);
        while (true)
        {
            // always keep 10 asteroids in a range around the player, despawn if no longer in range
            while (hazardCount < maxAsteroids)
            {
                GameObject hazard = debris_hazards[Random.Range(0, debris_hazards.Length)];

                // Sets the position to be somewhere inside a circle
                // with radius 5 and the center at zero. Note that
                // assigning a Vector2 to a Vector3 is fine - it will
                // just set the X and Y values.
                Vector2 playerPosition = gameController.player.transform.position;
                Vector2 spawnPosition = playerPosition + Random.insideUnitCircle * spawnRadius;
                Debug.Log("Spawned Asteroid " + spawnPosition);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                hazardCount++;
            }
            yield return new WaitForSeconds(spawnWait);

            if (gameController.IsGameOver())
            {
                break;
            }
        }
    }
}
