using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Spatialminds.Platformer
{
    [CustomEditor(typeof(CharacterDataScriptableObject))]
    public class CharacterDataScriptableObjectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var characterDataObject = (CharacterDataScriptableObject)target;

            if (GUILayout.Button("Update Animator Hash", GUILayout.Height(40)))
            {
                characterDataObject.UpdateAnimatorParameters();
            }

            if (GUILayout.Button("Clear Animator Hash", GUILayout.Height(40)))
            {
                characterDataObject.ClearAnimParams();

                GUILayout.Label("Cleared!");
            }
        }
    }
}
