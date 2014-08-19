using UnityEngine;
using System.Collections;

public class UserInputController : MonoBehaviour
{
    public struct State
    {
        public Vector3 move;
    }
    private Transform mainCamera;
    private Vector3 InputXDir;
    private Vector3 InputYDir;
    private State state = new State();	
    private PlayerControllerBase playerControllerBase;
    void Start()
    {
        mainCamera = Camera.main.transform;
            playerControllerBase = GetComponent<PlayerControllerBase>();
       	
    }
    void Update()
    {
        float inputX =  Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

       InputYDir = Vector3.Scale(mainCamera.forward, new Vector3(1, 0, 1)).normalized;
       InputXDir = inputY * mainCamera.right;


       state.move = (InputYDir + InputXDir).normalized;	
    }
    void FixedUpdate()
    {

    }



}