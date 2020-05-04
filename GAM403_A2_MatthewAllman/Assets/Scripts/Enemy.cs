using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public float speed;
    public NavMeshAgent agent;
    public float minDist, maxDist;
    public SpawnControl currentAmount;
    

    private Transform player;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        currentAmount = GameObject.Find("SpawnPoints").GetComponent<SpawnControl>(); 
    }


    void Update()
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
    }

    private void OnDestroy()
    {
        SpawnControl.currentAmount--;
        SpawnControl.unitKilled++;
    }
}
