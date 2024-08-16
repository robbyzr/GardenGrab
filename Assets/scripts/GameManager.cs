using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float totalPoints = 0f;
    public float lives = 3;
    public float timerDuration = 30f; //set timer 30sec
    public Text score;
    public Text remainingLives;
    public Text timer;
    public Text scoreTotal;

    public GameObject startPanel;
    public GameObject endPanel;

    private void Start()
    {
        startPanel.SetActive(true);
        Paused();
    }

    private void Update()
    {
        // Update timer setiap frame
        if (timerDuration > 0)
        {
            timerDuration -= Time.deltaTime;
            UpdateTimerUI();

            // cek jika waktu sudah habis
            if (timerDuration <= 0)
            {
                EndGame(); 
            }
        }
    }
    //update timer
    private void UpdateTimerUI()
    {
        timer.text = "Time: " + Mathf.Ceil(timerDuration).ToString();
    }
    //menambah point score
    public void AddPoints(float points)
    {
        totalPoints += points;
        score.text = "Score: " + totalPoints.ToString();
    }
    //mengurangi nyawa
    public void ReduceLives(float live)
    {
        lives -= live;
        remainingLives.text = lives.ToString();
        if (lives <= 0)
        {
            EndGame();
        }
    }
    public void Resume()
    {
        Time.timeScale = 1.0f;
    }
    public void Paused()
    {
        Time.timeScale = 0.0f;
    }

    
    public void ResetGame()
    {
        lives = 3;
        totalPoints = 0f;
        timerDuration = 30f;
        remainingLives.text = lives.ToString();
        score.text = "Score: " + totalPoints.ToString();

    }
        
    public void EndGame()
    {
        scoreTotal.text ="Your Score: " + totalPoints.ToString();
        endPanel.SetActive(true);
        
        Paused();
    }
}
