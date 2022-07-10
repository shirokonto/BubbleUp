using System.Collections;
using DataStructures.Variables;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Features.Menu_Namespace.Scripts
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private BoolVariable isPauseIButtonHit;
        [SerializeField] private BoolVariable resumeGame;
        public static Menu instance;
        private static bool _gameIsPaused = false;
        public GameObject PauseMenuUI;

        public static bool isGameOver;
        public GameObject GameOverScreen;

        public GameObject TutorialUI;

        public GameObject LeapMotionUI;

        public GameObject MainMenuUI;

        //public AudioSource AudioSource;

        public Animator transition;
        public float transitionTime = 2f;

        private void Awake()
        {
            isPauseIButtonHit.Set(true); //for hand gesture
            isGameOver = false;
            //SceneManager.LoadScene(4, LoadSceneMode.Additive);
        }
    
        // Start is called before the first frame update
        void Start()
        {
            //AudioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_gameIsPaused)
                {
                    StartCoroutine(PauseCoroutine());
                    Resume();
                }
                else
                {
                    Pause();
                }
            }

            if (isGameOver)
            {
                GOScreen();
                //AudioSource.Stop();
            }

            if (isGameOver == false && _gameIsPaused == false)
            {
                Time.timeScale = 1f;
            }
        }

        public void GOScreen()
        {
            StartCoroutine(ShowGOScreen());
        }

        public IEnumerator ShowGOScreen()
        {          
            yield return new WaitForSeconds(0.7f);
            GameOverScreen.SetActive(true);
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
            Time.timeScale = 1f;
        }

        public void Resume()
        { 
            
            PauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            _gameIsPaused = false;
            isPauseIButtonHit.Set(true);
            resumeGame.Set(true);
            Debug.Log("Resume Game");
            
        }
    
        private void Pause()
        {
            PauseMenuUI.SetActive(true);
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

        public IEnumerator PauseCoroutine()
        {
            yield return new WaitForSeconds(3f);
            PauseMenuUI.SetActive(false);
            Debug.Log("Wait for 3 seconds");

        }

        public void Tutorial()
        {
            MainMenuUI.SetActive(false);
            TutorialUI.SetActive(true);
        }

        public void LeapMotion()
        {
            MainMenuUI.SetActive(false);
            LeapMotionUI.SetActive(true);
        }

        public void BackT()
        {
            TutorialUI.SetActive(false);
            MainMenuUI.SetActive(true);
        }

        public void BackLM()
        {
            LeapMotionUI.SetActive(false);
            MainMenuUI.SetActive(true);
        }

        public void LoadMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");

        }

        public void Quit()
        {
            Application.Quit();
        }

    }
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
        
        public void Play()
        {
            SceneManager.LoadScene("Character_Selection");
        }

        /**
         * Reload the gameplay scenes after replay is chosen in
         * Pause or GameOver menu
         */
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
         * The Game Over screen is shown when the bubble bursts
         */
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

        /**
         * Quit the application
         */
        public void Quit()
        {
            Application.Quit();
        }
    }
}