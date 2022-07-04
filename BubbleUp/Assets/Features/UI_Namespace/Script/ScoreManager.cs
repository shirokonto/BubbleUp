using DataStructures.Variables;
using TMPro;
using UnityEngine;

namespace Features.UI_Namespace.Script
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager instance;
        [SerializeField] private IntVariable points;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI highscoreText;
        // int score = 0;
        //int highscore = 0;
        // Start is called before the first frame update

        private void Awake()
        {
            instance = this;
        }
        void Start()
        {
            //highscore = PlayerPrefs.GetInt("highscore", 0);
            scoreText.text = points.Get() + " POINTS";
            // highscoreText.text = "HIGHSCORE: " + highscore.ToString();
        }

        /* public void AddPoint()
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
   */


        void Update()
        {
            scoreText.text = points.Get() + " POINTS";
        }
    }
}
