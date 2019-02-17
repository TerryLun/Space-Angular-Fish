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
			if (gameObject.tag == "Spaceship")
            {
                Instantiate(playerExplosion, transform.position, transform.rotation);
                gameController.TakeDamage(spaceshipDamage);
                // destroy the spaceship
                Destroy(gameObject);
            }
            if (gameObject.tag == "Astronaut")
            {
                // increment energy and score
                gameController.IncreaseScore(1);
                // gameController.IncreaseEnergy(100); // increase the energy
                gameController.TakeDamage(spaceshipDamage);
                // destroy the astronaut
                Destroy(gameObject);
            }

            // this would destroy the player
            // Destroy(gameObject);
        }

        // This line is not supposed to be executed; if it does, we'd like to know what triggered it
        Debug.Log("DestroyByContact triggered by: ", gameObject.tag);
        Destroy(gameObject);
	}
}