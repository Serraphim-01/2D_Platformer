using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spatialminds.Platformer
{
    public class FallState : IState
    {
        private Character character;

        public FallState(Character character)
        {
            this.character = character;
        }

        public void EnterState()
        {
            character.CharacterAnim().SetTrigger("jump");
        }

        public void ExitState()
        {
            
        }

        public void UpdateState()
        {
            if(character.isGround)
            {
                if(Mathf.Abs(character.horizontal)>0)
                {
                    character.characterStateManager.ChangeState(character.characterStateManager.moveState);
                }else
                {
                    character.characterStateManager.ChangeState(character.characterStateManager.idleState);
                }
            }
        }
    }
}
