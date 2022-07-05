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
        public TextMeshProUGUI scoreTextGO;

        private void Awake()
        {
            instance = this;
            points.Set(0);
        }
        void Start()
        {
            scoreText.text = points.Get() + " POINTS";
        }
        
        void Update()
        {
            scoreText.text = points.Get() + " POINTS";
            scoreTextGO.text = points.Get() + " POINTS";
        }
    }
}
