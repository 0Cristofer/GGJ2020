using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float Seconds;
    float Minutes;
    float MiliSeconds;

    [SerializeField]
    private float timerToEnd;

    [SerializeField]
    private float timerToEmergency;

    [SerializeField]
    Text WhatchText;

    [SerializeField]
    private Animator TimerAnimator;



    // Update is called once per frame
    void Update()
    {
        if (timerToEnd >= 0)
        { 
        timerToEnd -= Time.deltaTime;
        Seconds = timerToEnd % 60;
        Minutes = (timerToEnd / 60);
        MiliSeconds = (timerToEnd * 99) % 99;

            WhatchText.text = ((int)Minutes).ToString("00") + ":" + ((int)Seconds).ToString("00") + ":" + ((int)MiliSeconds).ToString("00");
        }
        else
        {
            
        print("EndOfGame");
    }


        if(timerToEnd <= 0)
        {
            print("EndOfGame");
        }

        if(timerToEmergency >= timerToEnd)
        {
            TimerAnimator.Play("TimerEnding");
        }

    }
}
