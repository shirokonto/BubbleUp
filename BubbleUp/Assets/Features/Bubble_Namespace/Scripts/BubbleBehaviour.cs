using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BubbleBehaviour : MonoBehaviour
{
    [SerializeField] private string correctInfoType; //will be set before the game starts via character view
    private float _maximizeBubble = -0.03f;
    public ParticleSystem bubblePop;
    private Vector3 _scaleChange;
    private int _hit = 0;

    private void Awake()
    {
        _scaleChange = new Vector3(_maximizeBubble, _maximizeBubble, _maximizeBubble);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("InfoObject"))
        {
            if (!collision.gameObject.transform.name.Contains(correctInfoType))
            {
                _hit += 1;
                transform.localScale -= _scaleChange;
                
                //_wrongItemCounter.Equals(0) ? Debug.Log("GAME OVER") : _wrongItemCounter -=1;
            }
            if(_hit == 3)
            {
                Destroy(this.gameObject);
            }
            collision.gameObject.SetActive(false);
        }
        
        //adblocker and co.
            
    }
   
}
