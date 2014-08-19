using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class PokeController : PlayerControllerBase
{

    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    private bool canJump = false;
    public float jumpHeight = 2.0f;

   
  
  

    // Any mode //
 
    /*
    protected override void FixedUpdate()
    {
        if (mode != Mode.Physics)
        {
            Grounded = true;
            return;
        }

        Grounded = Physics.OverlapSphere(
            transform.position + ConstrainedGravity.normalized * groundedCheckOffset,
            groundedDistance,
            groundLayers
        ).Length > 0;
        // Do an overlap sphere at our feet to see if we're touching the ground

      
       

            if (!Grounded || ConstrainedVelocity.magnitude > 0.0f)
            // Update velocity if we should be moving
            {
                rigidbody.AddForce(ConstrainedVelocity - rigidbody.velocity + CurrentGravityVelocity, ForceMode.VelocityChange);
            }

            if (!Grounded || (transform.position - lastPosition).magnitude > minimumGravityMovement)
            // Only add gravity if not grounded or last position delta exceeds a minimum threshold. Prevents sliding.
            {
                rigidbody.AddForce(ConstrainedGravity);
            }
        

        lastPosition = transform.position;
     * 
    } */
    public override void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // Calculate how fast we should be moving
        Vector3 targetVelocity = new Vector3(h, 0, v);
        animator.SetFloat("DirY", v, 0.15f, Time.deltaTime);
        animator.SetFloat("DirX", h, 0.15f, Time.deltaTime);
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity = 
        targetVelocity *= speed;

        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = rigidbody.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;
        if(velocityChange.magnitude > 0.1f)
        rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

        //jump logic
        if (Physics.Raycast(transform.position, Vector3.down, 0.1f))
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }

        // Jump
        if (canJump && Input.GetKey(KeyCode.Space))
        {
            rigidbody.velocity = new Vector3(velocity.x, JumpSpeed(), velocity.z);
        }

        // We apply gravity manually for more tuning control
        rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));
    }

    float JumpSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
    protected virtual void OnDrawGizmos()
    {

        if (mode == Mode.Physics)
        {
            Gizmos.color = Grounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(transform.position + transform.up * -groundedCheckOffset, groundedDistance);
        }

        const float kIndicatorScale = 2.0f;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + targetVelocity.normalized * kIndicatorScale);

        Gizmos.color = new Color(1.0f, 0.3f, 0.3f);
        Gizmos.DrawLine(transform.position, transform.position + ConstrainedVelocity.normalized * kIndicatorScale);
    }
}