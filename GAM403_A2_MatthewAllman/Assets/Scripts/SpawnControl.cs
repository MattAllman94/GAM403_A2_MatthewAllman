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
            if(Time.time < 120) //Spawns Level 1 Enemies
            {
            
                randomSpawnPoint = Random.Range(0, spawnPoints.Length);
                enemy = Random.Range(0, skeleton.Length);
                GameObject e =Instantiate(skeleton[enemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
                currentAmount++;
                
                
        
            }
            else if(Time.time >= 120 && Time.time < 240) //Spawns Level 2 Enemies
            {
                randomSpawnPoint = Random.Range(0, spawnPoints.Length);
                enemy = Random.Range(0, goblin.Length);
                GameObject e =Instantiate(goblin[enemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
                currentAmount++;
                
            }
            else if(Time.time >= 240 && Time.time < 360) //Spawns Level 3 Enemies
            {
                randomSpawnPoint = Random.Range(0, spawnPoints.Length);
                enemy = Random.Range(0, skeletonKnight.Length);
                GameObject e = Instantiate(skeletonKnight[enemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
                currentAmount++;
                
                
            }
            else if(Time.time >= 360 && Time.time < 480) //Spawns Level 4 Enemies
            {
                randomSpawnPoint = Random.Range(0, spawnPoints.Length);
                enemy = Random.Range(0, golem.Length);
                GameObject e = Instantiate(golem[enemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
                currentAmount++;
               
                
                
            }
            else if(Time.time >= 480 && Time.time < 900) //Spawns All Enemies at random
            {
                randomSpawnPoint = Random.Range(0, spawnPoints.Length);
                enemy = Random.Range(0, finalWaves.Length);
                GameObject e = Instantiate(finalWaves[enemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
                currentAmount++;
                
                
               
            }
        }
        
        
    }

   

}
