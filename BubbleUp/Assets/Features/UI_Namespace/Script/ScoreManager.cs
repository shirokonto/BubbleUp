using DataStructures.Variables;
using TMPro;
using UnityEngine;

namespace Features.UI_Namespace.Script
{   
    /**
     * Handles initializing and updating the score view during playtime
     */
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
        
        private void Start()
        {
            scoreText.text = points.Get() + " POINTS";
        }

        private void Update()
        {
            scoreText.text = points.Get() + " POINTS";
            scoreTextGO.text = points.Get() + " POINTS";
        }
    }
}
