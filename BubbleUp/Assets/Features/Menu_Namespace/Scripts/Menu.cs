using System.Collections;
using DataStructures.Variables;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


namespace Features.Menu_Namespace.Scripts
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private BoolVariable isPauseIButtonHit;
        [SerializeField] private BoolVariable isInfoItemViewShown;
        [SerializeField] private GameObject pauseMenuUI;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private  Animator transition;
        [SerializeField] private  float transitionTime = 2f;
        [SerializeField] private int countdownTime;
        [SerializeField] private TextMeshProUGUI countdownDisplay;
        [SerializeField] private GameObject countdownAfterPause;
        public static Menu instance;
        public static bool IsGameOver;
        private static bool _gameIsPaused = false;
        private static bool _resumeGame;
        //public AudioSource AudioSource;


        private void Awake()
        {
            isPauseIButtonHit.Set(true); //for hand gesture
            IsGameOver = false;
            SceneManager.LoadScene(3, LoadSceneMode.Additive);
            //AudioSource = GetComponent<AudioSource>();
        }
 
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_gameIsPaused)
                {
                    Resume();
                    Countdown();
                    countdownAfterPause.SetActive(true);
                }
                else
                {
                    Pause();
                }
            }

            if (IsGameOver)
            {
                GOScreen();
                //AudioSource.Stop();
            }
            
            if (!IsGameOver && !_gameIsPaused && !isInfoItemViewShown.Get())
            {
                Time.timeScale = 1f;
            }

            if (_resumeGame)
            {
                Time.timeScale = 0f;
            }
        }
        

        /**
         * Reload the gameplay scenes after replay is chosen in
         * Pause or GameOver menu
         */
        public void TryAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
            SceneManager.LoadScene(3, LoadSceneMode.Additive);
            SceneManager.LoadScene(4, LoadSceneMode.Additive);
            SceneManager.LoadScene(5, LoadSceneMode.Additive);
            _gameIsPaused = false;
            Time.timeScale = 1f;
        }
        
        /**
         * Used by the Interaction Button to open the pause menu
         */
        public void TogglePauseMenu()
        {
            if (isPauseIButtonHit.Get())
            {
                Pause();
            }
            else
            {
                Resume();
                countdownAfterPause.SetActive(true);
                Countdown();
            }
        }

        /**
         * Resumes the gameplay
         */
        public void Resume()
        {            
            pauseMenuUI.SetActive(false);
            SetVariablesConditions(false, false, true, 1f);
        }
    
        /**
         * Pause the gameplay
         */
        private void Pause()
        {
            pauseMenuUI.SetActive(true);
            SetVariablesConditions(true, false, false, 0f);
        }

        /**
         * Load the main menu
         */
        public void LoadMenu()
        {
            SetVariablesConditions(false, true, false, 1f);
            SceneManager.LoadScene("MainMenu");
        }
        
        /**
         * The conditions of the variables _gameIsPaused, isInfoItemViewShown, isPauseIButtonHit
         * and timeScale have to be set for switching between views and gameplay
         */
        private void SetVariablesConditions(bool gamePaused, bool infoItemViewShown, bool iBtnPauseHit, float timeScale)
        {
            isPauseIButtonHit.Set(iBtnPauseHit);
            _gameIsPaused = gamePaused;
            isInfoItemViewShown.Set(infoItemViewShown);
            Time.timeScale = timeScale;
        }

        private IEnumerator CountdownCoroutine()
        {
            while (countdownTime > 0)
            {
                _resumeGame = true;
                countdownDisplay.text = countdownTime.ToString();
                yield return new WaitForSecondsRealtime(1f);
                countdownTime--;
            }
            countdownTime = 3;
            _resumeGame = false;
            countdownDisplay.gameObject.SetActive(false);
        }

        public void Countdown()
        {
            StartCoroutine(CountdownCoroutine());
        }

        /**
         * The Game Over screen is shown when the bubble bursts
         */
        private void GOScreen()
        {
            StartCoroutine(ShowGOScreen());
            pauseMenuUI.SetActive(false);

        }

        private IEnumerator ShowGOScreen()
        {
            yield return new WaitForSeconds(0.7f);
            gameOverScreen.SetActive(true);
            Time.timeScale = 0f;
        }

        /**
         * Quit the application
         */
        public void Quit()
        {
            Application.Quit();
        }
    }

}