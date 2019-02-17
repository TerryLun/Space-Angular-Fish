using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool facingRight = true;
    public float maxSpeed;
    public float timeZeroToMax;
    public float deccelerateBuff;
    public float waterResisance;

    private float inputx;
    private float inputy;

    private Rigidbody2D rb;
    private float accelRatePerSec;
    private float maxNegSpeed;
    private float forwardVelocityX;
    private float forwardVelocityY;
    private Vector2 moveVelocity;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        accelRatePerSec = maxSpeed / timeZeroToMax;
        forwardVelocityX = 0f;
        forwardVelocityY = 0f;
        maxNegSpeed = maxSpeed * -1;
        deccelerateBuff *= accelRatePerSec;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        moveInput = moveInput.normalized;

        inputx = Mathf.Abs(moveInput.x);
        if (inputx < 0.000001 && inputx > -0.000001)
        {
            inputx = 1;
        }
        inputy = Mathf.Abs(moveInput.y);
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

        } else if (accelDirectionX < 0) {
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
        }

        moveVelocity.y = verticalInput.y * forwardVelocityY;
        //moveVelocity.Normalize();


    }

    // Called once per frame when physics is used
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        return;
        if (rb.velocity.x < 0 && facingRight)
        {
            flip();
        } else if (rb.velocity.x > 0 && !facingRight)
        {
            flip();
        }
    }

    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector2.up * 180);
    }


}
