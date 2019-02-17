using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Set in Inspector")]
    [Tooltip("This sets the speed of the player")]
    public float speed;
    [Tooltip("How long after eating a ship until the player can eat astronaughts again")]
    public float digestTime = 2f;
    [Tooltip("How much energy is lost hitting a planet")]
    public int energyReduceByNonEdible = 20;
    [Tooltip("How much energy is gained from eating an astronaut")]
    public int energyIncreaseByOrganic = 10;
    [Tooltip("At what rate is the player become hungry")]
    public float energydrain;
   
    private Rigidbody2D rb;
    private Vector2 moveAmount;
    private bool ableToEat = true;
    public float energy = 70;

    public bool attractive = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        attractive = false;
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (moveAmount == Vector2.zero)
        {
            if (Input.GetButton("Attract"))
            {
                attractive = true;
                Debug.Log("Attractive: " + attractive);
            }
        }
        moveAmount = moveInput.normalized * speed;

        if (moveInput.normalized != Vector2.zero)
        {
            transform.up = moveInput.normalized;
        }
        energy -= energydrain * Time.deltaTime;


    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D other) {
        
        if (other.gameObject.CompareTag("Metal"))
        {
            Destroy(other.gameObject);
            ableToEat = false;
            Debug.Log("cannot eat");
            StartCoroutine(Digest());
        }

        if ((other.gameObject.CompareTag("NonEdible")) && (ableToEat == true))
        {
            energy-= energyReduceByNonEdible;
            Debug.Log(energy);
        }

        if ((other.gameObject.CompareTag("Organic")) && (ableToEat == true))
        {
            energy+= energyIncreaseByOrganic;
            Debug.Log(energy);
            Destroy(other.gameObject);
        }
    }

    IEnumerator Digest() {
        yield return new WaitForSeconds(digestTime);
        ableToEat = true;
        Debug.Log("can eat");
    }
}
