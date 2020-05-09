using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDoorControl : MonoBehaviour
{
    private Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) // closes the door as the player exits the start point
    {
        
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("Close"); 
        }
        
    }
}
