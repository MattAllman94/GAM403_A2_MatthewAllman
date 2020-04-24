using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{

    private NavMeshAgent agent;
    private GameObject player;
    public float minDist, maxDist;
    public SpawnControl sc;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("PlayerStandin");
        sc = GameObject.Find("SpawnPoints").GetComponent<SpawnControl>();
    }

  
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance >= minDist && distance <= maxDist)
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
        sc.unitCount--;
    }
}
