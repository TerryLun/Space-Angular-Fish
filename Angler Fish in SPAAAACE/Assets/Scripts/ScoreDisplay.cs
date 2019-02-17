using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text score;

    void Start()
    {
        score.text = Player.score.ToString();
    }


}
