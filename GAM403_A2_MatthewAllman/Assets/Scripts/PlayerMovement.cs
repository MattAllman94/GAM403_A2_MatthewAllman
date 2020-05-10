using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public float speed, defaultSpeed, rotationSpeed, brakeSpeed, jumpSpeed, range;
    public GameObject hitBox, swingHitBox, bow, sword;
    public int pHealth;
    public int ammunition;
    public Text healthText, ammunitionText;
    public  int meleeAttackDmg, meleeChargeDmg, rangedAttackDmg, rangedChargeDmg;
    public bool swordActive, bowActive, attack = false, chargedAttack = false, charging = false, grounded = false;
    public float fireRate = 0.25f, weaponRange = 50f, hitForce = 100f;
    public GameObject marker;

    private Vector3 move;
    private float moveAmount, nextFire;
    private Animator animator;
   
    

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        hitBox.SetActive(false);
        swingHitBox.SetActive(false);
        sword.SetActive(true);
        swordActive = true;
        bow.SetActive(false);
        bowActive = false;
        

        animator = GetComponent<Animator>();

        ammunition = 20;
    }


    void Update()
    {
        Movement();
        Attack();
        
        if (Input.GetKeyDown(KeyCode.Alpha1)) //Sets sword when 1 is pressed
            {
                sword.SetActive(true);
                swordActive = true;
                bow.SetActive(false);
                bowActive = false;

            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) //Sets bow when 2 is pressed
            {
                sword.SetActive(false);
                swordActive = false;
                bow.SetActive(true);
                bowActive = true;

            }

        if (pHealth <= 0) //Player dies = Game Over
        {
            GameObject.FindObjectOfType<UIController>().GameOver();
        }

        
        healthText.text = pHealth.ToString();  //Displays the current health of the player
        ammunitionText.text = ammunition.ToString(); // Displays the ammunition
    }

    private void FixedUpdate() //Applies Force for Movement
    {
        rb.AddForce(move);
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.25f)
        {
            rb.velocity = Vector3.MoveTowards(rb.velocity, Vector3.Scale(rb.velocity, Vector3.up), brakeSpeed * Time.deltaTime);
        }
    }


    void Movement() //Controls all the player's movement
    {
        move = transform.forward * Input.GetAxis("Vertical") * speed;

        Vector3 rot = Vector3.up * Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(rot);

        moveAmount = speed * Time.deltaTime;
        Vector3 movement = (Input.GetAxis("Vertical") * transform.forward * moveAmount);
        //rb.AddForce(movement, ForceMode.Force);
        rb.MovePosition(transform.position + movement);

        if (Input.GetKeyDown(KeyCode.Space) && grounded) // Jumps when space is pressed
        {
            
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            grounded = false;
        }
    }

    private void OnCollisionStay(Collision collision) //Checks that the player has landed before being able to jump again
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            grounded = true;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            grounded = true;
        }

    }

    private void OnCollisionExit(Collision collision) // Stops double jump
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            grounded = false;
        }

    }

    public void OnTriggerStay(Collider other) //Damages the enemy
    {
        if (other.CompareTag("Enemy")) // Controls damage over the enemies
        {
            Enemy enemy = other.GetComponent<Enemy>();
            
            if(enemy != null)
            {
                if (attack == true)
                {
                    enemy.eHealth = enemy.eHealth - meleeAttackDmg;
                    enemy.hit = true;


                }
                else if (chargedAttack == true)
                {
                    enemy.eHealth = enemy.eHealth - meleeChargeDmg;
                    enemy.hit = true;
                }
                else
                {
                    enemy.hit = false;
                }
            }
            
            
        }

        if (other.CompareTag("Boss"))  // Controls damage over the boss
        {
            Boss enemy = other.GetComponent<Boss>();
            
            if (enemy != null)
            {
                if (attack == true)
                {
                    enemy.eHealth = enemy.eHealth - meleeAttackDmg;
                    enemy.hit = true;


                }
                else if (chargedAttack == true)
                {
                    enemy.eHealth = enemy.eHealth - meleeChargeDmg;
                    enemy.hit = true;
                }
                else
                {
                    enemy.hit = false;
                }
            }


        }

        if (other.CompareTag("hPickup")) // Determines the pickup of a health item
        {
            other.gameObject.SetActive(false);
            if(pHealth < 100)
            {
                pHealth = Mathf.Clamp(pHealth +20, 0, 100);
            }
            
                
        }

        if(other.CompareTag("aPickup")) //Determines the pickup of ammunition
        {
            other.gameObject.SetActive(false);
            if (ammunition < 100)
            {
                ammunition = Mathf.Clamp(ammunition + 10, 0, 100);
            }
           
        }

        if(other.CompareTag("sPickup")) // Determines the pickup of Temporary speed
        {
            other.gameObject.SetActive(false);
            StartCoroutine("SpeedBoost");
        }
    
    }

    IEnumerator SpeedBoost()
    {
        speed = defaultSpeed * 2;
        yield return new WaitForSeconds(5);
        speed = defaultSpeed;
    }

    public void Attack() //Basic Attack Sequences
    {

        if (swordActive == true) // Controls Sword Attacks
        {
            if (Input.GetMouseButtonDown(0))
            {
                hitBox.SetActive(true);
                attack = true;
                animator.SetTrigger("Attack"); //Attack animation

            }
            else
            {
                hitBox.SetActive(false);
                attack = false;
            }

            if (Input.GetMouseButton(1))
            {
                charging = true;
                
            }
            else if (charging = true && Input.GetMouseButtonUp(1))
            {
                swingHitBox.SetActive(true);
                chargedAttack = true;
                animator.SetTrigger("Charged Attack"); // swing animation

            }
            else
            {
                charging = false;
                swingHitBox.SetActive(false);
                chargedAttack = false;
            }
        }
        else if (bowActive == true && ammunition > 0) // Controls Bow Attacks
        {

            if (Input.GetMouseButtonDown(0) && Time.time > nextFire) //when the mouse button is pressed and the time is greater than the time from nextFire variable
            {
                nextFire = Time.time + fireRate; //Sets the delay for the next attack

                Ray ray = new Ray(transform.position, transform.forward); // The origin of the raycast is set from the player
                RaycastHit hit; //Stores the info as hit
                animator.SetTrigger("Fire");

                if (Physics.Raycast(ray, out hit, weaponRange)) // Stores any data within hit as the ray shoots forward in the direction the player is moving.
                {
                    marker.transform.position = hit.point; //Uses a marker on screen to see where the raycast is hitting
                    if (hit.collider.CompareTag("Enemy")) // If the raycast hits an object tagged as Enemy
                    {
                        Enemy enemyHealth = GameObject.Find("Enemy").GetComponent<Enemy>(); // Access the enemy's script to change their health
                        enemyHealth.eHealth = enemyHealth.eHealth - rangedAttackDmg; //Deals Damage
                        enemyHealth.hit = true; //Creates Rebound
                        ammunition--; // uses one arrow from ammunition
                        print("Arrow fired at " + hit.collider.name);
                    }
                    else if (hit.collider.CompareTag("Boss"))
                    {
                        Boss bossHealth = GameObject.Find("Boss").GetComponent<Boss>();  //Access the boss' script to change its health
                        bossHealth.eHealth = bossHealth.eHealth - rangedAttackDmg;
                        bossHealth.hit = true;
                        ammunition--;
                    }
                }
            }

            if (Input.GetMouseButtonDown(1) && Time.time > nextFire) //Charge Range Attack (Same as above)
            {
                nextFire = Time.time + fireRate;

                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;
                animator.SetTrigger("Fire");

                if (Physics.Raycast(ray, out hit, weaponRange))
                {
                    marker.transform.position = hit.point;
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        Enemy enemyHealth = GameObject.Find("Enemy").GetComponent<Enemy>();
                        enemyHealth.eHealth = enemyHealth.eHealth - rangedChargeDmg;
                        enemyHealth.hit = true;
                        ammunition--;
                        print("Arrow fired at " + hit.collider.name);
                    }
                    else if (hit.collider.CompareTag("Boss"))
                    {
                        Boss bossHealth = GameObject.Find("Boss").GetComponent<Boss>();
                        bossHealth.eHealth = bossHealth.eHealth - rangedChargeDmg;
                        bossHealth.hit = true;
                        ammunition--;
                    }
                }
            }


        }

    }

}
