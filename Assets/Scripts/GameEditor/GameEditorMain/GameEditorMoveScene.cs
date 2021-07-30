using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEditorMoveScene : MonoBehaviour
{
    [SerializeField] GameEditorDataManager.GameEditorDataManager dataManager;
    [SerializeField] private string currentScene;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
    public void MoveTo(string scene)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        switch(currentScene)
        {
            case "GameEditor":
            break;
            case "MapEditor":
            break;
            case "ObjectEditor":

            GameEditorDataManager.ObjectEditorData data;
            data = new GameEditorDataManager.ObjectEditorData();

            GameObject panelGameObject = GameObject.Find("SelectObject");
            SelectObjectPanelController panel = panelGameObject.GetComponent<SelectObjectPanelController>();

            foreach(ObjectPrimitiveData primitiveData in panel.GetObjectPrimitiveDatas())
            {
                data.AddObjectType(primitiveData);
            }

            Transform objects = GameObject.Find("Objects").transform;
            foreach(Transform transform in objects)
            {
                if(transform.name == "currentObject")
                    continue;
                data.AddObjectData(
                    new GameEditorDataManager.ObjectEditorData.ObjectData(
                        transform.position,
                        transform.GetComponent<ObjectInstanceController>().GetObjectPrimitiveData().GetGuid()
                    )
                );
            }
            break;
            case "EventEditor":
            break;
        }

        StartCoroutine("WaitForSceneLoad", scene);
    }
    IEnumerator WaitForSceneLoad(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        while(!op.isDone)
            yield return null;
        AfterMovement(sceneName);
    }
    void AfterMovement(string scene)
    {
        switch(scene)
        {
            case "GameEditor":
            break;
            case "MapEditor":
            dataManager.printJsonString();
            break;
            case "ObjectEditor":
            break;
            case "EventEditor":
            break;
        }
    }
}
