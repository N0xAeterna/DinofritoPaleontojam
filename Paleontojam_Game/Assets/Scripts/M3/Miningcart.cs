using UnityEngine;

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
    const float JumpForce = 10f;
    bool onGround = false;
    float jumpInput;

    // rotation support
    float horizontalInput;
    const float AnglesPerSecond = 180f;

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
    /// Handles speed clamping and input for accelaration and jumping
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
    
        // handling mid air rotation
        if (horizontalInput != 0 && !onGround)
        {
            transform.Rotate(Vector3.right * AnglesPerSecond * Time.deltaTime, horizontalInput);
        }
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
            
        }

        if(jumpInput != 0 && onGround)
        {
            rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
            rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
            onGround = false;
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
}
