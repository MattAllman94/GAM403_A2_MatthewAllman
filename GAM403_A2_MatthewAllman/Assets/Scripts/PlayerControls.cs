using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    public float speed, rotationSpeed;
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
    }

    //void Movement()
    //{
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        transform.position += transform.forward * speed * Time.deltaTime;
    //    }
    //    else if (Input.GetKey(KeyCode.S))
    //    {
    //        transform.position -= transform.forward * speed * Time.deltaTime;
    //    }

    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    //    }
    //    else if (Input.GetKey(KeyCode.D))
    //    {
    //        transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
    //    }

    //}
}
