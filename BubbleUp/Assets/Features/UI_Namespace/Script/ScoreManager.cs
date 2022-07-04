using System.Collections;
using DataStructures.Variables;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] private IntVariable points;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
   // int score = 0;
    int highscore = 0;
    public GameObject[] bonbon;
    private int lifeWithBonbons;
    private int lifeAfterBonbons;
    private bool dead;
    


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

        scoreText.text = points.Get() + " POINTS";
      
    }


    void Update()
    {

        if (points.Get() <= 2)
        {
            dead = true;
        }

        if (dead == true)
        {
            scoreText.text = points.Get() + " POINTS";
        }

        

    }

    public void TakeDamage(int d)
    {
        lifeWithBonbons -= d;
        Destroy(bonbon[lifeWithBonbons].gameObject);
        if(lifeWithBonbons < 1)
        {
            dead = true;
        }
    }
}
