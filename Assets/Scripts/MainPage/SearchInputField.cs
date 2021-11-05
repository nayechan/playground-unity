using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MainPage.Panel;

public class SearchInputField : MonoBehaviour
{
    [SerializeField] float delayTime;
    [SerializeField] SearchPanelController searchPanelController;
    [SerializeField] QuerySandbox querySandbox;
    [SerializeField] Text statusText;
    [SerializeField] Dropdown filterTypeDropdown;
    Response response = null;
    string searchQuery = "";
    public void OnInputOccured()
    {
        StopCoroutine("SendQuery");
        searchQuery = GetComponent<InputField>().text;
        if(searchQuery != "")
        {
            StartCoroutine("SendQuery");
        }
    }

    IEnumerator SendQuery()
    {
        yield return new WaitForSeconds(delayTime);
        QueryProcess();
        
    }

    public void QueryProcess()
    {
        string filterType = filterTypeDropdown.options[filterTypeDropdown.value].text;

        QueryData<string> dataType = new QueryData<string>("EQ");
        QueryData<string> gameId = new QueryData<string>("BEGINS_WITH");

        QueryData<string> title = new QueryData<string>("CONTAINS");
        QueryData<string> creatorName = new QueryData<string>("CONTAINS");
        QueryData<string> desription = new QueryData<string>("CONTAINS");
        QueryData<int> upvote = new QueryData<int>("GE");


        dataType.AddData("GameAttribute");
        gameId.AddData("20");

        if(filterType == "제목")
        {
            title.AddData(searchQuery);
            creatorName.AddData("");
        }
        else if(filterType == "제작자")
        {
            title.AddData("");
            creatorName.AddData(searchQuery);
        }
        else
        {
            title.AddData("");
            creatorName.AddData("");
        }

        desription.AddData("");
        upvote.AddData(int.MinValue);

        SandboxQuery query = new SandboxQuery(
            new SandboxQuery.KeyCondition(
                dataType,
                gameId
            ),
            new SandboxQuery.QueryFilter(
                title,
                creatorName,
                desription,
                upvote
            )
        );


        statusText.text = "검색 중 ...";

        StartCoroutine(querySandbox.SendRequest(query, OnReceiveResponse));
    }

    public void OnReceiveResponse(Response response)
    {
        searchPanelController.OnSearchResult(response);
        statusText.text = response.GetDataList().Count +"개의 게임을 찾았습니다.";
        Debug.Log(response);
    }

    public Response GetReceivedResponse()
    {
        return response;
    }
}

public class SwitchUIProperty{
    [SerializeField] List<DesiredUIPropertyChangeAction> dataPairList;

    public void SwitchProperty()
    {
        foreach(DesiredUIPropertyChangeAction action in dataPairList)
        {
            action.Action();
        }
    }
}