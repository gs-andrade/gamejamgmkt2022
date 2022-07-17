using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AudioWindow : Editor
{
#if UNITY_EDITOR
    [CustomEditor(typeof(AudioManeger))]
    public class TowerollectionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            AudioManeger myTarget = (AudioManeger)target;

            if (GUILayout.Button("Apaga sons e recria array"))
            {
                myTarget.RefreshSounds();
            }


        }
#endif
    }
}
