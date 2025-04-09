using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spatialminds.Platformer
{
    public class JumpState : IState
    {
        private Character character;

        public JumpState(Character character)
        {
            this.character = character;
        }

        public void EnterState()
        {
            character.CharacterAnim().SetTrigger("jump");
            ServiceLocator.instance.GetService<AudioPlayerService>().PlayAudio("jump");
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
