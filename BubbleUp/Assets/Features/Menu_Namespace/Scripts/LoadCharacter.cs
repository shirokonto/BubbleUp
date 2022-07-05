using UnityEngine;

namespace Features.Menu_Namespace.Scripts
{
    public class LoadCharacter : MonoBehaviour
    {
        public GameObject[] characterPrefabs;
        public Transform spawnPoint;

        private void Start()
        {
            int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
            GameObject prefab = characterPrefabs[selectedCharacter];
            GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            clone.SetActive(true);
        }
    }
}
