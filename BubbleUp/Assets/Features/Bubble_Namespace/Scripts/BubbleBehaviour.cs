using System.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BubbleBehaviour : MonoBehaviour
{
    [SerializeField] private string correctInfoType; //will be set before the game starts via character view
    private const float BUBBLE_SCALING = 0.03f;
    public ParticleSystem bubblePop;
    private Vector3 _scaleChange;
    private int _hit = 0;
    private bool _adBlockerEnabled;

    private void Awake()
    {
        _scaleChange = new Vector3(BUBBLE_SCALING, BUBBLE_SCALING, BUBBLE_SCALING);
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "InfoObject": 
                HitInfoItem(collision.gameObject);
                break;
            case "Ad":
                HitAdItem();
                break;
            case "AdBlocker":
                HitAdBlocker();
                break;
            case "MinimizeBubble":
                HitMinimizeBubble(collision.gameObject);
                break;
            case "SlowMo":
                HitSlowMo(collision.gameObject);
                break;
        }
    }
    private void HitInfoItem(GameObject infoItem){
        if (!infoItem.transform.name.Contains(correctInfoType))
        {
            _hit += 1;
            transform.localScale += _scaleChange;
            //_wrongItemCounter.Equals(0) ? Debug.Log("GAME OVER") : _wrongItemCounter -=1;
        }
        if(_hit == 3)
        {
            Destroy(this.gameObject);
        }
        infoItem.SetActive(false);
    }
    
    private void HitAdItem()
    {
        if (!_adBlockerEnabled) //if adBlocker is NOT valid let bubble be transparent
        {
            StartCoroutine(SetTemporaryAd(2f, false));
        }
    }

    //TODO: call in HandBehaviour.cs
    public void HitAdBlocker()
    {
        //set temporary ad blocker
        StartCoroutine(SetTemporaryAdBlocker(3f, true));

    }
    
    //TODO: call in HandBehaviour.cs
    public void HitMinimizeBubble(GameObject minimizeItem)
    {
        Debug.Log("Hit minimize bubble item");
        if (_hit > 0 || _hit < 3)
        {
            _hit -= 1;
            transform.localScale -= _scaleChange;
        }
        Destroy(minimizeItem, 1);
    }
    
    //TODO: call in HandBehaviour.cs
    public void HitSlowMo(GameObject slowMoObject)
    {
        //Change speed in itemMover moveSpeed only on the one route that slowmo item was on
        GameObject parent = slowMoObject.GetComponentInParent<GameObject>();
        GameObject[] itemsOnSameRoute = parent.GetComponentsInChildren<GameObject>();
            foreach (var item in itemsOnSameRoute)
            {
                item.GetComponent<ItemMover>().SetMoveSpeed(0.25f);
            }
            //wait 2-4 seconds
    }

    private IEnumerator SetTemporaryAdBlocker(float duration, bool status)
    {
        _adBlockerEnabled = status;
        
        yield return new WaitForSeconds(duration);

        _adBlockerEnabled = !_adBlockerEnabled;
    }
    
    private IEnumerator SetTemporaryAd(float duration, bool status)
    {
        gameObject.GetComponent<SphereCollider>().enabled = status;
        
        yield return new WaitForSeconds(duration);

        gameObject.GetComponent<SphereCollider>().enabled = !status;
    }
}
