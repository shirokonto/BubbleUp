using System.Collections;
using DataStructures.Variables;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


namespace Features.Menu_Namespace.Scripts
{
    /**
     * Handles the menu interactions for main menu, pause and game over screen.
     */
    public class Menu : MonoBehaviour
    {
        [SerializeField] private BoolVariable isPauseIButtonHit;
        [SerializeField] private BoolVariable isInfoItemViewShown;
        [SerializeField] private GameObject pauseMenuUI;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private int countdownTime;
        [SerializeField] private TextMeshProUGUI countdownDisplay;
        [SerializeField] private GameObject countdownAfterPause;
        public static Menu instance; ///< the menu instance
        public static bool IsGameOver; ///< checks if game has finished or not
        private static bool _gameIsPaused = false;
        private static bool _resumeGame;

        private void Awake()
        {
            isPauseIButtonHit.Set(true); //for hand gesture
            IsGameOver = false;
            SceneManager.LoadScene(3, LoadSceneMode.Additive);
        }
 
        // Update is called once per frame
        private void Update()
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
            SceneManager.LoadScene(6, LoadSceneMode.Additive);
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

        /**
         * Countdown after pressing resume to pause for three seconds before continuing with the game
         */
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

        /*
         * Calls the Ienumerator in a new class
         */
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

        /*
         * Shows GameOver Screen
         */
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