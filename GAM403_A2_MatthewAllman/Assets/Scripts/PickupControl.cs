using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupControl : MonoBehaviour
{

    public Transform[] pickupSP;
    public GameObject[] pickups;
    public float spawnTime, spawnDelay;

    private bool stopSpawning = false;
    private int randomSpawnPoint, pickup;

    void Start()
    {
        
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay); //Repeats the Spawning of pickups with a delay
    }

    
   

    public void SpawnObject()
    {
        randomSpawnPoint = Random.Range(0, pickupSP.Length);
        pickup = Random.Range(0, pickups.Length);
        GameObject e = Instantiate(pickups[pickup], pickupSP[randomSpawnPoint].position, Quaternion.identity);
        Destroy(e, 20);
        if(stopSpawning)
        {
            CancelInvoke("SpawnObject");
        }
    }
}
