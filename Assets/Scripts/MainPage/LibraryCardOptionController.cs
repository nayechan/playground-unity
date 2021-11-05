using System.Collections;
using System.Collections.Generic;
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
        PlayerPrefs.SetString("sandboxToRun",gameID);
        PlayerPrefs.SetInt("isLocalSandbox", isLocal?1:0);
        PlayerPrefs.SetInt("isRunningPlayer", 1);

        SceneMover.getInstance().MoveScene(sceneToMoveOn);
    }

    public void OnClickEdit()
    {
        PlayerPrefs.SetString("sandboxToRun",gameID);
        PlayerPrefs.SetInt("isLocalSandbox", isLocal?1:0);
        PlayerPrefs.SetInt("isRunningPlayer", 0);

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
