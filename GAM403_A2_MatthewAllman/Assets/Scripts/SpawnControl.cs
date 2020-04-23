using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{

    public float spawnRate, spawnDelay;
    public Spawner[] spawns; //dont have to use getcomponent, access immediate script
    public int unitCount, minUnits, maxUnits;

    void Start()
    {
        
        spawns = GameObject.FindObjectsOfType<Spawner>(); //allows spawns to be added to the array automatically
        //InvokeRepeating("SelectSpawn", spawnDelay, spawnRate);
        Invoke("SelectSpawn", spawnRate);
        //StartCoroutine("SpawnRoutine");
    }


    void SelectSpawn()
    {
        float currentRate = spawnRate;
        if(unitCount < maxUnits)
        {
            int random = Random.Range(0, spawns.Length);
            spawns[random].Spawn();
            unitCount++;
        }

        if(unitCount < minUnits)
        {
            currentRate /= 2;
        }

        Invoke("SelectSpawn", currentRate);

        
    }

    IEnumerator SpawnRoutine()
    {
        int random = Random.Range(0, spawns.Length);
        print(random);
        spawns[random].Spawn();
        yield return new WaitForSeconds(spawnRate); //Delay
        StartCoroutine("SpawnRoutine");
    }
}
