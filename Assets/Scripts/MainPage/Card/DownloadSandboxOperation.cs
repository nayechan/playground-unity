using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainPage.Panel;

public class DownloadSandboxOperation : SandboxCardOnClickOperation
{
    Response.ResponseItem responseData;
    Transform downloadPanel;
    public override void execute()
    {
        downloadPanel.gameObject.SetActive(true);

        Debug.Log(responseData.getGameID());

        DownloadPanelController downloadPanelController = 
        downloadPanel.GetComponent<DownloadPanelController>();

        downloadPanelController.SetCurrentResponseData(responseData);
        downloadPanelController.UpdateComponent();
    }

    public void SetResponseData(Response.ResponseItem response)
    {
        responseData = response;
    }

    public void SetDownloadPanel(Transform transform){downloadPanel = transform;}
}
