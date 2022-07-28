using UnityEngine;

namespace Features.Menu_Namespace.Scripts
{
    /**
     * Takes care of the item movement between the starting way point
     * and the ending way point
     */
    public class LoadCharacter : MonoBehaviour
    {
        public GameObject[] characterPrefabs; ///< stores reference to all possible Prefabs that can be chosen
        public Transform spawnPoint;

    /**
     * Creates new variable and gets int of the same name we saved it in 
     * CharacterSelection script
     */
        private void Start()
        {
            int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
            GameObject prefab = characterPrefabs[selectedCharacter];
            GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            clone.SetActive(true);
        }
    }
}
