using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCombiner : MonoBehaviour
{
    // Start is called before the first frame update
    List<string> scenePathList;
    [SerializeField] private string initialScene;
    private string currentOpenedScene;
    private string sceneDir;
    void Start()
    {
        scenePathList = new List<string>();
        currentOpenedScene = initialScene;

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

            if(System.IO.Path.GetFileNameWithoutExtension(scenePath)
            == initialScene)
            {
                StartCoroutine("CustomLoadSceneAsync", i);
            }
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
    }

    IEnumerator CustomUnloadSceneAsync(int sceneBuildIndex)
    {
        AsyncOperation asyncOperation = 
        SceneManager.UnloadSceneAsync(sceneBuildIndex);

        while(!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    public void OpenScene(string sceneName){
        int buildIndex;
        
        buildIndex = SceneUtility.GetBuildIndexByScenePath(
            sceneDir+currentOpenedScene+".unity"
        );
        StartCoroutine("CustomUnloadSceneAsync",buildIndex);

        buildIndex = SceneUtility.GetBuildIndexByScenePath(
            sceneDir+sceneName+".unity"
        );
        
        currentOpenedScene = sceneName;
        StartCoroutine("CustomLoadSceneAsync",buildIndex);
    }
}
