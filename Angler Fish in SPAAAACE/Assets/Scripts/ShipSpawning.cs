using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawning : MonoBehaviour
{
    public GameObject[] Ships;
    public Vector3 initialPosition;
    public bool negative;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        if(initialPosition.x < 0)
        {
            negative = true;
        }
        else
        {
            negative = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (negative)
        {
            if(transform.position.x > 0)
            {
                negative = false;
                SpawnShip();
            }
        }
        else
        {
            if(transform.position.x < 0)
            {
                negative = true;
                SpawnShip();
            }
        }
    }

    void SpawnShip()
    {
        Instantiate(Ships[Random.Range(0, Ships.Length)]);
    }
}
