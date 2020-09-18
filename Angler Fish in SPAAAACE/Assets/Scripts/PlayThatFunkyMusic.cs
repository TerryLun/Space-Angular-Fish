using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayThatFunkyMusic : MonoBehaviour
{

    public bool highIntensity = false;
    public AudioSource Intense;
    public AudioSource Default;

    // Update is called once per frame
    void Update()
    {
        if (highIntensity)
        {
            Intense.volume = 1;
            Default.volume = 0;
        }
        else
        {
            Default.volume = 1;
            Intense.volume = 0;
        }
    }
}
