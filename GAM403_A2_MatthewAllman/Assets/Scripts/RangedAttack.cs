using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public float fireRate = 0.25f, weaponRange = 50f, hitForce = 100f;
    public GameObject marker;

    private float nextFire;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>(); // Access player's script to use the randeg attack stats for different characters.

        if (Input.GetMouseButtonDown(0) && Time.time > nextFire) //when the mouse button is pressed and the time is greater than the time from nextFire variable
        {
            nextFire = Time.time + fireRate; //Sets the delay for the next attack
            
            Ray ray = new Ray(transform.position, transform.right); // The origin of the raycast is set from the player
            RaycastHit hit; //Stores the info as hit
            animator.SetTrigger("Fire");
            
            if (Physics.Raycast(ray, out hit, weaponRange)) // Stores any data within hit as the ray shoots forward in the direction the player is moving.
            {
                marker.transform.position = hit.point; //Uses a marker on screen to see where the raycast is hitting
                if (hit.collider.tag == "Enemy") // If the raycast hits an object tagged as Enemy
                {
                    Enemy enemyHealth = GameObject.Find("Enemy").GetComponent<Enemy>(); // Access the enemy's script to change their health
                    enemyHealth.eHealth = enemyHealth.eHealth - player.rangedAttackDmg; //Deals Damage
                    enemyHealth.hit = true; //Creates Rebound
                    player.ammunition--; // uses one arrow from ammunition
                    print("Arrow fired at " + hit.collider.name);
                }
                else if (hit.collider.CompareTag("Boss"))
                {
                    Boss bossHealth = GameObject.Find("Boss").GetComponent<Boss>();  //Access the boss' script to change its health
                    bossHealth.eHealth = bossHealth.eHealth - player.rangedAttackDmg;
                    bossHealth.hit = true;
                    player.ammunition--;
                }
            }
        }
        
    }

}