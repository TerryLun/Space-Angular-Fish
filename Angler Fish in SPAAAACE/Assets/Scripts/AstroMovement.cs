using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class AstroMovement : MonoBehaviour
{
    public float fleeSpeed;
    private Vector2 target;
    private Vector2 position;
    private Camera cam;
    Rigidbody2D rb;
    public float searchDistance;

    public Player player;
    public Transform Father;
    private Vector3 randomVector;

    // Start is called before the first frame update
    void Awake()
    {
        Father = transform.parent;
        rb = GetComponent<Rigidbody2D>();
        randomVector = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.attractive)
        {
            randomVector += new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f));
            transform.position = transform.position + Time.deltaTime * randomVector;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Father.position, Time.deltaTime * fleeSpeed);
        }
        

    }
}

