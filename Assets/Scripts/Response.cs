using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Response{
    [Serializable]
    public class ResponseItem{
        string title;
        int upvote;
        string dataType;
        string description;
        string gameId;
        string creatorName;
        public ResponseItem(string title, int upvote, string dataType, string description, string gameId, string creatorName){
            this.title = title;
            this.upvote = upvote;
            this.dataType = dataType;
            this.description = description;
            this.gameId = gameId;
            this.creatorName = creatorName;
        }

        public string getTitle(){return title;}
        public int getUpVote(){return upvote;}
        public string getDescription(){return description;}
        public string getGameID(){return gameId;}
        public string getCreatorName(){return creatorName;}


    }
    [SerializeField] List<ResponseItem> items;
    [SerializeField] int Count;
    [SerializeField] int ScannedCount;
    public Response()
    {
        items = new List<ResponseItem>();
        Count = items.Count;
        ScannedCount = items.Count;
    }

    public void AddItem(ResponseItem item){
        items.Add(item);
        ++Count;
        ++ScannedCount;
    }

    public List<ResponseItem> GetDataList()
    {
        return items;
    }
}