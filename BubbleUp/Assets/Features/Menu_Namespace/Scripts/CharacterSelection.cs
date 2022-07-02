using UnityEngine;
using UnityEngine.SceneManagement;

namespace Features.Menu_Namespace.Scripts
{
    public class CharacterSelection : MonoBehaviour
    {
        public GameObject[] characters; // Character variable to switch between characters
        public int selectedCharacter = 0; // saves which character is currently chosen

        public void NextCharacter() // if button is clicked
        {
            characters[selectedCharacter].SetActive(false); // selectedCharacter dectivated to switch to next character
            selectedCharacter = (selectedCharacter + 1) % characters.Length; // Index erhoeht um naechsten Charakter auszuwaehlen, modu Anzahl der Charaktere
            characters[selectedCharacter].SetActive(true); // neu ausgewaehlte Charakter wird aktiviert bzw sichtbar
        }

        public void PreviousCharacter()
        {
            characters[selectedCharacter].SetActive(false);
            selectedCharacter--;
            if (selectedCharacter < 0)
            {
                selectedCharacter += characters.Length;
            }
            characters[selectedCharacter].SetActive(true);
        }

        public void StartGame()
        {
            PlayerPrefs.SetInt("selectedCharacter", selectedCharacter); // save data
            SceneManager.LoadScene(2, LoadSceneMode.Single);
            SceneManager.LoadScene(3, LoadSceneMode.Additive);
            SceneManager.LoadScene(4, LoadSceneMode.Additive);
            SceneManager.LoadScene(5, LoadSceneMode.Additive);
            SceneManager.LoadScene(6, LoadSceneMode.Additive);
        }
    }
}