﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Core Components")]
    Rigidbody rb;
    Animator anim;

    [Header("Horizontal Movement")]
    public Joystick joystick;
    public float horizontalSpeed;
    public float minX;
    public float maxX;
    private float horizontalMove = 0f;

    [Header("Forwards Movement")]
    public float currentVelocity;
    public float maximumVelocity;
    public float initialThrustValue;
    public float thrustValue;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        rb.AddForce(Vector3.forward * initialThrustValue);
        InvokeRepeating("RaiseMaximumVelocity", 1f, 1f);
    }

    void Update()
    {
        //transform.position += currentVelocity * Vector3.forward * Time.deltaTime;
        IncreaseVelocity();
        anim.SetFloat("Roll", joystick.Horizontal);

        if (joystick.Horizontal < Mathf.Epsilon || joystick.Horizontal > Mathf.Epsilon)
        {
            if(transform.position.x >= minX && transform.position.x <= maxX)
            {
                
                horizontalMove = joystick.Horizontal * horizontalSpeed * Time.deltaTime;
                transform.position += Vector3.right * horizontalMove;
            } 

            if(transform.position.x < minX)
            {
                transform.position = new Vector3(minX, transform.position.y, transform.position.z);
            }

            if (transform.position.x > maxX)
            {
                transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
            }
        }
         
    }

    private void IncreaseVelocity()
    {
        if(rb.velocity.magnitude < maximumVelocity)
        {
            rb.AddForce(Vector3.forward * thrustValue);
        }
    }

    private void RaiseMaximumVelocity()
    {
        maximumVelocity++;
    }
}
