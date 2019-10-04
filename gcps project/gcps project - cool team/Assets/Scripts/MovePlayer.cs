using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

   
    private CharacterController controller; // Character Controller
    private Vector3 mVector;                // Movement Vector

    private float speed = 5.0f;             // Player Speed
    private float gravity = 7.0f;           // Gravity value
    private float yVelocity;                // Velocity to be applied upwards or downwards

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

        mVector = Vector3.zero;

        
        // Checks if Player is on the floor 
        // Applies downwards velocity if the player is not
        if (controller.isGrounded) {
            yVelocity = 0.0f;
        }
        else {
            yVelocity -= gravity * Time.deltaTime;
        }
        
      
        
        // Player movement (To be overhauled) ------------------------
        // X - Strafe Left and Right - *Update to mobile input*
        mVector.x = Input.GetAxisRaw("Horizontal") * speed;

        // Y - Apply Upwards and downwards Velocity - *tba - Jumping mechanics*
    
        mVector.y = yVelocity;

        // Z - Move forward
        mVector.z = speed;

        controller.Move(mVector * Time.deltaTime);
        // ----------------------------------------------
	}
}
