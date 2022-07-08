using System.Collections;
using System.Collections.Generic;
using DataStructures.Variables;
using Features.Menu_Namespace.Scripts;
using UnityEngine;

public class ShowInfoObject : MonoBehaviour
{
    [SerializeField] private StringVariable correctInfoType;
    [SerializeField] private GameObject viewsWindow;
    public bool showView;
    

    // Start is called before the first frame update
    void Start()
    {
        Transform child = transform.Find(correctInfoType.Get());

        child.gameObject.SetActive(true);
        //Time.timeScale = 0f;
        Debug.Log(child.name);
    }

    // Update is called once per frame
    void Update()
    {
        //if (showView)
        //{
        //    Transform child = transform.Find(correctInfoType.Get());

        //    child.gameObject.SetActive(true);
        //    //Time.timeScale = 0f;
        //    Debug.Log(child.name);
        //}
    }

    public void StartGame()
    {
        Transform child = transform.Find(correctInfoType.Get());
        child.gameObject.SetActive(false);
    }
}
