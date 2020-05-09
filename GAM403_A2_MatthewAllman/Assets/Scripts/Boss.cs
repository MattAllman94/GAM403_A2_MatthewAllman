using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{

    public float speed, attackRate;
    public NavMeshAgent agent;
    public float minDist, maxDist;
    public SpawnControl currentAmount;
    public int eHealth, attackDmg;
    public bool hit = false;
    
    

    private float nextAttack;
    private Transform player;
    private Animator animator;

    Rigidbody rb;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        currentAmount = GameObject.Find("SpawnPoints").GetComponent<SpawnControl>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }


    void Update() //Targets the player and moves towards them using NavMesh
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance  > minDist && distance < maxDist)
        {
            agent.destination = player.transform.position;
        }
        else
        {
            agent.ResetPath();
        }

        if(hit == true)
        {
          
            rb.AddForce(transform.forward * -10f, ForceMode.Impulse);
            
            hit = false;
        }
        
        if (eHealth <= 0) // Kills the Boss when their health is 0
        {

            gameObject.SetActive(false);
            GameObject.FindObjectOfType<UIController>().Win();

        }
        
        
        
    }

    public void OnTriggerStay(Collider other) // Attacks the player
    {
        if(other.CompareTag("Player"))
        {
            PlayerMovement health = other.GetComponent<PlayerMovement>();
            if (Time.time > nextAttack)
            {
                health.pHealth = health.pHealth - attackDmg;
                nextAttack = Time.time + attackRate;
                rb.AddForce(transform.forward * -5f, ForceMode.Impulse);
                animator.SetTrigger("Attack");
            }
           

        }
    }


}
