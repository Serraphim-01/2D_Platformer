using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spatialminds.Platformer
{
    public class HitState : IState
    {
        private Character character;

        public HitState(Character character)
        {
            this.character = character;
        }

        public void EnterState()
        {
            character.CharacterAnim().SetBool("isRunning", true);
            character.CharacterAnim().SetBool("isIdle", false);
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
                    //character.characterStateManager.ChangeState(character.characterStateManager.)
                }
            }
        }
    }
}
