using System.Collections;
using System.Collections.Generic;
using Leap.Unity;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

public class BubbleBehaviour : MonoBehaviour
{
    [SerializeField] private string correctInfoType; //will be set before the game starts via character view
    private const float BUBBLE_SCALING = 0.03f;
    public ParticleSystem bubblePop;
    private Vector3 _scaleChange;
    private int _hit = 0;
    public bool adBlockerEnabled;
    public ParticleSystem bubblePop;

    private void Start()
    {
        _scaleChange = new Vector3(BUBBLE_SCALING, BUBBLE_SCALING, BUBBLE_SCALING);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "InfoObject": 
                HitInfoItem(collision.gameObject);
                break; ;
            case "MinimizeBubble":
                HitMinimizeBubble(collision.gameObject);
                break;
        }
    }
    private void HitInfoItem(GameObject infoItem){
        if (!infoItem.transform.name.Contains(correctInfoType))
        {
            _hit += 1;
            transform.localScale += _scaleChange;
        }
        if(_hit == 3)
        {
            Destroy(this.gameObject);
            bubblePop.Play();
        }
        infoItem.SetActive(false);
    }
    
    //TODO: call in HandBehaviour.cs
    public void HitMinimizeBubble(GameObject minimizeItem)
    {
        if (_hit >= 1 && _hit < 3)
        {
            _hit -= 1;
            transform.localScale -= _scaleChange;
        }
        minimizeItem.GetComponent<SphereCollider>().enabled = false;
        minimizeItem.SetActive(false);
    }
}
