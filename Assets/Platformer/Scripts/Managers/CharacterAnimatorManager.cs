using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spatialminds.Platformer
{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        [SerializeField] Animator characterAnimator;
        [SerializeField] CharacterDataScriptableObject characterData;
        
        public void SetBool(string name, bool value)
        {
            if(characterData.animatorParameters.TryGetValue(name, out int hash))
            {
                characterAnimator.SetBool(hash, value);
            }
        }

        public void SetTrigger(string name)
        {
            if (characterData.animatorParameters.TryGetValue(name, out int hash))
            {
                characterAnimator.SetTrigger(hash);
            }
        }
        
    }
}
