﻿using UnityEngine;

/// <summary>
/// Mining cart behavior
/// </summary>
public class Miningcart : MonoBehaviour
{
    // accelaration support
    const float Acceleration = 25f;
    const float MaxVelocity = 40f;
    float accelerationInput;

    // speed clamping support
    bool needsToAccelerateForward = false;
    bool needsToAccelerateReverse = false;

    // jump support
    const float JumpForce = 1000f;
    float jumpInput;
    bool onGround = false;
    bool jumpApplied = false;

    // rotation support
    float horizontalInput;
    const float RotationForce = 1000f;

    // for efficiency
    Rigidbody rb;
    
    /// <summary>
    /// Gets RigidBody Components
    /// </summary>
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Handles speed clamping, input for accelaration, rotation and jumping 
    /// </summary>
    private void Update()
    {
        // makes sure it can only accelarate while not at max speed
        if(rb.velocity.x < MaxVelocity)
        {
            needsToAccelerateForward = true;
        }

        if(rb.velocity.x > -MaxVelocity)
        {
            needsToAccelerateReverse = true;
        }

        // updating input
        jumpInput = Input.GetAxis("JumpMiningcart");
        accelerationInput = Input.GetAxis("AccelerateMiningcart");
        horizontalInput = Input.GetAxis("Horizontal");
    }

    /// <summary>
    /// Applies physic movement, accelaration and jumping
    /// </summary>
    private void FixedUpdate()
    {
        if (onGround)
        {
            // handling acceleration input
            if(needsToAccelerateForward && accelerationInput > 0)
            {
                rb.AddForce(transform.forward * accelerationInput * Acceleration, ForceMode.Acceleration);
                needsToAccelerateForward = false;
            }

            if(needsToAccelerateReverse && accelerationInput < 0)
            {
                rb.AddForce(transform.forward * accelerationInput * Acceleration, ForceMode.Acceleration);
                needsToAccelerateReverse = false;
            }

            // handling jumpinput
            if (jumpInput != 0 && !jumpApplied)
            {
                rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
                rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
                jumpApplied = true;
            }

        }

        if (jumpInput == 0)
            jumpApplied = false;

        // handling mid air rotation
        if (horizontalInput != 0 && !onGround)
        {
            rb.AddTorque(transform.right * RotationForce * horizontalInput * Time.deltaTime, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // sticks mining cart to rails by enabling y pos constraint
        if(other.transform.tag == "Rail")
        {
            transform.parent = other.gameObject.transform;
            //rb.constraints |= RigidbodyConstraints.FreezePositionY;
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        // unsticks mining cart from rails by disabling y pos constraint
        if(other.transform.tag == "Rail")
        {
            transform.parent = null;
            //rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
            onGround = false;
        }
    }

    void OnCollisionStay(Collision other)
    {
        if(other.transform.tag == "Rail")
        {
            onGround = true;
        }
    }

    // eliminar despues
    private void OnTriggerEnter(Collider other)
    {
        // eliminar despues
        if (other.transform.tag == "_LoseGame")
        {
            _GameHandler.Instancia.Mensaje("Te has equivocado de camino, no has podido entregar el fosil");
            _GameHandler.Instancia.IniciarTemporizador();
        }

        // eliminar despues
        if (other.transform.tag == "_WinGame")
        {
            _GameHandler.Instancia.Mensaje("Has entregado el fosil con exito! felicidades");
            _GameHandler.Instancia.IniciarTemporizador();
        }
    }
}
