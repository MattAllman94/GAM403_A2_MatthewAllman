using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public Text timerText;
    public Text alive, killed;
    public GameObject[] enemyMenu;
    
    private float startTime;

    void Start()
    {
        startTime = Time.time;
        enemyMenu = GameObject.FindGameObjectsWithTag("EnemyMenu");
        hideMenu();

    }


    void Update()
    {
        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f0");

        timerText.text = minutes + ":" + seconds;

        alive.text = SpawnControl.currentAmount.ToString();
        killed.text = SpawnControl.unitKilled.ToString();


        if (Input.GetKey(KeyCode.Tab))
        {
            showMenu();
        }
        else
        {
            hideMenu();
        }

    }


    public void showMenu()
    {
        foreach (GameObject g in enemyMenu)
        {
            g.SetActive(true);
        }
    }

    public void hideMenu()
    {
        foreach (GameObject g in enemyMenu)
            g.SetActive(false);
    }
}
