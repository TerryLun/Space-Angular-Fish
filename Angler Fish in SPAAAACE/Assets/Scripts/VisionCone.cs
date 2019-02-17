using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class VisionCone : MonoBehaviour
{
    Light lt;
    private float vision;
    float max = 70;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        lt = GetComponent<Light>();
        lt.type = LightType.Spot;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        lt.spotAngle = player.energy;

        Debug.Log(lt.spotAngle);
        Debug.Log(player.energy);
    }

}