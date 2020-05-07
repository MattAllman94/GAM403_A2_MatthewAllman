using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed, rotationSpeed, brakeSpeed, jumpSpeed;
    public GameObject hitBox, swingHitBox, bow, sword;
    public static int health, ammunition;

    private Vector3 move;
    private bool swordActive, bowActive, attack = false, chargedAttack = false, charging = false, grounded = false;
    private float moveAmount;
    private int attackDamage = 10, chargedAttackDamage = 20;

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
        

        health = 100;
        ammunition = 20;
    }


    void Update()
    {
        Movement();
        Attack();
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                sword.SetActive(true);
                swordActive = true;
                bow.SetActive(false);
                bowActive = false;

            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                sword.SetActive(false);
                swordActive = false;
                bow.SetActive(true);
                bowActive = true;

            }

        if (health <= 0)
        {
            GameObject.FindObjectOfType<UIController>().GameOver();
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(move);
        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.25f)
        {
            rb.velocity = Vector3.MoveTowards(rb.velocity, Vector3.Scale(rb.velocity, Vector3.up), brakeSpeed * Time.deltaTime);
        }
    }


    void Movement()
    {
        move = transform.forward * Input.GetAxis("Vertical") * speed;

        Vector3 rot = Vector3.up * Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(rot);

        moveAmount = speed * Time.deltaTime;
        Vector3 movement = (Input.GetAxis("Horizontal") * -Vector3.left * moveAmount) + (Input.GetAxis("Vertical") * Vector3.forward * moveAmount);
        rb.AddForce(movement, ForceMode.Force);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpSpeed);

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (CompareTag("Floor"))
        {
            grounded = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("enemy"))
        {
            if(attack == true)
            {
                Enemy.health = Enemy.health - attackDamage;
                print("Enemy took Damage");
            }
            else if(chargedAttack == true)
            {
                Enemy.health = Enemy.health - chargedAttackDamage;
            }
            
        }

    }

    public void Attack()
    {


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            sword.SetActive(true);
            swordActive = true;
            bow.SetActive(false);
            bowActive = false;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            sword.SetActive(false);
            swordActive = false;
            bow.SetActive(true);
            bowActive = true;

        }

        if (swordActive == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hitBox.SetActive(true);
                attack = true;
                
                print("Stab");
            }
            else
            {
                hitBox.SetActive(false);
                attack = false;
            }

            if (Input.GetMouseButton(1))
            {
                charging = true;
                print("charging ");
            }
            else if (charging = true && Input.GetMouseButtonUp(1))
            {
                swingHitBox.SetActive(true);
                chargedAttack = true;
                print("Charge attack swung");
            }
            else
            {
                charging = false;
                swingHitBox.SetActive(false);
                chargedAttack = false;
            }
        }
        else if (bowActive == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                print("Fire Arrow");
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
