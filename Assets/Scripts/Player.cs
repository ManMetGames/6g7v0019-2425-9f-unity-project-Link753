using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] GameObject Ball;
    [SerializeField] Transform Camera;
    [SerializeField] CharacterController characterController;
    [SerializeField] float Cooldown;
    int BulletsLeft;
    Vector3 Move;
    float xRotation, timeSinceLastFire;

    [Header("INPUTS")]
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Vector2 MoveValue;
    [SerializeField] Vector2 CameraMoveValue;
    InputAction FireButton;

    [Header("MOVEMENT")]
    [SerializeField] float Speed;
    [SerializeField] float MouseSensitivity = 100f;

    [Header("UI")]
    [SerializeField] TMP_Text DisplayCount;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Gameplay.Movement.performed += ctx => MoveValue = ctx.ReadValue<Vector2>();
        playerInput.Gameplay.Movement.canceled += ctx => MoveValue = Vector2.zero;
        playerInput.Gameplay.CameraMovement.performed += ctx => CameraMoveValue = ctx.ReadValue<Vector2>();
        playerInput.Gameplay.CameraMovement.canceled += ctx => CameraMoveValue = Vector2.zero;
        FireButton = playerInput.Gameplay.Fire;

        timeSinceLastFire = 0;
        BulletsLeft = 5;
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
        DisplayCount = GameObject.Find("PlayerBallCount").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastFire += Time.deltaTime;
        Move = transform.right * MoveValue.x + transform.forward * MoveValue.y;
        characterController.Move(Move * Speed * Time.deltaTime);
        xRotation -= CameraMoveValue.y * MouseSensitivity/2 * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Camera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * CameraMoveValue.x * MouseSensitivity * Time.deltaTime);
        DisplayCount.text = BulletsLeft.ToString();

        if (FireButton.IsInProgress() & timeSinceLastFire > Cooldown & BulletsLeft > 0)
        {
            if (GameManager.instance.GetPooledObject())
            {
                GameObject g = GameManager.instance.GetPooledObject();
                g.SetActive(true);
                g.transform.position = transform.GetChild(1).position;
                g.GetComponent<Rigidbody>().AddForce(Camera.forward * 40, ForceMode.VelocityChange);
                timeSinceLastFire = 0;
                BulletsLeft--;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            BulletsLeft += Random.Range(1, 3);
            Destroy(other.gameObject);
        }
    }
}
