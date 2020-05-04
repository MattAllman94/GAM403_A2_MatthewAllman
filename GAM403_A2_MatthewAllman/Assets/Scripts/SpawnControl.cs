using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject[] skeleton, goblin, skeletonKnight, golem;
    public  int maxUnits;
    public static int currentAmount;

    private int randomSpawnPoint, enemy;
    void Start()
    {
        
        currentAmount = 0;
        InvokeRepeating("SpawnEnemy", 0f, 1f);
    }

    void SpawnEnemy()
    {
        if(currentAmount < maxUnits)
        {
            
            randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            enemy = Random.Range(0, skeleton.Length);
            GameObject e =Instantiate(skeleton[enemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            currentAmount++;
            print(currentAmount);
            Destroy(e, 20);
        
        }
       
    }

}
