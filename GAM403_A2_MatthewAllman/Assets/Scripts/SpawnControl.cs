using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject[] skeleton, goblin, skeletonKnight, golem, finalWaves;
    public  int maxUnits;
    public static int currentAmount, unitKilled;

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
            if(unitKilled < 50)
            {
            
                randomSpawnPoint = Random.Range(0, spawnPoints.Length);
                enemy = Random.Range(0, skeleton.Length);
                GameObject e =Instantiate(skeleton[enemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
                currentAmount++;
                Destroy(e, 20);
                
        
            }
            else if(unitKilled > 50 && unitKilled < 100)
            {
                randomSpawnPoint = Random.Range(0, spawnPoints.Length);
                enemy = Random.Range(0, goblin.Length);
                GameObject e =Instantiate(goblin[enemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
                currentAmount++;
                Destroy(e, 20);
            }
            else if(unitKilled > 100 && unitKilled < 150)
            {
                randomSpawnPoint = Random.Range(0, spawnPoints.Length);
                enemy = Random.Range(0, skeletonKnight.Length);
                GameObject e = Instantiate(skeletonKnight[enemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
                currentAmount++;
                Destroy(e, 20);
                
            }
            else if(unitKilled > 150 && unitKilled < 200)
            {
                randomSpawnPoint = Random.Range(0, spawnPoints.Length);
                enemy = Random.Range(0, golem.Length);
                GameObject e = Instantiate(golem[enemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
                currentAmount++;
               
                Destroy(e, 20);
                
            }
            else if(unitKilled > 200)
            {
                randomSpawnPoint = Random.Range(0, spawnPoints.Length);
                enemy = Random.Range(0, finalWaves.Length);
                GameObject e = Instantiate(finalWaves[enemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
                currentAmount++;
                
                Destroy(e, 20);
               
            }
        }
        
        
    }

}
