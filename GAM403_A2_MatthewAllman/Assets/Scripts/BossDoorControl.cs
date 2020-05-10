using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorControl : MonoBehaviour
{

    private Animator animator;
    public GameObject boss;


    private bool doorClosed = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        boss.SetActive(false);
    }

    private void Update()
    {
        if (Time.time >= 900 && !doorClosed)
        {
            animator.SetTrigger("Open");
            
        }
        
        if (doorClosed)
        {
            boss.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            animator.SetTrigger("Close");
            doorClosed = true;
            
        }
        
    }

   
}

