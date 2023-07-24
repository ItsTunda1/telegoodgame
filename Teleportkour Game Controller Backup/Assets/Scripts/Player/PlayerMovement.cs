using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public bool isDisabled;

    [Header("Movement")]
    public float moveSpeed;
    public float sprintSpeed;
    float speed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier;
    bool readyToJump = true;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    public Rigidbody rb;
    public GameObject playerObj;


    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //playerObj = this.gameObject;
        //rb.freezeRotation = true;

        speed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDisabled == false)
        {
            //Ground Check
            grounded = Physics.Raycast(playerObj.transform.position, Vector3.down, playerHeight * 0.5f + 0.2f/*, whatIsGround*/);

            MyInput();
            SpeedControl();

            //handle drag
            if (grounded)
                rb.drag = groundDrag;
            else
                rb.drag = 0;
        }
    }

    void FixedUpdate()
    {
        if (isDisabled == false)
        {
            MovePlayer();
        }
    }

    //Inputs
    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCoolDown);
        }

        //Sprinting
        if (Input.GetKeyDown(sprintKey))
        {
            speed = sprintSpeed;
        }

        //Stopped Sprinting
        if (Input.GetKeyUp(sprintKey))
        {
            speed = moveSpeed;
        }
    }

    //Movement
    void MovePlayer()
    {
        //Calc move dir
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //when grounded
        if (grounded)
            rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
        //in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * speed * 10f * airMultiplier, ForceMode.Force);
    }

    //Speed Control
    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //Limit velocity if needed
        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    //Jump
    void Jump()
    {
        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        readyToJump = true;
    }


}
