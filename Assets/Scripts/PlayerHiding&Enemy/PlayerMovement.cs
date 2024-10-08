using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    [SerializeField]
    private float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    private bool canMove = true;
    [HideInInspector] public StaminaController _staminaController;

    void Start()
    {
        _staminaController = GetComponent<StaminaController>();
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && _staminaController.playerStamina > 0; // 스태미너가 있을 때만 달리기 가능

        // 스태미너가 0이면 달리기 속도를 걷기 속도로 바꾸기
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // 점프 로직
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
            _staminaController.StaminaJump();  // 점프할 때 스태미너 사용
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // 중력 적용
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // 앉기 로직
        if (Input.GetKey(KeyCode.R) && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;
        }
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 3f;
            runSpeed = 6f;
        }

        // 달리기 시 스태미너 소모
        if (isRunning)
        {
            _staminaController.Sprinting();  // 스태미너 소모
        }

        characterController.Move(moveDirection * Time.deltaTime);

        // 카메라 회전
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}
