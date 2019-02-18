using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawning : MonoBehaviour
{
    public GameObject[] Ships;
    public Vector3 offSet;
    public int maxShips;
    public float shipGainRate;
    public float shipSpawnRate;

    private bool goodToSpawn;
    private int currentShips;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlanetProduction());
        StartCoroutine(SpawningTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if(currentShips < maxShips && goodToSpawn)
        {
            goodToSpawn = false;
            SpawnShip();
        }

    }

    void SpawnShip()
    {
        GameObject spawn = Instantiate(Ships[Random.Range(0, Ships.Length)], transform.position + offSet, transform.rotation);
        spawn.GetComponent<Shipmovement>().home = transform;
    }

    IEnumerator SpawningTimer()
    {
        while (true)
        {
            goodToSpawn = true;
            yield return new WaitForSeconds(shipSpawnRate);
        }
    }

    IEnumerator PlanetProduction()
    {
        yield return new WaitForSeconds(shipGainRate);
        while (true)
        {
            maxShips++;
            yield return new WaitForSeconds(shipGainRate);
        }
    }
}
