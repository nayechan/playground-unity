using System.Collections;
using System.Collections.Generic;
using SandboxEditor.Data.Sandbox;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LibraryCardOptionController : MonoBehaviour
{
    [SerializeField] GameObject option;
    [SerializeField] string sceneToMoveOn = "SandboxEditAndPlayer";
    string gameID;
    bool isLocal;
    // Start is called before the first frame update
    private void Awake() {
        LibraryCardObserver.RegisterCard(this);
    }

    public void OnClick()
    {
        LibraryCardObserver.OnClick(this);
    }

    public void OnClickPlay()
    {
        Sandbox.SetSandboxDataToRun(gameID, isLocal, true);

        Debug.Log(string.Format("Game ID : {0}, isLocalSandbox : {1}, isRunningPlayer : {2}",gameID,isLocal,1));

        SceneMover.getInstance().MoveScene(sceneToMoveOn);
    }

    public void OnClickEdit()
    {
        Sandbox.SetSandboxDataToRun(gameID, isLocal, false);
        Debug.Log(string.Format("Game ID : {0}, isLocalSandbox : {1}, isRunningPlayer : {2}",gameID,isLocal,0));

        SceneMover.getInstance().MoveScene(sceneToMoveOn);
    }

    public void ActivateOption()
    {
        option.SetActive(true);
    }

    public void DeactivateOption()
    {
        option.SetActive(false);
    }

    private void OnDestroy() {
        LibraryCardObserver.RemoveCard(this);
    }

    public void SetGameID(string gameID, bool isLocal)
    {
        this.gameID = gameID; this.isLocal = isLocal;
    }
}
