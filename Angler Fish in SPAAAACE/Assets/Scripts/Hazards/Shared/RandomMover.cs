using UnityEngine;
using System.Collections;

/// <summary>
/// Moves GameObject up to maxSpeed * (random float from 0.5 to 1.0)
/// </summary>
public class RandomMover : MonoBehaviour
{
	public float maxSpeed;

    //void Start ()
    //{
    //       float randomSpeedModifier = Random.Range(0.5f, 1.0f);
    //       Vector2.MoveTowards
    //	GetComponent<Rigidbody>().velocity = transform.forward * maxSpeed * randomSpeedModifier;
    //}

    private float speed = 1.0f;
    private Vector2 target;
    private Vector2 position;
    private Camera cam;
    private bool playerStopped;
    private Vector3 savedPosition;
    private GameObject[] astronaut_hazards;
    private int hazardCount, spawnWait, waveWait;


    public GameObject player;
    public int startWait;

    void Start()
    {
        position = gameObject.transform.position;
        cam = Camera.main;
        StartCoroutine(PlayerMoved());
        playerStopped = false;
    }

    void Update()
    {
        target = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
        if (Vector2.Distance(transform.position, target) <= 2.0f)
        {
            if (playerStopped)
            {
                //StartCoroutine(SpawnAstronauts());
                return;
            }
                
        }

        float step = speed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, target, step);
        
    }

    IEnumerator PlayerMoved()
    {
   
        yield return new WaitForSeconds(1);
        while (true)
        {
            Vector3 currentPosition = Camera.main.transform.position;
            if (currentPosition.Equals(savedPosition))
            {
                playerStopped = true;
            }
            else
            {
                savedPosition = Camera.main.transform.position;
                playerStopped = false;
            }
            yield return new WaitForSeconds(1);

            //if (gameOver)
            //{
            //    restart = true;
            //    break;
            //}
        }
    }

    //IEnumerator SpawnAstronauts()
    //{
    //    // From the GDOC:
    //    // Control Astronaut spawning
    //    // Need to be aware that fish isn’t moving
    //    // Need to stop after fish moves again
    //    // Control Spaceship spawning
    //    // Every 10, 15 - ish(whichever number) seconds, initialize with an angle that the ship goes in
    //    // Random number of astronauts initialized
    //    // Astronaut starts despawning after fish moves, at a certain rate and spaceship is nearby
    //    // If fish eats ship, astronauts just stay there
    //    // Picked up all astronauts, spaceship despawn via warping

    //    // Ensure there 0-3 ship hazards at any time
    //    // Everytime the coroutine wakes up, if num_hazards are within bounds, flip a coin to see if we spawn a ship
    //    yield return new WaitForSeconds(startWait);
    //    while (true)
    //    {
    //        if (hazardCount < 3 && Random.value >= 0.5)
    //        {
    //            GameObject hazard = astronaut_hazards[Random.Range(0, astronaut_hazards.Length)];

    //            // -0.1 to 0.1 + 1
    //            Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector2(x, y));

    //            //float x = player.transform.position.x + Screen.width; 
    //            // float y = player.transform.position.y + Screen.height;
    //            // Vector2 spawnPosition = new Vector2(x, y);
    //            Quaternion spawnRotation = Quaternion.identity;
    //            Instantiate(hazard, spawnPosition, spawnRotation);
    //            hazardCount++;
    //            yield return new WaitForSeconds(spawnWait);
    //        }
    //        yield return new WaitForSeconds(waveWait);

    //        //if (gameOver)
    //        //{
    //        //    restart = true;
    //        //    break;
    //        //}
    //    }
    //}
}
