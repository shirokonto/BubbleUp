using DataStructures.Variables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Features.Menu_Namespace.Scripts
{
    public class Menu : MonoBehaviour
    {
        public static Menu instance;
        private static bool _gameIsPaused = false;
        [SerializeField] private GameObject pauseMenuUI;
        public static bool IsGameOver;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject tutorialUI;
        [SerializeField] private GameObject leapMotionUI;
        [SerializeField] private GameObject mainMenuUI;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Animator transition;
        [SerializeField] private float transitionTime = 2f;
        [SerializeField] private IntVariable points;

        private void Awake()
        {
            IsGameOver = false;
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
                gameOverScreen.SetActive(true);
                audioSource.Stop();
                Time.timeScale = 0f;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void Play()
        {
            SceneManager.LoadScene("Character_Selection");
        }

        public void TryAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Hands
            SceneManager.LoadScene(3, LoadSceneMode.Additive); //Bubble
            SceneManager.LoadScene(4, LoadSceneMode.Additive); //Environment
            SceneManager.LoadScene(5, LoadSceneMode.Additive); //UI_Pavel
            SceneManager.LoadScene(6, LoadSceneMode.Additive); //UI
            points.Set(0);
            Time.timeScale = 1f;
        }

        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            _gameIsPaused = false;
        }
        
        private void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            _gameIsPaused = true;
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
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}