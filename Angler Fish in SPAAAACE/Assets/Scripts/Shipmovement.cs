using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shipmovement : MonoBehaviour
{
    public float maxSpeed;
    public float closeEnough;
    public GameObject[] AstroPrefabs;
    public GameObject[] Planets;
    public Player player;
    public Vector3[] offsets;

    private bool empty = false;
    private bool scared = false;
    private GameObject home;
    private int morality = 0;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        home = Planets[Random.Range(0, Planets.Length)];
        StartCoroutine(Dangers());
    }

    // Update is called once per frame
    void Update()
    {

        if (!scared)
        {
            if (Mathf.Abs(Vector2.Distance(player.transform.position, transform.position)) < closeEnough)
            {
                SpawnAstronauts();
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * maxSpeed);
            }
        }
        else
        {
            transform.RotateAround(home.transform.position, Vector3.forward, maxSpeed * Time.deltaTime);
        }
    }

    IEnumerator Dangers()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {

            if (player.attractive)
            {
                scared = false;
            }
            else
            {
                scared = true;
                yield return new WaitForSeconds(3);
            }
            yield return new WaitForSeconds(1);
        }
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Organic"))
        {
            morality++;
            if (morality > 2)
            {
                empty = false;
            }
            Destroy(other.gameObject);
        }
    }

    void SpawnAstronauts()
    {
        if (!empty)
        {
            for (int i = 0; i < AstroPrefabs.Length; i++)
            {
                Instantiate(AstroPrefabs[i], transform.position + offsets[i], transform.rotation, transform);
            }
            empty = true;
        }
    }
}
