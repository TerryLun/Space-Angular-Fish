using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float digestTime = 2f;
    [SerializeField] public int energyReduceByNonEdible = 20;
    [SerializeField] public int energyIncreaseByOrganic = 10;

    private Rigidbody2D rb;
    private Vector2 moveAmount;
    private bool ableToEat = true;
    private int energy = 100;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (energy <= 0) {
            GameOver();
        }
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;
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
            Destroy(other.gameObject);
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

    private void GameOver()
    {
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings-1);
    }
}
