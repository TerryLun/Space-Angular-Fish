using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Asteroid movement:
/// </summary>
public class DriftMover : MonoBehaviour
{
    private Vector3 randomVector;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        randomVector = new Vector3(Random.Range(-1.0f * speed, speed), Random.Range(-1.0f * speed, speed), 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + Time.deltaTime * randomVector;
    }
}
