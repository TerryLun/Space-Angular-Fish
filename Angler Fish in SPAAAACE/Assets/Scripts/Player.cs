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

    // Movement stuff with acceleration
    public bool facingRight = true;
    public float maxSpeed;
    public float timeZeroToMax;
    public float deccelerateBuff;
    public float waterResisance;

    private float inputx;
    private float inputy;

    private float accelRatePerSec;
    private float maxNegSpeed;
    private float forwardVelocityX;
    private float forwardVelocityY;
    private Vector2 moveVelocity;

    //boost
    private Boolean boostReady;
    private Boolean recharged;

    //Animation
    public Animator animator;
    private float chompTime;
    private float boostTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        accelRatePerSec = maxSpeed / timeZeroToMax;
        forwardVelocityX = 0f;
        forwardVelocityY = 0f;
        maxNegSpeed = maxSpeed * -1;
        deccelerateBuff *= accelRatePerSec;
        boostReady = false;
        recharged = true;
}

    private void Update()
    {

        if (recharged)
        {
            endBoost();
            recharged = false;
        }
        if (Input.GetKeyDown("left shift") && boostReady)
        {
            startBoost();
            boostReady = false;
        }
       

        attractive = false;
        
        if (moveAmount == Vector2.zero)
        {
            if (Input.GetButton("Attract"))
            {
                attractive = true;
                Debug.Log("Attractive: " + attractive);
            }
        }

        Vector2 moveInput2 = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput2 = moveInput2.normalized;

        inputx = Mathf.Abs(moveInput2.x);
        if (inputx < 0.000001 && inputx > -0.000001)
        {
            inputx = 1;
        }
        inputy = Mathf.Abs(moveInput2.y);
        if (inputy < 0.000001 && inputx > -0.000001)
        {
            inputy = 1;
        }

        Vector2 horizontalInput = new Vector2(1, 0);
        float accelDirectionX = Input.GetAxisRaw("Horizontal");
        if (accelDirectionX > 0)
        {
            if (forwardVelocityX < 0)
            {
                forwardVelocityX += deccelerateBuff * Time.fixedDeltaTime;
                forwardVelocityX = Mathf.Min(forwardVelocityX, maxSpeed);
            }
            else
            {
                forwardVelocityX += accelRatePerSec * Time.fixedDeltaTime;
                forwardVelocityX = Mathf.Min(forwardVelocityX, maxSpeed);
            }
            animator.SetBool("isSwimming", true);
        }
        else if (accelDirectionX < 0)
        {
            if (forwardVelocityX > 0)
            {
                forwardVelocityX -= deccelerateBuff * Time.fixedDeltaTime;
                forwardVelocityX = Mathf.Max(forwardVelocityX, maxNegSpeed);
            }
            else
            {
                forwardVelocityX -= accelRatePerSec * Time.fixedDeltaTime;
                forwardVelocityX = Mathf.Max(forwardVelocityX, maxNegSpeed);
            }
            animator.SetBool("isSwimming", true);
        }
        moveVelocity.x = horizontalInput.x * forwardVelocityX;

        Vector2 verticalInput = new Vector2(0, 1);
        float accelDirectionY = Input.GetAxisRaw("Vertical");
        if (accelDirectionY > 0)
        {
            if (forwardVelocityY < 0)
            {
                forwardVelocityY += deccelerateBuff * Time.fixedDeltaTime;
                forwardVelocityY = Mathf.Min(forwardVelocityY, maxSpeed);
            }
            else
            {
                forwardVelocityY += accelRatePerSec * Time.fixedDeltaTime;
                forwardVelocityY = Mathf.Min(forwardVelocityY, maxSpeed);
            }
            animator.SetBool("isSwimming", true);
        }
        else if (accelDirectionY < 0)
        {
            if (forwardVelocityY > 0)
            {
                forwardVelocityY -= deccelerateBuff * Time.fixedDeltaTime;
                forwardVelocityY = Mathf.Max(forwardVelocityY, maxNegSpeed);
            }
            else
            {
                forwardVelocityY -= accelRatePerSec * Time.fixedDeltaTime;
                forwardVelocityY = Mathf.Max(forwardVelocityY, maxNegSpeed);
            }
            animator.SetBool("isSwimming", true);
        }

        if(Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
        {
            animator.SetBool("isSwimming", false);
        }

        moveVelocity.y = verticalInput.y * forwardVelocityY;
        energy -= energydrain * Time.deltaTime;

        stopEat();

        if(Time.time - boostTime >= 1 && animator.GetBool("isBoosting"))
        {
            endBoost();
        }

    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //moveAmount = moveInput.normalized * speed;

        if (moveInput.normalized != Vector2.zero)
        {
            //Vector2 vecSubtract = Vector2.Set(0.0f,0.0f);
            transform.right = -moveInput.normalized ;
            Debug.Log("face direction"+moveInput.normalized);
        }
        
    }

    void startBoost()
    {
        Debug.Log("Boosting");
        energy -= energydrain;
        accelRatePerSec = accelRatePerSec * 10;
        deccelerateBuff = deccelerateBuff * 10;
        maxSpeed = maxSpeed * 3;
        maxNegSpeed = maxSpeed * -1;
        StartCoroutine(boostTimer());
        animator.SetBool("isBoosting", true);
        boostTime = Time.time;
    }

    void endBoost()
    {
        Debug.Log("Boost Ended");
        accelRatePerSec = accelRatePerSec / 10;
        deccelerateBuff = deccelerateBuff / 10;
        maxSpeed = maxSpeed / 3;
        maxNegSpeed = maxSpeed * -1;
        boostReady = true;
        animator.SetBool("isBoosting", false);
    }

    public IEnumerator boostTimer()
    {
        yield return new WaitForSeconds(4f); // waits 3 seconds
        recharged = true; // will make the update method pick up 
    }

    void OnCollisionEnter2D(Collision2D other) {
        
        if (other.gameObject.CompareTag("Metal"))
        {
            Destroy(other.gameObject);
            ableToEat = false;
            Debug.Log("cannot eat");
            StartCoroutine(Digest());
        }

        if ((other.gameObject.CompareTag("NonEdible")))
        {
            energy-= energyReduceByNonEdible;
            Debug.Log(energy);
        }

        if ((other.gameObject.CompareTag("Organic")) && (ableToEat == true))
        {
            energy+= energyIncreaseByOrganic;
            Debug.Log(energy);
            Destroy(other.gameObject);
            eatAnim();
        }
    }

    void eatAnim()
    {
        animator.SetBool("isEating", true);
        chompTime = Time.time;
    }

    void stopEat()
    {
        if(Time.time - chompTime >= 1 && animator.GetBool("isEating"))
        {
            animator.SetBool("isEating", false);
        }
    }

    IEnumerator Digest() {
        yield return new WaitForSeconds(digestTime);
        ableToEat = true;
        Debug.Log("can eat");
    }
}
