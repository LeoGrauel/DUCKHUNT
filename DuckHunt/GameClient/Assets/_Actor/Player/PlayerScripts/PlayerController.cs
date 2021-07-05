using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private bool isenabled = true;

    public CharacterController controller;
    public float gravity = -9.81f;
    public float moveSpeed = 5f;
    public float jumpSpeed = 5f;
    public float maxvelocity = 9f;


    private bool[] inputs;
    private float yVelocity = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    private void Start()
    {
        gravity *= Time.fixedDeltaTime * Time.fixedDeltaTime;
        moveSpeed *= Time.fixedDeltaTime;
        jumpSpeed *= Time.fixedDeltaTime;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            GameInstance.quitGame();
        }
        if (Input.GetKey(KeyCode.Tab))
        {
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isenabled)
        {
            movement();
            shoot();
            ClientSend.PlayerTransform();
        }
    }

    private void OnEnable()
    {
        Debug.Log("PC enabled");
        controller.enabled = true;
        isenabled = true;
    }
    private void OnDisable()
    {
        Debug.Log("PC disabled");
        controller.enabled = false;
        isenabled = false;
    }

    public void setLocation(Vector3 location)
    {

    }

    /// <summary>Sends player input to the server.</summary>
    private void movement()
    {
        inputs = new bool[]
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.Space)
        };

        Vector2 _inputDirection = Vector2.zero;
        if (this.inputs[0])
        {
            _inputDirection.y += 1;
        }
        if (this.inputs[1])
        {
            _inputDirection.y -= 1;
        }
        if (this.inputs[2])
        {
            _inputDirection.x -= 1;
        }
        if (this.inputs[3])
        {
            _inputDirection.x += 1;
        }

        Vector3 _moveDirection = transform.right * _inputDirection.x + transform.forward * _inputDirection.y;
        _moveDirection *= moveSpeed;

        if (controller.isGrounded)
        {
            yVelocity = 0f;
            if (this.inputs[4])
            {
                yVelocity = jumpSpeed;
            }
        }
        yVelocity += gravity;

        _moveDirection.y = yVelocity;
        controller.Move(_moveDirection);
    }

    public void shoot()
    {
        inputs = new bool[]
        {
            Input.GetKey(KeyCode.Mouse0),
            Input.GetKey(KeyCode.Mouse1),
        };

        if (this.inputs[0])
        {

        }

        if (this.inputs[1])
        {

        }
    }
}