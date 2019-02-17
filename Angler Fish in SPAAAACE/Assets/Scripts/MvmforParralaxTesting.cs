using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MvmforParralaxTesting : MonoBehaviour
{

    public float playerspeed;
    Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        float aX = Input.GetAxis("Horizontal");
        float aY = Input.GetAxis("Vertical");

        Vector3 vel = new Vector3(aX, aY);
        if (vel.magnitude > 1)
        {
            vel.Normalize();
        }

        if (vel != Vector3.zero)
        {
            transform.up = vel;
        }
        rigid.velocity = vel * playerspeed;

 
    }


}
