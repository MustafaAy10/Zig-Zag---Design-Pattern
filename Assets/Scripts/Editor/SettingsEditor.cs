using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    public class SettingsEditor : EditorWindow
    {
        [MenuItem("Window/ZigZag Settings")]
        public static void OpenWindow()
        {
            SettingsEditor wnd = GetWindow<SettingsEditor>();
        }

        public void OnGUI()
        {
            GUILayout.Space(30);

            if (GUILayout.Button("Delete All PlayerPrefs"))
            {
                PlayerPrefs.DeleteAll();
            }
        }
    }

}
