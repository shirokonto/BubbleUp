using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLink : MonoBehaviour
{
    public void OpenTSDownload()
    {
        Application.OpenURL("https://developer.leapmotion.com/get-started");
    }
    public void OpenUDownload()
    {
        Application.OpenURL("https://developer.leapmotion.com/unity#setup-unity-packages");
    }
}
