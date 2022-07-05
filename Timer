using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class scr_Timer : MonoBehaviour
{
    public float timeRemaining = 90;
    public bool timerIsRunning = false;
    public Text timeText;

    public GameObject GameOver;

    public string currentLevel;

    void Start()
    {
        timerIsRunning = true;
        GameOver.gameObject.SetActive(false);
        currentLevel = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else 
        {
            GameOver.gameObject.SetActive(true);
            timeRemaining = 0;
            timerIsRunning = false;

            Thread.Sleep(3000);
            RestartLevel();
            
        }
    }

    public void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = "Time Remaining: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentLevel);
    }



}


