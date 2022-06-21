using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BubbleBehaviour : MonoBehaviour
{
    [SerializeField] private string correctInfoType; //will be set before the game starts via character view
    [SerializeField] private string wrongInfoType;
    private float _maximizeBubble = -0.03f;
    private Vector3 _scaleChangeMax;
    private float _minimizeBubble = 0.03f;
    private Vector3 _scaleChangeMin;
    private int _hit = 0;
    public ParticleSystem bubblePop;

    private void Awake()
    {
        _scaleChangeMax = new Vector3(0.03f, 0.03f, 0.03f);
        //_scaleChangeMin = new Vector3(0.03f, 0.03f, 0.03f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("InfoObject"))
        {
            if (!collision.gameObject.transform.name.Contains(correctInfoType))
            {
                _hit += 1;
                transform.localScale += _scaleChangeMax;
               

                //_wrongItemCounter.Equals(0) ? Debug.Log("GAME OVER") : _wrongItemCounter -=1;
            }
           /* if (!collision.gameObject.transform.name.Contains(wrongInfoType))
            {
                
                transform.localScale -= _scaleChangeMin;

            }
           */
         
            if (_hit == 3)
            {

                Destroy(this.gameObject);
                bubblePop.Play();
            }
            collision.gameObject.SetActive(false);
        }
        
        //adblocker and co.
            
    }

   
}
