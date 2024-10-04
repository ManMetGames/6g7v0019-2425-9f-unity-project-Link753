using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] GameObject Ball;
    [SerializeField] Transform Camera;
    [SerializeField] CharacterController characterController;
    Vector3 Move;
    float xRotation;

    [Header("INPUTS")]
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Vector2 MoveValue;
    [SerializeField] Vector2 CameraMoveValue;
    InputAction FireButton;

    [Header("MOVEMENT")]
    [SerializeField] float Speed;
    [SerializeField] float MouseSensitivity = 100f;


    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Gameplay.Movement.performed += ctx => MoveValue = ctx.ReadValue<Vector2>();
        playerInput.Gameplay.Movement.canceled += ctx => MoveValue = Vector2.zero;
        playerInput.Gameplay.CameraMovement.performed += ctx => CameraMoveValue = ctx.ReadValue<Vector2>();
        playerInput.Gameplay.CameraMovement.canceled += ctx => CameraMoveValue = Vector2.zero;
        FireButton = playerInput.Gameplay.Fire;
    }

    private void OnEnable()
    {
        playerInput.Gameplay.Enable();
    }

    private void OnDisable()
    {
        playerInput.Gameplay.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Camera = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        Move = transform.right * MoveValue.x + transform.forward * MoveValue.y;
        characterController.Move(Move * Speed * Time.deltaTime);
        xRotation -= CameraMoveValue.y * MouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Camera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * CameraMoveValue.x);

        if (FireButton.IsInProgress())
        {
            GameObject g = Instantiate(Ball);
            g.transform.position = transform.position;
            g.GetComponent<Rigidbody>().AddForce(Random.Range(-100, 100), 10, Random.Range(-100, 100));
        }
    }
}
