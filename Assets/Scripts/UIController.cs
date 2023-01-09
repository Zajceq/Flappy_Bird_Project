using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private GameplayManager gameplayManager;

    private void Start()
    {
        gameplayManager = FindObjectOfType<GameplayManager>();
        scoreText.text = "Points: " + 0;
    }
    public void UpdateScoreText()
    {
        scoreText.text = "Points: " + gameplayManager.Score;
    }
}
