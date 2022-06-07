using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BubbleBehaviour : MonoBehaviour
{
    [SerializeField] private string correctInfoType; //will be set before the game starts via character view
    private int _temporaryCount;
    private float _maximizeBubble = -0.03f;

    private int _wrongItemCounter = 3;
    //add minimizebubble float for minimizeBubbleitem
    private Vector3 _scaleChange;
    private int hit = 0;

    private void Awake()
    {
        _temporaryCount = 0;
        _scaleChange = new Vector3(_maximizeBubble, _maximizeBubble, _maximizeBubble);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("InfoObject"))
        {
            if (collision.gameObject.transform.name.Contains(correctInfoType))
            {
                _temporaryCount += 1;
                Debug.Log("( + ) TemporaryCount: " + _temporaryCount + " Nice!!! ");
            }
            else
            {
                hit += 1;
                Debug.Log(hit);
                _temporaryCount -= 1;
                transform.localScale -= _scaleChange;
                
                //_wrongItemCounter.Equals(0) ? Debug.Log("GAME OVER") : _wrongItemCounter -=1;
                //var result = x > y ? "x is greater than y" : "x is less than y";
                //Console.WriteLine((_wrongItemCounter == 0) ? _wrongItemCounter true : false);

                Debug.Log("( - ) TemporaryCount: " + _temporaryCount + " :-( ");
                
                //Destroy(this.gameObject);
            }
            if(hit == 3)
            {
                Destroy(this.gameObject);
            }
            collision.gameObject.SetActive(false);
        }
        
        //adblocker and co.
            
    }
   
}
