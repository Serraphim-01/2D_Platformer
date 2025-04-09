using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spatialminds.Platformer
{
    public class Player : Character
    {
        InputManager inputManager;

        public override void Start()
        {
            inputManager = FindObjectOfType<InputManager>();

            inputManager.Jump += OnJump;

            base.Start();

        }

        private void OnJump()
        {
            Jump();
        }

        public override void Update()
        {
            if(inputManager == null) return;

            SetHorizontal(inputManager.Move.x);
            SetJumpPressed(inputManager.isJumpPressed);

            base.Update();
        }

        private void OnDestroy()
        {
            inputManager.Jump -= OnJump;  
        }
    }
}
