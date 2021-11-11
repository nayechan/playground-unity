using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SandboxEditor.UI
{
    public class ExitButtonController : MonoBehaviour
    {
        [SerializeField] string sceneName;
        public void OnExitButtonClick()
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}

