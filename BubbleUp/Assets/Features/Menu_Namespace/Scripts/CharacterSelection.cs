using DataStructures.Variables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Features.Menu_Namespace.Scripts
{
    /**
     * Selects the character which the player chooses
     */
    public class CharacterSelection : MonoBehaviour
    {
        [SerializeField] private IntVariable points;
        [SerializeField] private BoolVariable antiVirusEnabled;
        public GameObject[] characters; // Character variable to switch between characters
        public int selectedCharacter = 0; // saves which character is currently chosen

        /**
         * Called when the ">" button is clicked to set the next character in the index active
         */
        public void NextCharacter()
        {
            characters[selectedCharacter].SetActive(false); 
            selectedCharacter = (selectedCharacter + 1) % characters.Length; 
            characters[selectedCharacter].SetActive(true); 
        }

        /**
         * Called when the "<" button is clicked to set the previous character in the index active
         */
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

        /**
         * Called when Play button is clicked
         * Playerpref saves data (which character is selected) which can be transefered
         * onto the next scene
         */
        public void StartGame()
        {
            PlayerPrefs.SetInt("selectedCharacter", selectedCharacter); // save data
            points.Set(0);
            antiVirusEnabled.Set(false);
            LoadScenes();
        }

        /**
         * Loads the gameplay scenes after a character is chosen in
         * Character Selection scene
         */
        private void LoadScenes()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
            SceneManager.LoadScene(3, LoadSceneMode.Additive);
            SceneManager.LoadScene(4, LoadSceneMode.Additive);
            SceneManager.LoadScene(5, LoadSceneMode.Additive);
            SceneManager.LoadScene(6, LoadSceneMode.Additive);
        }
    }
}