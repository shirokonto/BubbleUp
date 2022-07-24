using UnityEngine;

namespace Features.Menu_Namespace.Scripts
{
    /**
     * Opens link in main menu to download Tracking Software 
     */
    public class OpenLink : MonoBehaviour
    {
        public void OpenTsDownload()
        {
            Application.OpenURL("https://developer.leapmotion.com/get-started");
        }
    }
}
