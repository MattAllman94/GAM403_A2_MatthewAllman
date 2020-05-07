using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDoorControl : MonoBehaviour
{
    private Animator animator;
    public GameObject player;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (player)
        {
            animator.SetTrigger("doorClose"); 
        }
    }
}
