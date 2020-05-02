using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{

    public float spawnRate, spawnDelay;
    public Spawner[] spawns;
    public int unitCount, minUnits, maxUnits;
    public static int unitAlive;

    void Start()
    {
        
        spawns = GameObject.FindObjectsOfType<Spawner>();
        //InvokeRepeating("SelectSpawn", spawnDelay, spawnRate);
        Invoke("SelectSpawn", spawnRate);
        
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

        unitAlive = unitCount;

        
    }

    IEnumerator SpawnRoutine()
    {
        int random = Random.Range(0, spawns.Length);
        spawns[random].Spawn();
        yield return new WaitForSeconds(spawnRate);
        StartCoroutine("SpawnRoutine");
    }
}
