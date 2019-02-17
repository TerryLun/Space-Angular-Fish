using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Asteroid movement:
/// </summary>
public class DriftMover : MonoBehaviour
{
    private Vector3 randomVector;

    // Start is called before the first frame update
    void Start()
    {
        randomVector = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + Time.deltaTime * randomVector;
    }
}
