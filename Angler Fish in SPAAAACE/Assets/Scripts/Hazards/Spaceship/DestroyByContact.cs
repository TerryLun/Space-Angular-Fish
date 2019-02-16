using UnityEngine;
using System.Collections;

/// <summary>
/// Define behavior when player comes into contact with the spaceship
/// </summary>
public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

    public int spaceshipDamage = 10;

    /// <summary>
    /// Attempt to get a reference to game controller as we need it to keep track of player health
    /// </summary>
	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

    /// <summary>
    /// If contact spaceship, decrement health on player and destroy the spaceship.
    /// If contact astronaut, increase health on player and destroy the spaceship.
    /// </summary>
    /// <param name="other"></param>
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Boundary" || other.tag == "Enemy")
		{
			return;
		}

		if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);
		}

		if (other.tag == "Player")
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.TakeDamage(spaceshipDamage);
            // destroy the spaceship
            Destroy(other.gameObject);

            // this would destroy the player
            // Destroy(gameObject);
        }
	}
}