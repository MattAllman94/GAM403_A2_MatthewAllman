﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed, rotationSpeed, brakeSpeed, jumpSpeed;
    public GameObject hitBox, swingHitBox, sword, bow;

    private Vector3 move;
    private bool charging = false, swordActive, bowActive, grounded = false;
    private float maxCharge, currentCharge, moveAmount;

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
    }

    
    void Update()
    {
        Movement();
        Attack();
    }

    private void FixedUpdate()
    {
        rb.AddForce(move);
        if(Mathf.Abs(Input.GetAxis("Vertical")) < 0.25f)
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

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
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

    public void Attack()
    {

        maxCharge = 10;
        currentCharge = 1;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            sword.SetActive(true);
            swordActive = true;
            bow.SetActive(false);
            bowActive = false;
            print("Sword Equipped");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            sword.SetActive(false);
            swordActive = false;
            bow.SetActive(true);
            bowActive = true;
            print("Bow Equipped");
        }

        if (swordActive == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hitBox.SetActive(true);
                print("Stab");
            }
            else
            {
                hitBox.SetActive(false);
            }

            if (Input.GetMouseButton(1))
            {
                charging = true;
                currentCharge = +currentCharge;
                if (currentCharge < maxCharge)
                {
                    print("charging " + currentCharge);
                }
                else if (currentCharge == maxCharge)
                {
                    print("Full Charged");
                }
            }
            else if (charging = true && Input.GetMouseButtonUp(1))
            {
                swingHitBox.SetActive(true);
                print("Charge attack swung");
            }
            else
            {
                charging = false;
                swingHitBox.SetActive(false);
            }
        }
        else if (bowActive == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                print("Fire Arrow");
            }

            if (Input.GetMouseButton(1))
            {
                charging = true;
                currentCharge = +currentCharge;
                if (currentCharge < maxCharge)
                {
                    print("charging " + currentCharge);
                }
                else if (currentCharge == maxCharge)
                {
                    print("Full Charged");
                }
            }
            else if (charging = true && Input.GetMouseButtonUp(1))
            {
                print("Charged Shot Fired");
            }
            else
            {
                charging = false;
            }
        }

    }

}
