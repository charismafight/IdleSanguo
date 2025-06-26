using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets
{
    public class BaseComponent : MonoBehaviour
    {
        private GameObject ConfirmWindow;

        public GameObject CreatePrefabFromFile(string filename, GameObject parent = null)
        {
            Debug.Log("Trying to load LevelPrefab from file (" + filename + ")...");
            var loadedObject = PrefabUtility.LoadPrefabContents("Assets/Prefab/" + filename);
            if (loadedObject == null)
            {
                throw new FileNotFoundException("...no file found - please check the configuration");
            }

            return Instantiate(loadedObject, parent?.transform);
        }

        public bool Confirm(Action action, string content, string title = "提示")
        {
            if (ConfirmWindow == null)
            {
                ConfirmWindow = CreatePrefabFromFile(Constants.ConfirmWindowPrefab);
            }

            var cwScript = ConfirmWindow.GetComponent<ConfirmWindow>();
            cwScript.Callback = action;
            cwScript.Title = title;
            cwScript.Content = content;
            ConfirmWindow.SetActive(true);
            return ConfirmWindow.GetComponent<ConfirmWindow>().DialogResult;
        }
    }
}
