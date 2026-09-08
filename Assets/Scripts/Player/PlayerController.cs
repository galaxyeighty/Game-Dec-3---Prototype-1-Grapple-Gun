using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 3f;
    [SerializeField] private float JumpHeight = 5f;
    [SerializeField] private float Gravity = 20f;
    [SerializeField] private float LookSensitivity = 0.2f;
    [SerializeField] private float LookAngleLimit = 90f;

    private Camera mainCamera;
    private CharacterController character;

    private InputAction moveInput;

    private InputAction jumpInput;
    private bool jumping = false;

    private float currentMoveSpeed = 0f;
    private Vector3 moveDirection = Vector3.zero;
    private float lookAngle = 0f;

    private void Start()
    {
        mainCamera = GetComponentInChildren<Camera> ();
        character = GetComponent<CharacterController>();

        moveInput = InputSystem.actions.FindAction("Move");

        jumpInput = InputSystem.actions.FindAction("Jump");
        jumpInput.started += Jumping;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        currentMoveSpeed = MoveSpeed;
    }


    private void Update()
    {
        Vector2 moveVector = moveInput.ReadValue<Vector2>();
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        if (!character.isGrounded)
        {
            jumping = false;
        }

        HandleMovement(moveVector);
        HandleLooking (mouseDelta);
    }

    private void HandleMovement(Vector2 moveVector)
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float oldY = moveDirection.y;

        Vector2 newSpeed = new Vector2(moveVector.y * currentMoveSpeed, moveVector.x * currentMoveSpeed);

        moveDirection = (forward * newSpeed.x) + (right * newSpeed.y);
        if (jumping && character.isGrounded)
        {
            moveDirection.y = JumpHeight;
        }
        else
        {
            moveDirection.y = oldY;
        }

            character.Move(moveDirection * Time.deltaTime);

        if (!character.isGrounded)
        {
            moveDirection.y -= Gravity * Time.deltaTime;
        }
    }

    private void Jumping(InputAction.CallbackContext _)
    {
        jumping = true;
    }

    private void HandleLooking(Vector2 mouseDelta)
    {
        lookAngle += -mouseDelta.y * LookSensitivity;
        lookAngle = Mathf.Clamp(lookAngle, -LookAngleLimit, LookAngleLimit);

        mainCamera.transform.localRotation = Quaternion.Euler(lookAngle, 0, 0);
        transform.rotation *= Quaternion.Euler(0, mouseDelta.x * LookSensitivity, 0);
    }
}
