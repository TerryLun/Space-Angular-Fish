using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour
{
	public float lifetime;

	void Start ()
	{
		if (gameObject.tag == "Spaceship")
        {
            Destroy(gameObject, lifetime);
            // after destruction, play warp drive animation
            Instantiate(warpDriveAnim, transform.position, transform.rotation);
        }
        Destroy (gameObject, lifetime);
	}
}
