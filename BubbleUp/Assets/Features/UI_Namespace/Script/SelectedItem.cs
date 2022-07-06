using System.Collections;
using DataStructures.Variables;
using UnityEngine;

namespace Features.UI_Namespace.Script
{
    public class SelectedItem : MonoBehaviour
    {
        public static SelectedItem instance;

        [SerializeField] private BoolVariable showAntiVirus;
        public static bool antiVirus;
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
            antiVirus = false;
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

        private void ShowAntiVirus()
        {
            StartCoroutine(ShowAntiVirusActive());
        }
        private void ShowVirus()
        {
            Virus.SetActive(true);
        }

        private void ShowMinimize()
        {
            Minimize.SetActive(true);
        }

        private void ShowTimer()
        {
            Timer.SetActive(true);
        }
        private IEnumerator ShowAntiVirusActive()
        {
            AntiVirus.SetActive(true);
            yield return new WaitForSeconds(3f);
            showAntiVirus.Set(false);
        }
        
    }
}
