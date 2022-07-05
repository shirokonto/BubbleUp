using UnityEngine;

namespace Features.Menu_Namespace.Scripts
{
    public class OpenLink : MonoBehaviour
    {
        public void OpenTsDownload()
        {
            Application.OpenURL("https://developer.leapmotion.com/get-started");
        }
    }
}
