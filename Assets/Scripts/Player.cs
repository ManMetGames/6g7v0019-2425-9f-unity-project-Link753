using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInput playerInput;
    Vector2 Movevalue;

    private void Awake()
    {
        playerInput = new PlayerInput();
        Movevalue = playerInput.Gameplay.Movement.ReadValue<Vector2>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
