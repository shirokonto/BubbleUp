using DataStructures.Variables;
using TMPro;
using UnityEngine;

namespace Features.UI_Namespace.Script
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager instance;
        [SerializeField] private IntVariable points;
        [SerializeField] private BoolVariable hitBubble;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI scoreTextGO;
        public GameObject[] life;
        private bool dead;
        private int tempPoints;
        private int dam = 1;


        private void Awake()
        {
            instance = this;
            points.Set(0);
        }
        void Start()
        {
            tempPoints = life.Length;
            scoreText.text = points.Get() + " POINTS";

        }


        void Update()
        {

            scoreText.text = points.Get() + " POINTS";

            scoreTextGO.text = points.Get() + " POINTS";

        }

 
    }
}
