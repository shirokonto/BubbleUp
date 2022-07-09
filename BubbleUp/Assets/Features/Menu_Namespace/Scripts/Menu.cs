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
}