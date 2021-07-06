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
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            GameInstance.quitGame();
        }
        if (Input.GetKey(KeyCode.Tab))
        {
            PauseMenu.togglePausemenu();
        }
        if (Input.GetKey(KeyCode.CapsLock))
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

        if (isenabled)
        {
            movement();
            ClientSend.PlayerTransform();
        }
    }

    private void OnEnable()
    {
        controller.enabled = true;
        isenabled = true;
    }
    private void OnDisable()
    {
        controller.enabled = false;
        isenabled = false;
    }

    /// <summary>Sends player input to the server.</summary>
    private void movement()
    {
        Vector3 _moveDirection;
        Vector2 _inputDirection = Vector2.zero;
        {
            if (Input.GetKey(KeyCode.W))
            {
                _inputDirection.y += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                _inputDirection.y -= 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                _inputDirection.x -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                _inputDirection.x += 1;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                _inputDirection *= 1.6f;
            }

            _moveDirection = transform.right * _inputDirection.x + transform.forward * _inputDirection.y;
            _moveDirection *= moveSpeed;
        }

        if (controller.isGrounded)
        {
            yVelocity = 0f;
            if (Input.GetKey(KeyCode.Space))
            {
                yVelocity = jumpSpeed;
            }
        }
        yVelocity += gravity;

        _moveDirection.y = yVelocity;
        controller.Move(_moveDirection);

        HUD.instance.percentage = _moveDirection.magnitude;
    }

    
}