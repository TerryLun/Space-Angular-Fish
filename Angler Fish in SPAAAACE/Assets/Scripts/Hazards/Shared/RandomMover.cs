using UnityEngine;
using System.Collections;

/// <summary>
/// Moves GameObject up to maxSpeed * (random float from 0.5 to 1.0)
/// </summary>
public class RandomMover : MonoBehaviour
{
	public float maxSpeed;

	void Start ()
	{
        float randomSpeedModifier = Random.Range(0.5f, 1.0f);
		GetComponent<Rigidbody>().velocity = transform.forward * maxSpeed * randomSpeedModifier;
	}
}
