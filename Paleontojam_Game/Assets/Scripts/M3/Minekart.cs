using UnityEngine;

/// <summary>
/// Controls minekart behavior
/// </summary>
public class Minekart : MonoBehaviour
{
    const float Accelaration = 15f;
    const float JumpForce = 10f;
    const float MaxVelocity = 20f;
    Rigidbody rb;
    bool needsToAccelarate = false;
    bool onGround = false;
    float jumpInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(rb.velocity.x < MaxVelocity)
        {
            needsToAccelarate = true;
        }

        jumpInput = Input.GetAxis("JumpMinekart");
    }

    private void FixedUpdate()
    {
        if (needsToAccelarate && onGround)
        {
            rb.AddForce(Vector3.right * Accelaration, ForceMode.Acceleration);
            needsToAccelarate = false;
        }

        if(jumpInput != 0 && onGround)
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            onGround = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "Rail")
        {
            transform.parent = other.gameObject.transform;
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if(other.transform.tag == "Rail")
        {
            transform.parent = null;
        }
    }
}
