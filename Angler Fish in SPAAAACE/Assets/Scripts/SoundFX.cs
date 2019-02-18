using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundFX : MonoBehaviour
{
    public AudioClip EatOrganic, EatShip, HitPlanet;
    private AudioSource soundboard;

    public void EatAstro()
    {
        soundboard.clip = EatOrganic;
        soundboard.Play();
    }

    public void EatMetal()
    {
        soundboard.clip = EatShip;
        soundboard.Play();
    }

    public void Ouch()
    {
        soundboard.clip = HitPlanet;
        soundboard.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        soundboard = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
