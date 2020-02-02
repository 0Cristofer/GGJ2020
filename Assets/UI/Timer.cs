using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float Seconds;
    float Minutes;
    float MiliSeconds;

    [SerializeField] private float timerToStart;
    private bool Started = false;

    [SerializeField] private float timerToEnd;

    [SerializeField] private float timerToEmergency;

    [SerializeField] Text WhatchText;

    [SerializeField] private Animator TimerAnimator;
    [SerializeField] private GameController.GameController _gameController;
    private bool _gameEnded = false; 


    void Update()
    {
        if (!Started)
        {
            timerToStart -= Time.deltaTime;
            if (timerToStart <= 0)
            {
                Started = true;
            }
        }

        if (timerToEnd >= 0 && Started)
        {
            timerToEnd -= Time.deltaTime;
            Seconds = timerToEnd % 60;
            Minutes = (timerToEnd / 60);
            MiliSeconds = (timerToEnd * 99) % 99;

            WhatchText.text = ((int) Minutes).ToString("00") + ":" + ((int) Seconds).ToString("00") + ":" +
                              ((int) MiliSeconds).ToString("00");
        }
        
        if ((timerToEnd <= 0) && (!_gameEnded))
        {
            _gameEnded = true;
            _gameController.EndGame();
        }

        if (timerToEmergency >= timerToEnd)
        {
            TimerAnimator.Play("TimerEnding");
        }
    }
}