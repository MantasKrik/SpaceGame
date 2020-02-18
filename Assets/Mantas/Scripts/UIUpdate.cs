using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdate : MonoBehaviour
{
    public PlayerController player;

    public Text scoreText;

    void Update()
    {
        scoreText.text = player.score.ToString();
    }
}
