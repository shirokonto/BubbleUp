using DataStructures.Variables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Features.Menu_Namespace.Scripts
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private BoolVariable isPauseIButtonHit;
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
                    Resume();
                }
                else
                {
                    Pause();
                }
            }

            if (isGameOver)
            {
                GameOverScreen.SetActive(true);
                //AudioSource.Stop();
                Time.timeScale = 0f;
            }
            if (isGameOver == false && _gameIsPaused == false)
            {
                Time.timeScale = 1f;
            }
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