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
        private static bool _antiVirus;
        public GameObject AntiVirus;

        public static bool virus;
        public GameObject Virus;

        public static bool minimize;
        public GameObject Minimize;

        public static bool timer;
        public GameObject Timer;

        

        // Start is called before the first frame update
        void Start()
        {
            showAntiVirus.Set(false);
            _antiVirus = false;
            virus = false;
            minimize = false;
            timer = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (showAntiVirus.Get())
            {
                ShowAntiVirus();
            } else
            {
                AntiVirus.SetActive(false);
            }

            if (virus && !showAntiVirus.Get())
            {
                ShowVirus();
            }
            else
            {
                Virus.SetActive(false);
            }
            
            if (minimize)
            {
                ShowMinimize();
            }
            else
            {
                Minimize.SetActive(false);
            }
            
            if (timer)
            {
                ShowTimer();
            }
            else
            {
                Timer.SetActive(false);
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
            Virus.SetActive(true);
        }

        /**
         * Sets the minimize bubble power-up UI-icon active
         */
        private void ShowMinimize()
        {
            Minimize.SetActive(true);
        }
        
        /**
         * Sets the timer power-up UI-icon active
         */
        private void ShowTimer()
        {
            Timer.SetActive(true);
        }
        
        /**
         * Sets the anti virus power-up UI-icon active
         * and waits for 3 seconds before disabling it again
         */
        private IEnumerator ShowAntiVirusActive()
        {
            AntiVirus.SetActive(true);
            yield return new WaitForSeconds(3f);
            showAntiVirus.Set(false);
        }
        
    }
}
