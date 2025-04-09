using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Spatialminds.Platformer
{
    /// <summary>
    /// Provides the invokations for inputs based on the Input System
    /// </summary>
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : MonoBehaviour
    {
        PlayerInput playerInput;
        InputAction moveAction;
        InputAction jumpAction;
        InputAction fireAction;
        InputAction interactAction;

        public event Action Jump;
        public event Action Fire;
        public event Action Interact;

        void Start()
        {
            playerInput = GetComponent<PlayerInput>();
            moveAction = playerInput.actions["Move"];
            jumpAction = playerInput.actions["Jump"];
            fireAction = playerInput.actions["Fire"];
            interactAction = playerInput.actions["Interact"];

            jumpAction.performed += OnJump;
            fireAction.performed += OnFire;
            interactAction.performed += OnInteract;
        }

        public Vector2 Move => moveAction.ReadValue<Vector2>();

        public bool isJumpPressed => jumpAction.ReadValue<float>() > 0;

        public void OnInteract(InputAction.CallbackContext obj) => Interact?.Invoke();

        public void OnFire(InputAction.CallbackContext obj) => Fire?.Invoke();

        public void OnJump(InputAction.CallbackContext obj) => Jump?.Invoke();

        void OnDestroy()
        {
            jumpAction.performed -= OnJump;
            fireAction.performed -= OnFire;
            interactAction.performed -= OnInteract;
        }

    }
}
