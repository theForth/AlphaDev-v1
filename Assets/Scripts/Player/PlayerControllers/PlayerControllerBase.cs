using UnityEngine;
using System.Collections;



/// <summary>
/// The base abstract class for all character controllers, provides common functionality.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public abstract class PlayerControllerBase : MonoBehaviour
{
    /// <summary>
    /// Test Collider
    /// </summary>
    protected CapsuleCollider collider;
    /// <summary>
    /// Minimum Height above ground to be considered jumping
    /// </summary>
    protected float airborneThreshold = 0.6f;
    /// <summary>
    /// Terrain where feet touch
    /// </summary>
    [SerializeField]
    public LayerMask groundLayers;
    [SerializeField]
    float spherecastRadius = 0.1f;
    public enum Mode
    {
        Auto,
        // Select based on which component is attached to the same GameObject
        Basic,
        // Transform movement based on targetVelocity and drag - gravity is ignored
        Physics,
        // Rigidbody movement with drag, gravity and groundedness check
        Navigation
        // NavMeshAgent movement
    };
    public Mode mode;
    private bool _grounded;
    public float drag = 0.0f;
    public float speed = 5.0f;
    // What is the fastest this mover can move?
    public float rotationSpeed = 5.0f;
    // What is the fastest this mover can rotate?
    public float arrivalDistance = 1.0f;

    public Vector3 gravity = new Vector3(0.0f, -9.81f, 0.0f);
    // Local, tweakable gravity
    public float minimumGravityMovement = 0.001f;
    // Below which position delta should we stop applying gravity in order to stop sliding?
    // Which layers should be walkable?
    // NOTICE: Make sure that the target collider is not in any of these layers!
    public float groundedCheckOffset = 0.7f;

    // Tweak so check starts from just within target footing
    public float groundedDistance = 1.0f;
    // Any mode //
    public Vector3 targetVelocity, targetPosition;
    public Quaternion targetRotation;
    public float
        rotationInterpolationSpeed = 10.0f,
        // How fast should current, predicted, rotation be interpolated to target rotation?
        positionInterpolationSpeed = 30.0f;
    public float speedScale = 1.0f;
    bool applyPosition = true, firstSync = true, validDestination = false;

    public Vector3 lastPosition;

    public Animator animator;
    
    // Tweak if character lands too soon or gets stuck "in air" often
    public bool Grounded
    {
        get
        {
            return _grounded;
        }
        set
        {
            _grounded = value;
        }
    }

    public bool ValidDestination
    {
        get
        {
            return validDestination;
        }
    }
    public bool ApplyPosition
    {
        get
        {
            return applyPosition;
        }
        set
        {
            applyPosition = value;
        }
    }
    public virtual Vector3 ConstrainedVelocity
    {
        get
        {
            return targetVelocity * speedScale;
        }
    }


    public virtual Quaternion ConstrainedRotation
    {
        get
        {
            return targetRotation;
        }
    }


    public virtual Vector3 ConstrainedGravity
    {
        get
        {
            return gravity;
        }
    }
    public Vector3 CurrentVelocity
    {
        get
        {
            switch (mode)
            {
               
                case Mode.Physics:
                    return rigidbody.velocity;
                case Mode.Basic:
                default:
                    return targetVelocity;
            }
        }
    }
    public Vector3 CurrentGravityVelocity
    {
         get
        {
            Vector3 gravityNormal = ConstrainedGravity.normalized;
            return Vector3.Dot(CurrentVelocity, gravityNormal) * gravityNormal;
        }
    }
    public void Init()
    // Set default state
    {
        animator = GetComponent<Animator>();
        targetPosition = transform.position;
        targetRotation = transform.rotation;
       targetPosition = transform.position;
			targetRotation = transform.rotation;
			switch (mode)
			{
				case Mode.Basic:
					targetVelocity = Vector3.zero;
					drag = 0.0f;
				break;
				case Mode.Physics:
					targetVelocity = rigidbody.velocity;
					drag = rigidbody.drag;
				break;
			}
        }
    
    
    //public abstract void UpdateState(System.Object state);
    //public abstract void Move(Vector3 deltaPosition, Quaternion deltaRotation);
    protected virtual void Start()
    // Configure rigidbody
    {
        switch (mode)
        {
            case Mode.Physics:
                rigidbody.isKinematic = false;
                rigidbody.freezeRotation = true;
                rigidbody.useGravity = false;
                 rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    
                break;
            
        }
    }
   
    public virtual void Awake()
    {
        Init();
    }
    public virtual void Stop()
    {
        targetVelocity = Vector3.zero;
        validDestination = false;

        switch (mode)
        {
          
            case Mode.Physics:
                rigidbody.velocity = rigidbody.angularVelocity = Vector3.zero;
                rigidbody.Sleep();
                break;
        }
    }
    protected virtual RaycastHit GetSpherecastHit()
    {
        Ray ray = new Ray(rigidbody.position + Vector3.up * airborneThreshold, Vector3.down);
        RaycastHit h = new RaycastHit();

        Physics.SphereCast(ray, spherecastRadius, out h, airborneThreshold * 2f, groundLayers);
        return h;
    }
    void OnAnimatorMove()
    {
        Move(animator.deltaPosition, animator.deltaRotation);
    }
    void Update()
    {

    }

    public virtual void FixedUpdate()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");	
     
        animator.SetFloat("DirY", v,  0.15f, Time.deltaTime);
        animator.SetFloat("DirX", h, 0.15f, Time.deltaTime);
        //animator.SetBool("Grounded", grounded);
    }
    bool IsGrounded()
    {
        return Physics.OverlapSphere(
           transform.position + ConstrainedGravity.normalized * groundedCheckOffset,
           groundedDistance,
           groundLayers
       ).Length > 0;
    }
    public void Move(Vector3 deltaPosition, Quaternion deltaRotation)
    {
        
        //rigidbody.position += deltaPosition * speed;
      

    
        

        // Limit the vertical velocity
        //velocity.y = Mathf.Clamp(rigidbody.velocity.y, rigidbody.velocity.y, onGround ? advancedSettings.maxVerticalVelocityOnGround : rigidbody.velocity.y);

        //velocity += Vector3.down * stickyForce * Time.deltaTime;


        //rigidbody.velocity = velocity;

        // Dampering forward speed on the slopes
       // float slopeDamper = !onGround ? 1f : GetSlopeDamper(-deltaPosition / Time.deltaTime, normal);
        //forwardMlp = Mathf.Lerp(forwardMlp, slopeDamper, Time.deltaTime * 5f);
    }

}
