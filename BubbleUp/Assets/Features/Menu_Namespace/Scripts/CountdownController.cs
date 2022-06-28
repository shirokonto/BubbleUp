using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownController : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject Countdown;

    public int countdownTime;
    public TextMeshProUGUI countdownDisplay;

    private void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);

            countdownTime--;
        }
        countdownDisplay.text = "GO!";

        yield return new WaitForSeconds(1f);
        countdownDisplay.gameObject.SetActive(false);
        Countdown.SetActive(false);
        Canvas.SetActive(true);
    }
}
