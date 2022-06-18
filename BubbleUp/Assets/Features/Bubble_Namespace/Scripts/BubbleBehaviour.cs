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
            }
            if(_hit == 3)
            {
                bubblePop.Play();
                Destroy(this.gameObject);
                Debug.Log("Bubble Pop!");
                
            }
            collision.gameObject.SetActive(false);
        }
        
        //adblocker and co.
            
    }
   
}
