using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spatialminds.Platformer
{
    public class MoveState : IState
    {
        private Character character;

        public MoveState(Character character)
        {
            this.character = character;
        }

        public void EnterState()
        {
            character.CharacterAnim().SetBool("isRunning", true);
        }

        public void ExitState()
        {
            character.CharacterAnim().SetBool("isRunning", false);
        }

        public void UpdateState()
        {
            if(character.isGround)
            {
                if(Mathf.Abs(character.horizontal)==0)
                {
                    character.characterStateManager.ChangeState(character.characterStateManager.idleState);
                }
            }else
            {
                if(character.characterRB.velocity.y <= 0)
                    character.characterStateManager.ChangeState(character.characterStateManager.fallState);
                else
                    character.characterStateManager.ChangeState(character.characterStateManager.jumpState);
            }
        }
    }
}
