using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9f;

    [SerializeField] InputActionAsset inputActions;
    private InputAction moveAction;
    private InputAction runAction;
    private Rigidbody rb;

    public List<System.Func<float>> speedOverrides = new();

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = inputActions.FindAction("Move");
        runAction = inputActions.FindAction("Sprint");
    }

    void FixedUpdate()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        IsRunning = canRun && runAction.ReadValue<float>() > 0.5f;

        float targetMovingSpeed = speedOverrides.Count > 0
            ? speedOverrides[^1]()
            : (IsRunning ? runSpeed : speed);

        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y) * targetMovingSpeed;
        rb.linearVelocity = transform.rotation * new Vector3(move.x, rb.linearVelocity.y, move.z);
    }
}