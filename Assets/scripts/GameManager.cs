using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float totalPoints = 0f;
    public float lives = 3;
    public Text score;
    public Text remainingLives;
    
    public Text scoreTotal;
    public GameObject howtoPanel;
   
    public GameObject endPanel;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
       
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Gameplay")
        {
            howtoPanel.SetActive(true);
            Paused();
        }

    }



    //menambah point score
    public void AddPoints(float points)
    {
        totalPoints += points;
        score.text = "Score: " + totalPoints.ToString();
        audioManager.PlaySFX(audioManager.scoreIn);
    }

    public void ButtonClick()
    {
        audioManager.PlaySFX(audioManager.click);
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
    public void IncreaseLives(float live)
    {
        if (lives < 3)
        {
            lives += live;
            remainingLives.text = lives.ToString();
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
        remainingLives.text = lives.ToString();
        score.text = "Score: " + totalPoints.ToString();

    }
     
    public void EndGame()
    {
        scoreTotal.text ="Your Score: " + totalPoints.ToString();
        endPanel.SetActive(true);
        audioManager.PlaySFX(audioManager.endgame);
        Paused();
    }

    public void Gameplay()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void Mainmenu()
    {
        SceneManager.LoadScene("Mainmenu");
    }
    public void Exitgame()
    {
        Application.Quit();
    }
}
