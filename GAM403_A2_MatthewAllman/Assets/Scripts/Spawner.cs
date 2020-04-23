using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject enemy;

   
    public void Spawn() //Apply to rts with onclick spawning buildings or units
    {
        GameObject e = Instantiate(enemy, transform.position, transform.rotation);
        Destroy(e, 5); //Remove for player kills

    }

}
