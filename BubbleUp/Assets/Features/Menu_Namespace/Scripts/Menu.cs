using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Menu instance;

    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    public static bool isGameOver;
    public GameObject GameOverScreen;

    public GameObject TutorialUI;

    public GameObject LeapMotionUI;

    public GameObject MainMenuUI;

    public AudioSource AudioSource;

    public Animator transition;
    public float transitionTime = 2f;

    private void Awake()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
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
            AudioSource.Stop();
            Time.timeScale = 0f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        SceneManager.LoadScene("Character_Selection");
    }


    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(4, LoadSceneMode.Additive);
        SceneManager.LoadScene(5, LoadSceneMode.Additive);
        SceneManager.LoadScene(6, LoadSceneMode.Additive);
        Time.timeScale = 1f;
        
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    

    private void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
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