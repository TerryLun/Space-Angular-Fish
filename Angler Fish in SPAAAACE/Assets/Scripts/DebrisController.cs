using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisController : MonoBehaviour
{
    public GameController gameController;
    public GameObject[] debris_hazards;
    public int maxAsteroids;
    private int hazardCount;
    public int spawnRadius;

    public float spawnWait;

    // Start is called before the first frame update
    void Start()
    {
        hazardCount = 0;
        StartCoroutine(SpawnDebris());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DespawnHazard()
    {
        hazardCount--;
    }

    IEnumerator SpawnDebris()
    {
        //yield return new WaitForSeconds(0.2f);
        while (true)
        {
            // always keep 10 asteroids in a range around the player, despawn if no longer in range
            while (hazardCount < maxAsteroids)
            {
                GameObject hazard = debris_hazards[Random.Range(0, debris_hazards.Length)];
                Vector2 spawnPosition = gameController.getRandomSpawnPoint();
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
