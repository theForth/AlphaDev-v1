using UnityEngine;
using System.Collections;

public class HashIDs :MonoBehaviour
{
    // Here we store the hash tags for various strings used in our animators.
    public static int dyingState;
    public static int idleState;
    public static int locomotionState;
    public static int shoutState;
    public static int deadBool;
    public static int speedFloat;
    public static int sneakingBool;
    public static int shoutingBool;
    public static int playerInSightBool;
    public static int thunderTrigger;
    public static int growlTrigger;



    void Awake()
    {
        idleState = Animator.StringToHash("Idle");
        speedFloat = Animator.StringToHash("Speed");
        thunderTrigger = Animator.StringToHash("Thunder");
        growlTrigger = Animator.StringToHash("Growl");
        dyingState = Animator.StringToHash("Base Layer.Dying");
        locomotionState = Animator.StringToHash("Base Layer.Locomotion");
        shoutState = Animator.StringToHash("Shouting.Shout");
        deadBool = Animator.StringToHash("Dead");

    }
    void Update()
    {

    }
}