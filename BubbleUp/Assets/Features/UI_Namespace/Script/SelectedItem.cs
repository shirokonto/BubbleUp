using System.Collections;
using DataStructures.Variables;
using UnityEngine;

namespace Features.UI_Namespace.Script
{   
    /**
     * Handles enabling and disabling the Power-Up and Blocker icons
     * during playtime to indicate whether a power-up or a blocker
     * object is currently active or not.
     */
    public class SelectedItem : MonoBehaviour
    {
        [SerializeField] private BoolVariable showAntiVirus;
        [SerializeField] private BoolVariable showVirus;
        [SerializeField] private BoolVariable showMinimize;
        [SerializeField] private BoolVariable showSlowMo;
        [SerializeField] private GameObject antiVirus;
        [SerializeField] private GameObject virus;
        [SerializeField] private GameObject minimize;
        [SerializeField] private GameObject slowMo;

        // Start is called before the first frame update
        void Start()
        {
            showAntiVirus.Set(false);
            showVirus.Set(false);
            showMinimize.Set(false);
            showSlowMo.Set(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (showAntiVirus.Get())
            {
                ShowAntiVirus();
            } else
            {
                antiVirus.SetActive(false);
            }

            if (showVirus.Get() && !showAntiVirus.Get())
            {
                ShowVirus();
            }
            else
            {
                virus.SetActive(false);
            }
            
            if (showMinimize.Get())
            {
                ShowMinimize();
            }
            else
            {
                minimize.SetActive(false);
            }
            
            if (showSlowMo.Get())
            {
                ShowTimer();
            }
            else
            {
                slowMo.SetActive(false);
            }
        }
        
        /**
         * Starts the ShowAntiVirusActive routine
         */
        private void ShowAntiVirus()
        {
            StartCoroutine(ShowAntiVirusActive());
        }
        
        /**
         * Sets the virus blocker object UI-icon active
         */
        private void ShowVirus()
        {
            virus.SetActive(true);
        }

        /**
         * Sets the minimize bubble power-up UI-icon active
         */
        private void ShowMinimize()
        {
            minimize.SetActive(true);
        }
        
        /**
         * Sets the timer power-up UI-icon active
         */
        private void ShowTimer()
        {
            slowMo.SetActive(true);
        }
        
        /**
         * Sets the anti virus power-up UI-icon active
         * and waits for 3 seconds before disabling it again
         */
        private IEnumerator ShowAntiVirusActive()
        {
            antiVirus.SetActive(true);
            yield return new WaitForSeconds(3f);
            showAntiVirus.Set(false);
        }
        
    }
}
