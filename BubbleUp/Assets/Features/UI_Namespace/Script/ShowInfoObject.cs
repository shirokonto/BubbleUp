using DataStructures.Variables;
using UnityEngine;

namespace Features.UI_Namespace.Script
{   
    /**
     * Shows the UI-Window at the beginning of the game
     * which tells the player what info object to collect
     */
    public class ShowInfoObject : MonoBehaviour
    {
        [SerializeField] private StringVariable correctInfoType;

        // Start is called before the first frame update
        private void Start()
        {
            Transform child = transform.Find(correctInfoType.Get());
            child.gameObject.SetActive(true);
            Time.timeScale = 0f;
            Debug.Log(child.name);
        }
    }
}
