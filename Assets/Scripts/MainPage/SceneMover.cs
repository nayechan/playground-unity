using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
    static SceneMover sceneMoverInstance;
    [SerializeField] GameObject sceneMovementDisplay, blackPanel;
    [SerializeField] Text sceneMovementText;
    const string loadText = "게임 에디터 로딩 중... ({0}%)";

    private void Awake() {
        if(SceneMover.getInstance()==null)
        {
            SceneMover.setInstance(this);
        }
    }
    public void MoveScene(string sceneName)
    {
        StartCoroutine(ProcessMoveScene(sceneName));
    }

    IEnumerator ProcessMoveScene(string sceneName)
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        if(sceneMovementDisplay != null)
        {
            sceneMovementDisplay.SetActive(true);
        }

        if(blackPanel != null)
        {
            blackPanel.SetActive(true);
        }

        while (!asyncLoad.isDone)
        {
            if(sceneMovementText != null)
            {
                sceneMovementText.text = string.Format(
                    loadText, asyncLoad.progress.ToString()
                );
            }
            yield return null;
        }
    }

    public static void setInstance(SceneMover sceneMover){sceneMoverInstance = sceneMover;}
    public static SceneMover getInstance(){return sceneMoverInstance;}
}
