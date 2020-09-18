using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Indicator : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Color[] indicators;
    public Player player;
    private SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rend = GetComponent<SpriteRenderer>();
        StartCoroutine(changingtest());
    }

    IEnumerator changingtest()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            rend.color = indicators[0];
            yield return new WaitForSeconds(1);
            rend.color = indicators[1];
            yield return new WaitForSeconds(1);
            rend.color = indicators[2];
            yield return new WaitForSeconds(1);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(player.boostReady && player.ableToEat)
        {
            rend.color = indicators[0];
        } else if (player.boostReady && !player.ableToEat)
        {
            rend.color = indicators[1];
        } else if (!player.boostReady && player.ableToEat)
        {
            rend.color = indicators[2];
        }
        else
        {
            rend.color = indicators[3];
        }
    }
}
