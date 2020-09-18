using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame.
    // Late Update runs after all items have been processed in update
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
