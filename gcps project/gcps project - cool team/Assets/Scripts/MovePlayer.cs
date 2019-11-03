using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    // Movement | Physics 
    private float jumpForce = 6.0f;
    private float gravity = 10.0f;
    private float verticalVelocity;
    private float turnSpeed = 0.05f;

    //Animation
    private Animator anim;

    //Speed Increase
    private float originalSpeed = 7.0f;
    private float speed = 7.0f;
    private float speedIncreaseLastTick;
    private float speedIncreaseTime = 2.5f;
    private float speedIncreaseAmount = 0.1f;

    private bool isRunning = false;

    private float animationDuration = 2.0f;

    private bool isDead = false;

    // Lanes
    private const float laneWidth = 2.5f;
    private int goalLane = 1; // 0 Left | 1 Centre | 2 Right

    // Controller
    private CharacterController controller;

    // Use this for initialization
    void Start()
    {
        speed = originalSpeed;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

        //if (!isRunning)
        //return;

        if (isDead)
            return;

        if (Time.time < animationDuration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }
        
        //speed modifier 
        if (Time.time - speedIncreaseLastTick > speedIncreaseTime)
        {
            speedIncreaseLastTick = Time.time;
            speed += speedIncreaseAmount;
            GameManager.Instance.UpdateModifier(speed - originalSpeed);
        }

        if (MobileInput.Instance.SwipeLeft)
            changeLane(false);
        if (MobileInput.Instance.SwipeRight)
            changeLane(true);

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

        bool isGrounded = IsGrounded();
        anim.SetBool("Grounded", isGrounded);

        if (controller.isGrounded)
        {
            verticalVelocity = -0.1f;
            

            if (MobileInput.Instance.SwipeUp)
            {
                anim.SetTrigger("Jump");
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

        // Rotate
        Vector3 dir = controller.velocity;
        if (dir != Vector3.zero)
        {
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, turnSpeed);
        }
        
    }

    private void Collider()
    {
        anim.SetTrigger("Death");
        isRunning = false;
        speed = 0;
        GameManager.Instance.OnDeath();
    }
 


    // called when player crashes 
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.gameObject.tag)
        {
            case "Barrier":
                Collider();
                break;
        }
    }

    private void Death()
    {
        Debug.Log("Dead");
        isDead = true;
        GetComponent<GameManager> ().OnDeath ();
    }

    private bool IsGrounded()
    {
        return false;
    }

    private void changeLane(bool moveRight)
    {

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


    //public void StartRunning()
   //{
        //isRunning = true;
   //}

}
