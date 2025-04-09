using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spatialminds.Platformer
{
    [Serializable]
    public class CharacterStateManager
    {
        public IState currentState { get; private set; }

        public MoveState moveState;
        public JumpState jumpState;
        public IdleState idleState;
        public HitState hitState;
        public FallState fallState;

        public CharacterStateManager(Character character) 
        {
            moveState = new MoveState(character);

            jumpState = new JumpState(character);

            idleState = new IdleState(character);

            hitState = new HitState(character);
            fallState = new FallState(character);


        }

        public void Initialize(IState state)
        {
            currentState = state;
            state.EnterState();
        }

        public void ChangeState(IState state)
        {
            currentState.ExitState();
            currentState = state;
            state.EnterState();
        }

        public void Update()
        {
            if (currentState == null) return;
            
            currentState.UpdateState();
        }
    }
}
