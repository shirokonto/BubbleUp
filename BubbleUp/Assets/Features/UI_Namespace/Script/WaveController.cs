using System;
using DataStructures.Variables;
using TMPro;
using UnityEngine;

namespace Features.UI_Namespace.Script
{
    /**
     * Handles the UI components for the two waves
     * and sets the timer before the second wave starts.
     * If the countdown hits zero, the secondWave variable
     * is triggered.
     */
    public class WaveController : MonoBehaviour
    {
        [SerializeField] private BoolVariable secondWave;
        [SerializeField] private FloatVariable currentTime;
        [SerializeField] private TextMeshProUGUI waveText;
        [SerializeField] private TextMeshProUGUI countdownText;
        private const float STARTING_TIME = 30f;
        private bool _triggeredSecondWave;
        private bool _isInitialized;
        
        private void Awake()
        {
            countdownText.text = "";
            waveText.text = "";
        }
    
        /**
         * Sets the countdown to 30 seconds and
         * waveText to "Wave 1/2".
         */
        public void Initialize()
        {
            _isInitialized = true;
            currentTime.Set(STARTING_TIME);
            waveText.text = "Wave 1/2";
        }

        /// Update is called once per frame
        private void Update ()
        {
            // sets the UI countdown component 
            if (!_isInitialized) return;
            if (_triggeredSecondWave) return;
            currentTime.Add(-1 * Time.deltaTime);
            countdownText.text = Math.Floor(currentTime.Get()/60).ToString("0") + ":" + Math.Floor(currentTime.Get() % 60).ToString("00");
            // triggers the second wave 
            if (currentTime.Get() <= 0) 
            {
                countdownText.text = "";
                waveText.text = "Wave 2/2";
                secondWave.Set(true);
                _triggeredSecondWave = true;
            }
        }
    }
}