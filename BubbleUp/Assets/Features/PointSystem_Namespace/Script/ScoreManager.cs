using System.Collections;
using DataStructures.Variables;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] private PointInt points;
    public Text scoreText;
    public Text highscoreText;
    int score = 0;
    int highscore = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        scoreText.text = score.ToString() + " POINTS";
        highscoreText.text = "HIGHSCORE: " + highscore.ToString();
    }

    public void AddPoint()
    {
        score += 3;
        scoreText.text = score.ToString() + " POINTS";

        if (highscore < score)
        {
            PlayerPrefs.SetInt("highscore", score);
        }

    }
    public void MinusPoint()
    {
        score -= 1;
        scoreText.text = score.ToString() + " POINTS";
    }
}
