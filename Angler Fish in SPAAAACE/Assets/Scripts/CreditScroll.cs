using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class CreditScroll : MonoBehaviour
{
    public float yMax;
    private float start;
    private RectTransform here;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        start = GetComponent<RectTransform>().position.y;
        here = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        while(here.position.y > yMax)
        here.position.Set(here.position.x, here.position.y + speed * Time.deltaTime, here.position.z);

    }
}
