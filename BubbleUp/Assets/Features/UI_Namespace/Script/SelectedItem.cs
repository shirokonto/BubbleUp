using UnityEngine;

namespace Features.UI_Namespace.Script
{
    public class SelectedItem : MonoBehaviour
    {
        public static SelectedItem instance;

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
            antiVirus = false;
            virus = false;
            minimize = false;
            timer = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (antiVirus)
            {
                ShowAntiVirus();
            } else
            {
                AntiVirus.SetActive(false);
            }

            if (virus)
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

        public void ShowAntiVirus()
        {
            AntiVirus.SetActive(true);
        }

        public void ShowVirus()
        {
            Virus.SetActive(true);
        }

        public void ShowMinimize()
        {
            Minimize.SetActive(true);
        }

        public void ShowTimer()
        {
            Timer.SetActive(true);
        }
    }
}
