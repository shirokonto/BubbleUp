using System.Collections;
using System.Collections.Generic;
using Features.UI_Namespace.Script;
using UnityEngine;

namespace Features.Interactables_Namespace.Scripts
{
    public class PopupBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject popupWindow;
        private List<GameObject> _popupChildren;
        
        // Start is called before the first frame update

        /*
         * Initializes variables 
         */
        void Start()
        {
            _popupChildren = new List<GameObject>();
            popupWindow.SetActive(false);
            foreach (Transform child in popupWindow.transform)
            {
                _popupChildren.Add(child.gameObject);
                child.gameObject.SetActive(false);
            }
        }

        /*
         * Sets the PopUps visible
         */
        public void ShowPopups()
        {
            popupWindow.SetActive(true);
            StartCoroutine(ShowAdWhenVirusHit());
        }

        /*
         * Sets the different Popups visible
         */
        private IEnumerator ShowAdWhenVirusHit(){
            
            //show ad popups
            _popupChildren[4].SetActive(true);
            yield return new WaitForSeconds(0.3f);
            _popupChildren[1].SetActive(true);
            yield return new WaitForSeconds(0.3f);
            _popupChildren[4].SetActive(false);

            _popupChildren[2].SetActive(true);
            yield return new WaitForSeconds(0.3f);
            _popupChildren[1].SetActive(false);

            _popupChildren[3].SetActive(true);
            yield return new WaitForSeconds(0.3f);
            
            _popupChildren[0].SetActive(true);
            yield return new WaitForSeconds(0.3f);
            _popupChildren[2].SetActive(false);
            yield return new WaitForSeconds(0.3f);
            _popupChildren[3].SetActive(false); 
            
            yield return new WaitForSeconds(0.3f);
            _popupChildren[0].SetActive(false);

            SelectedItem.virus = false;
        }
    }
}
