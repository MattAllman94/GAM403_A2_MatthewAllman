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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            animator.SetTrigger("doorClose"); 
        }
    }
}
