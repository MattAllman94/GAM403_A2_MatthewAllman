using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public Text timerText, healthText, ammunitionText;
    public Text alive, killed;
    public Text score;
    public GameObject enemyMenu;
    public GameObject pauseMenuUI, loseMenu;
    
    public static bool gameIsPaused = false;
    

    private float startTime;

    void Start()
    {
        startTime = Time.time;
        hideMenu();

    }


    void Update()
    {
        float t = Time.time - startTime;

        
        string seconds = (t % 60).ToString("f0");

        timerText.text =seconds;
        healthText.text = PlayerMovement.health.ToString();
        ammunitionText.text = PlayerMovement.ammunition.ToString();


        alive.text = SpawnControl.currentAmount.ToString();
        killed.text = SpawnControl.unitKilled.ToString();
        score.text = killed.text + timerText.text;

       


        if (Input.GetKey(KeyCode.Tab))
        {
            showMenu();
        }
        else
        {
            hideMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    public void showMenu()
    {
        enemyMenu.SetActive(true);
    }

    public void hideMenu()
    {
        enemyMenu.SetActive(false);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void LoadMenu()
    {

        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {

        Application.Quit();
    }

    public void GameOver()
    {
        loseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
