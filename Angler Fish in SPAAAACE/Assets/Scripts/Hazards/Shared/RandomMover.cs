using UnityEngine;
using System.Collections;

/// <summary>
/// Moves GameObject up to maxSpeed * (random float from 0.5 to 1.0)
/// </summary>
public class RandomMover : MonoBehaviour
{
	public float maxSpeed;


    private float speed = 1.0f;
    private Vector2 target;
    private Vector2 position;
    private Camera cam;
    public bool playerStopped;
    private Vector3 savedPosition;
    private GameObject[] astronaut_hazards;
    private int hazardCount, spawnWait, waveWait;
    public GameObject[] childsouls;


    //public GameObject player;
    public Player player;
    public int startWait;

    void Start()
    {        
        position = gameObject.transform.position;
        cam = Camera.main;
        StartCoroutine(PlayerMoved());
        playerStopped = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void FixedUpdate()
    {
        target = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
        if (Vector2.Distance(transform.position, target) >= 5.0f)
        {
            if (playerStopped)
            {
                Debug.Log("I see you");
                // move sprite towards the target location
                transform.position = Vector2.MoveTowards(transform.position, target, 0.3f);
                //StartCoroutine(SpawnAstronauts());
                return;
            }
            else
            {
                transform.Translate(transform.forward.normalized * maxSpeed * Time.fixedDeltaTime);
            }
                
        }
        else
        {
            if (playerStopped)
            {
                SpawnChildren();
            }
        }


        
    }

    void SpawnChildren()
    {
        Debug.Log("Making kid");
        

    }

    IEnumerator PlayerMoved()
    {
   
        yield return new WaitForSeconds(1);
        while (true)
        {

            if (player.attractive)
            {
                playerStopped = true;
            }
            else
            {
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


}
