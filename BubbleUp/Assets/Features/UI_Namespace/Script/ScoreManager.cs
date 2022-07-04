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
    [SerializeField] private IntVariable tempoPoints;
    public TextMeshProUGUI scoreText;
    public GameObject[] life;
    private bool dead;
    private int tempPoints = 0;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        tempPoints = life.Length;
        scoreText.text = points.Get() + " POINTS";
    }


    void Update()
    {
        tempPoints = 0;
        tempoPoints.Get();
        if(dead == true)
        {
            scoreText.text = points.Get() + " POINTS";
        }
        scoreText.text = points.Get() + " POINTS";
    }

    public void TakeDamage (int dam)
    {
        if (tempPoints >= 1)
        {
            tempPoints -= dam;
            Destroy(life[tempPoints].gameObject);

            if (tempPoints < 1)
            {
                dead = true;
            }
        }
    }
}
