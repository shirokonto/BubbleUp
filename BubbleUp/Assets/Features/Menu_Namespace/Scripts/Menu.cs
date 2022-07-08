using System.Collections;
using DataStructures.Variables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Features.Menu_Namespace.Scripts
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private BoolVariable isPauseIButtonHit;
        [SerializeField] private BoolVariable isInfoItemViewShown;
        [SerializeField] private GameObject pauseMenuUI;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject tutorialUI;
        [SerializeField] private GameObject leapMotionUI;
        [SerializeField] private GameObject mainMenuUI;
        [SerializeField] private  Animator transition;
        [SerializeField] private  float transitionTime = 2f;
        public static Menu instance;
        public static bool IsGameOver;
        private static bool _gameIsPaused = false;
        //public AudioSource AudioSource;
        

        private void Awake()
        {
            isPauseIButtonHit.Set(true); //for hand gesture
            IsGameOver = false;
            SceneManager.LoadScene(4, LoadSceneMode.Additive);
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
            
        }

        private void GOScreen()
        {
            StartCoroutine(ShowGOScreen());
        }

        private IEnumerator ShowGOScreen()
        {          
            yield return new WaitForSeconds(0.7f);
            gameOverScreen.SetActive(true);
            Time.timeScale = 0f;
        }

        public void Play()
        {
            SceneManager.LoadScene("Character_Selection");
        }

        public void TryAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(3, LoadSceneMode.Additive);
            SceneManager.LoadScene(4, LoadSceneMode.Additive);
            SceneManager.LoadScene(5, LoadSceneMode.Additive);
            SceneManager.LoadScene(6, LoadSceneMode.Additive);
            _gameIsPaused = false;
            Time.timeScale = 1f;
        }

        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            _gameIsPaused = false;
            isPauseIButtonHit.Set(true);
        }
    
        private void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            _gameIsPaused = true;
            isPauseIButtonHit.Set(false);
        }

        public void TogglePauseMenu()
        {
            if (isPauseIButtonHit.Get())
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }

        public void Tutorial()
        {
            mainMenuUI.SetActive(false);
            tutorialUI.SetActive(true);
        }

        public void LeapMotion()
        {
            mainMenuUI.SetActive(false);
            leapMotionUI.SetActive(true);
        }

        public void BackT()
        {
            tutorialUI.SetActive(false);
            mainMenuUI.SetActive(true);
        }

        public void BackLm()
        {
            leapMotionUI.SetActive(false);
            mainMenuUI.SetActive(true);
        }

        public void LoadMenu()
        {
            SetBackTimeCondition(false, true, 1f);
            SceneManager.LoadScene("MainMenu");
        }

        public void Quit()
        {
            Application.Quit();
        }

        /**
         * the conditions gameIsPaused, isInfoItemViewShown and IsGameOver
         * have to be set to their original value when replaying the game
         */
        private void SetBackTimeCondition(bool gamePaused, bool infoItemViewShown, float timeScale)
        {
            _gameIsPaused = gamePaused;
            isInfoItemViewShown.Set(infoItemViewShown);
            Time.timeScale = timeScale;
        }
    }
}