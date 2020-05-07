using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupControl : MonoBehaviour
{

    public Transform[] pickupSP;
    public GameObject[] pickups;
    public float spawnTime, spawnDelay;

    private bool stopSpawning = false;
    //private bool pickupActive;
    private int randomSpawnPoint, pickup;

    void Start()
    {
        //pickupActive = false;
        //ammunitionActive = false;
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    
    //void Update()
    //{
    //    //if(pickupActive == false && ammunitionActive == false)
    //    //{
    //    //    randomSpawnPoint = Random.Range(0, pickupSP.Length);
    //    //    pickup = Random.Range(0, pickups.Length);
    //    //    GameObject e = Instantiate(pickups[pickup], pickupSP[randomSpawnPoint].position, Quaternion.identity);
    //    //    pickupActive = true;
    //    //    Destroy(e, 30);
    //    //}
    //    //else
    //    //{
    //    //    pickupActive = false;
    //    //}
    //}

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
