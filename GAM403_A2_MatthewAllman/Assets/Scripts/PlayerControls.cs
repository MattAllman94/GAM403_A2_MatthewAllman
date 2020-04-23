using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    public float speed, rotationSpeed, brakeSpeed;
    Rigidbody rb;
    public Vector3 move;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        Movement();
    }

    void Movement()
    {
        move = transform.forward * Input.GetAxis("Vertical") * speed;
                   
        
        Vector3 rot = Vector3.up * Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(rot);
    }

    private void FixedUpdate()
    {
        rb.AddForce(move);
        if(Mathf.Abs(Input.GetAxis("Vertical")) < 0.25f)
        {
            rb.velocity = Vector3.MoveTowards(rb.velocity, Vector3.Scale(rb.velocity, Vector3.up), brakeSpeed * Time.deltaTime);
        }
    }

  
}
