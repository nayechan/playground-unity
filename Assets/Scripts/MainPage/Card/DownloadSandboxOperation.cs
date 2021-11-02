using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownloadSandboxOperation : SandboxCardOnClickOperation
{
    Response responseData;
    public override void execute()
    {
        
    }
    public void setResponseData(Response response)
    {
        responseData = response;
    }
}
