using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEditorMainUI : MonoBehaviour
{
    [SerializeField] private GameEditorMoveScene moveScene;
    List<string> scenePathList;
    private string sceneDir;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        scenePathList = new List<string>();

        Scene currentScene = SceneManager.GetActiveScene();
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        sceneDir = SceneUtility.GetScenePathByBuildIndex(
            currentScene.buildIndex
        );

        sceneDir = sceneDir.Substring(
            0, 
            sceneDir.Length - System.IO.Path.GetFileName(sceneDir).Length
        );

        Debug.Log(sceneDir);

        for(int i=0;i<sceneCount;++i)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);

            string dir = scenePath.Substring(
                0, 
                scenePath.Length - System.IO.Path.GetFileName(scenePath).Length
            );

            if(System.IO.Path.GetFileNameWithoutExtension(scenePath)
            == currentScene.name) continue;

            if(dir != sceneDir) continue;
            scenePathList.Add(scenePath);
        }
    }
    public void SceneChange(string sceneName)
    {
        moveScene.MoveTo(sceneName);
        if(!scenePathList.Contains(sceneDir + sceneName + ".unity"))
            Destroy(gameObject);
    }
}
