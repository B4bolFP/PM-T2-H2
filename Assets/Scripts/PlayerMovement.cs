using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Controles")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Movimiento")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;


    public float groundDrag;

    public int jumps;
    private int jumpsLeft;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;


    [Header("Comprobacion suelo")]
    public float playerHeight;
    public LayerMask queEsSuelo;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;

    public MovementState state;

    public enum MovementState
    {
        walking,
        sprinting,
        air
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        jumpsLeft = jumps - 1;
        resetJump();
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, queEsSuelo);

        MyInput();
        SpeedControl();
        StateHandler();

        if (grounded)
        {

            rb.drag = groundDrag;
            jumpsLeft = jumps;
        } else
        {

            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && readyToJump && jumpsLeft > 0)
        {
            readyToJump = false;

            jump();
            jumpsLeft -= 1;

            Invoke(nameof(resetJump), jumpCooldown);
        }

        
    }

    public void StateHandler()
    {
        // Corriendo
        if(grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;

        // Andando
        } else if(grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;

        // En el aire
        } else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        // Calculo de movimiento
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        } else if(!grounded) {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0 , rb.velocity.z);

        // Limitamos la velocidad si nos pasamos
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }


    private void resetJump()
    {
        readyToJump = true;
    }
}
