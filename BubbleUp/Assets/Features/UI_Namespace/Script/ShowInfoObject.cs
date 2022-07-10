using DataStructures.Variables;
using UnityEngine;

namespace Features.UI_Namespace.Script
{
    public class ShowInfoObject : MonoBehaviour
    {
        [SerializeField] private StringVariable correctInfoType;
        //public GameObject pauseMenuUI;


        // Start is called before the first frame update
        void Start()
        {
            Transform child = transform.Find(correctInfoType.Get());

            //Change that only the view with the "Ok" button is shown
            child.gameObject.SetActive(true);
            Time.timeScale = 0f;
            Debug.Log(child.name);
            //pauseMenuUI.SetActive(false);
        }
    }
}
