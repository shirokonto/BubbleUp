using Leap.Unity.Interaction;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Interactables_Namespace.Scripts
{
    public class InfoItemBehaviour : MonoBehaviour
    {
        public Text scoreText;
        public int point = 1;
        public void PrintInfoItemColor()
        {
            Debug.Log("I'm a " + gameObject.name);
        }

        public void DisableItem()
        {
            //gameObject.GetComponent<SphereCollider>().enabled = false;
            //gameObject.SetActive(false, 1f);
        }
        public void AddPoint()
        {
            point += 1;
            scoreText.text = point.ToString() + " POINTS";

        }
    }
}