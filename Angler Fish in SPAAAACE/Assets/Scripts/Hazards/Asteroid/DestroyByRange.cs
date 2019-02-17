using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroy the asteroid once it falls out of the player's radius
/// Attaches onto asteroid prefab
/// </summary>
public class DestroyByRange : MonoBehaviour
{
    private Player player;
    private DebrisController debrisController;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        debrisController = GameObject.FindGameObjectWithTag("DebrisController").GetComponent<DebrisController>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if ((other.gameObject.CompareTag("Player")))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 asteroidPosition = transform.position;
        if ((playerPosition - asteroidPosition).magnitude > debrisController.spawnRadius)
        {
            debrisController.DespawnHazard();
            Destroy(gameObject);
        }
    }
}
