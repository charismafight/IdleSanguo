using Assets.Scripts.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Editor
{
    internal class EditorHelper
    {
        public static void CreateCommonEditorButton(string content, float width, float height, Action onclickAction)
        {
            if (GUILayout.Button(new GUIContent(content), GUI.skin.button, GUILayout.Width(width), GUILayout.Height(height)))
            {
                onclickAction?.Invoke();
            }
        }

        public static void CreateCommonEditorButton(string content, string tooltip, float width, float height, Action onclickAction)
        {
            if (GUILayout.Button(new GUIContent(content, tooltip), GUI.skin.button, GUILayout.Width(width), GUILayout.Height(height)))
            {
                onclickAction?.Invoke();
            }
        }

        public static void CreateDefaultEditorButton(string content, string tooltip, Action onclickAction, float width = 140f, float height = 40f)
        {
            if (GUILayout.Button(new GUIContent(content, tooltip), GUI.skin.button, GUILayout.Width(width), GUILayout.Height(height)))
            {
                onclickAction?.Invoke();
                Debug.Log($"{content}完毕");
            }
        }
    }
}
