using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCombiner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        string sceneDir = SceneUtility.GetScenePathByBuildIndex(
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
            StartCoroutine("CustomLoadSceneAsync", i);
        }
    }

    IEnumerator CustomLoadSceneAsync(int sceneBuildIndex)
    {
        AsyncOperation asyncOperation = 
        SceneManager.LoadSceneAsync(sceneBuildIndex,LoadSceneMode.Additive);

        while(!asyncOperation.isDone)
        {
            yield return null;
        }

        Destroy(GameObject.Find("EventSystem"));

        // Scene scene = SceneManager.GetSceneByBuildIndex(sceneBuildIndex);
        // GameObject[] gameObjects = scene.GetRootGameObjects();
        // foreach(GameObject gameObject in gameObjects)
        // {
        //     gameObject
        // }
    }
}
