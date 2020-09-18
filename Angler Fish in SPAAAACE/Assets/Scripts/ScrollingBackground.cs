using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float scrollSpeed;
    public float tileSizeZ;

    MeshRenderer mr;
    Material mat;

    private Vector2 offset;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mat = mr.material;
        offset = mat.mainTextureOffset;
    }
    // Update is called once per frame
    void Update()
    {
        offset = transform.position / scrollSpeed;
        mat.mainTextureOffset = offset;
    }
}
