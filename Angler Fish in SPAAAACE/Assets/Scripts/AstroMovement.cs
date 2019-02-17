using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroMovement : MonoBehaviour
{

    private Vector2 target;
    private Vector2 position;
    private Camera cam;

    public bool playerStopped;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        position = gameObject.transform.position;
        cam = Camera.main;
        playerStopped = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(rb.position + moveAmount * Time.DeltaTime);
        transform.position = Time.deltaTime * 
        

    }
}
