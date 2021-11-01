using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuerySandbox : MonoBehaviour
{
    Query query;
    private void Awake() {
        QueryData<string> dataType = new QueryData<string>("EQ");
        QueryData<string> gameId = new QueryData<string>("BEGINS_WITH");
        QueryData<string> creatorName = new QueryData<string>("CONTAINS");
        QueryData<string> desription = new QueryData<string>("CONTAINS");
        QueryData<int> upvote = new QueryData<int>("GE");


        dataType.AddData("GameAttribute");
        gameId.AddData("20");
        creatorName.AddData("black");
        desription.AddData("");
        upvote.AddData(int.MinValue);

        Query query = new Query(
            new Query.KeyCondition(
                dataType,
                gameId
            ),
            new Query.QueryFilter(
                creatorName,
                desription,
                upvote
            )
        );

        string jsonResult = JsonUtility.ToJson(query, true);
        Debug.Log(jsonResult);

        Response response = new Response();
        response.AddItem(new Response.ResponseItem(
            "Title",
            0,
            "GameAttribute",
            "Description",
            "20",
            "Creator"
        ));
    }
    public Response GetResponse(){
        Response response = new Response();
        int r = Random.Range(3,35);
        for(int i=0;i<r;++i)
        {
            response.AddItem(new Response.ResponseItem(
                "Test "+i,
                0,
                "GameAttribute",
                "Description",
                "20",
                "Creator"
            ));
        }
        
        return response;
    }
}
