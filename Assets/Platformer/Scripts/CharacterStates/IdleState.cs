using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spatialminds.Platformer
{
    public class IdleState : IState
    {
        private Character character;

        public IdleState(Character character)
        {
            this.character = character;
        }

        public void EnterState()
        {
            character.CharacterAnim().SetBool("isIdle", true);
        }

        public void ExitState()
        {
            character.CharacterAnim().SetBool("isIdle", false);
        }

        public void UpdateState()
        {
            if(character.isGround)
            {
                if(Mathf.Abs(character.horizontal)>0)
                {
                    character.characterStateManager.ChangeState(character.characterStateManager.moveState);
                }
            }else
            {
                character.characterStateManager.ChangeState(character.characterStateManager.jumpState);
            }
        }
    }
}
