using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public float speed, defaultSpeed, rotationSpeed, brakeSpeed, jumpSpeed, range;
    public GameObject hitBox, swingHitBox, bow, sword, marker;
    public int pHealth;
    public static int ammunition;
    public Text healthText;
    

    private Vector3 move;
    private bool swordActive, bowActive, attack = false, chargedAttack = false, charging = false, grounded = false;
    private float moveAmount;
    private int attackDamage = 1, chargedAttackDamage = 20;
    

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

    private void OnCollisionStay(Collision collision)
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

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            grounded = false;
        }

    }

    public void OnTriggerStay(Collider other) //Damages the enemy
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            print("hit " + other.gameObject.name);
            if(enemy != null)
            {
                if (attack == true)
                {
                    enemy.eHealth = enemy.eHealth - attackDamage;
                    enemy.hit = true;


                }
                else if (chargedAttack == true)
                {
                    enemy.eHealth = enemy.eHealth - chargedAttackDamage;
                    enemy.hit = true;
                }
                else
                {
                    enemy.hit = false;
                }
            }
            
            
        }

        if(other.CompareTag("hPickup")) // Determines the pickup of a health item
        {
            other.gameObject.SetActive(false);
            if(pHealth < 80)
            {
                pHealth = Mathf.Clamp(pHealth +20, 0, 100);
            }
            
                
        }

        if(other.CompareTag("aPickup")) //Determines the pickup of ammunition
        {
            other.gameObject.SetActive(false);
            if (ammunition < 90)
            {
                ammunition = ammunition + 10;
            }
            else
            {
                ammunition = 100;
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

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Shoots a raycast at the mouse position
            if (Physics.Raycast(ray, out hit, range))
            {
                marker.transform.position = hit.point;
                if (Input.GetMouseButtonDown(0) && hit.collider.tag == "Enemy")
                {
                    print("Fire Arrow, hit: " + hit.transform.name);
                    attack = true;

                }
                else
                {
                    attack = false;
                }

                if (Input.GetMouseButton(1))
                {
                    charging = true;
                    print("charging ");

                }
                else if (charging = true && Input.GetMouseButtonUp(1))
                {
                    chargedAttack = true;
                    print("Charged Shot Fired");
                }
                else
                {
                    charging = false;
                    chargedAttack = false;
                }
                
                
            }

            
        }

    }

}
