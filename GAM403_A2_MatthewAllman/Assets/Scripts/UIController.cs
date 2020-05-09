using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public Text timerText, ammunitionText;
    public Text alive, killed;
    public Text score, winScore;
    public GameObject enemyMenu;
    public GameObject pauseMenuUI, loseMenu, winMenu, characterSelect;
    public GameObject knight, dwarf, elf;
    
    
    public static bool gameIsPaused = false;
    

    private float startTime;

    void Start()
    {
        startTime = Time.time;
        hideMenu();
        Time.timeScale = 0f;
        
        
    }


    void Update()
    {
        PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();

        float t = Time.time - startTime;

        string minutes = ((int) t / 60).ToString();
        string seconds = (t % 60).ToString("f0");

        timerText.text = minutes + ":" + seconds; // Displays the time in minutes and seconds
        
        ammunitionText.text = player.ammunition.ToString(); // Displays the ammunition


        alive.text = SpawnControl.currentAmount.ToString(); //Displays the amount of enemies alive
        killed.text = SpawnControl.unitKilled.ToString(); // displays the amount of enemies killed
        score.text = killed.text + timerText.text; // Displays the final score in the Game Over Screen
        winScore.text = killed.text + timerText.text + 1000.ToString(); // Displays the final score in the Win Screen
       


        if (Input.GetKey(KeyCode.Tab)) //Enemy Menu when Tab is held
        {
            showMenu();
        }
        else
        {
            hideMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) // Pauses the Game when ESC is pressed
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

    public void Resume() // Resumes the Game (Pause menu is toggled off)
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;

    }

    void Pause() //Pauses the game
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;

    }

    public void Restart() //Restarts the game
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void LoadMenu() //Goes back to the main menu
    {

        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void QuitGame() //Quits the game
    {

        Application.Quit();
    }

    public void GameOver() //Displays the Game Over Screen
    {
        loseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Win()
    {
        winMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Knight()
    {
        knight.SetActive(true);
        dwarf.SetActive(false);
        elf.SetActive(false);
        Time.timeScale = 1f;
        characterSelect.SetActive(false);

    }

    public void Dwarf()
    {
        knight.SetActive(false);
        dwarf.SetActive(true);
        elf.SetActive(false);
        Time.timeScale = 1f;
        characterSelect.SetActive(false);
    }

    public void Elf()
    {
        knight.SetActive(false);
        dwarf.SetActive(false);
        elf.SetActive(true);
        Time.timeScale = 1f;
        characterSelect.SetActive(false);
    }
}
