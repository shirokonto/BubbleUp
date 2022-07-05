using DataStructures.Variables;
using TMPro;
using UnityEngine;

namespace Features.UI_Namespace.Script
{
    public static ScoreManager instance;
    [SerializeField] private IntVariable points;
    [SerializeField] private BoolVariable hitBubble;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreTextGO;
    public GameObject[] life;
    private bool dead;
    private int tempPoints;
    private int dam = 1;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        tempPoints = life.Length;
        scoreText.text = points.Get() + " POINTS";
    }


    void Update()
    {
        Hitbubble();
        if(dead == true)
        {
            scoreText.text = points.Get() + " POINTS";
        }
        
        

        //scoreText.text = points.Get() + " POINTS";
        scoreTextGO.text = points.Get() + " POINTS";
    }

    public void Hitbubble()
    {
        if (hitBubble.Get())
        {
            hitBubble.Set(false);
            Debug.Log("hit!");

            if (tempPoints >= 1)
            {
                tempPoints -= dam;

                Destroy(life[tempPoints].gameObject);

                Debug.Log("Life Destroyed");

                if (tempPoints < 1)
                {
                      dead = true;
                }
                
            }
        }
    }

    /*
    public void TakeDamage(int dam)
    {
        if (hitBubble.Get() )
        {
            Debug.Log("hit!");
            if (tempPoints >= 1)
            {
                tempPoints -= dam;
                
                Destroy(life[tempPoints].gameObject);

                  if (tempPoints < 1)
                  {
                      dead = true;
                  }
                
            }
        }
    }
    */
   
}
