using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour { 

    // Movement | Physics 
    private float jumpForce = 6.0f;
    private float gravity = 10.0f;
    private float verticalVelocity;
    private float speed = 3.0f;

    // Lanes
    private const float laneWidth = 2.5f;
    private int goalLane = 1; // 0 Left | 1 Centre | 2 Right

    // Controller
    private CharacterController controller;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
	}

    // Update is called once per frame
    void Update() {

        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            changeLane(false);
            Debug.Log("left key pressed");
        }

        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            changeLane(true);
        }

        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (goalLane == 0) {
            targetPosition += Vector3.left * laneWidth;
        }
        else if (goalLane == 2) {
            targetPosition += Vector3.right * laneWidth;
        }
        

        // Move delta
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * speed;

        if (controller.isGrounded)
        {
            verticalVelocity = -0.1f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;

            // Fast fall
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                verticalVelocity = -jumpForce;
            }
        }
        

        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        // Move Player
        controller.Move(moveVector * Time.deltaTime);

    }

    private void changeLane(bool moveRight) {

        /*
        goalLane += (moveRight) ? 1 : -1;
        goalLane = Mathf.Clamp(goalLane, 0, 2);
        */

        if (moveRight == false) {
            goalLane--;
        }

        else {
            goalLane++;
        }
            goalLane = Mathf.Clamp(goalLane, 0, 2); 
    }
}
