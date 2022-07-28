using UnityEngine;

namespace Features.Menu_Namespace.Scripts
{
    /**
     * Opens a link in main menu 
     */
    public class OpenLink : MonoBehaviour
    {
     /**
     * Opens link in main menu to download Tracking Software 
     */
        public void OpenTsDownload()
        {
            Application.OpenURL("https://developer.leapmotion.com/get-started");
        }
    }
}
