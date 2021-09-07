using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * 
 */
public class GameEditorSceneManager : MonoBehaviour
{
    [SerializeField] private string currentScene;
    [SerializeField] private TemporaryGameEditorDataManager gameEditorDataManager;

    private GameObject tiles, objects;

    public void MoveTo(string scene)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        switch(currentScene)
        {
            case "GameEditor":
            break;
            case "MapEditor":
            GameObject tiles = GameObject.Find("Tiles");
            gameEditorDataManager.AttachTheirTiles(tiles);
            break;
            case "ObjectEditor":
            GameObject objects = GameObject.Find("Objects");
            GameObject dataManager = GameObject.Find("ObjectDataManager");

            gameEditorDataManager.LoadObjectTypes(
                dataManager.GetComponent<ObjectDataManager>()
            );

            gameEditorDataManager.AttachTheirObjects(objects);
            break;
            case "EventEditor":
            GameObject blocks = GameObject.Find("Blocks");
            gameEditorDataManager.AttachTheirBlocks(blocks);
            break;
            case "GamePlayPreview":
            break;
        }

        StartCoroutine("WaitForSceneLoad", scene);
    }
    void AfterMovement(string scene)
    {
        switch(scene)
        {
            case "GameEditor":
            break;
            case "MapEditor":
            GameObject tiles = GameObject.Find("Tiles");
            gameEditorDataManager.FetchOurTiles(tiles);
            break;
            case "ObjectEditor":
            GameObject objects = GameObject.Find("Objects");
            GameObject dataManager = GameObject.Find("ObjectDataManager");
            gameEditorDataManager.FetchOurObjects(objects);
            gameEditorDataManager.RestoreObjectTypes(
                dataManager.GetComponent<ObjectDataManager>()
            );
            break;
            case "EventEditor":
            GameObject blocks = GameObject.Find("Blocks");
            gameEditorDataManager.FetchOurBlocks(blocks);
            break;
            case "GamePlayPreview":
            break;
        }
    }
    IEnumerator WaitForSceneLoad(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        while(!op.isDone)
            yield return null;
        AfterMovement(sceneName);
    }
}
