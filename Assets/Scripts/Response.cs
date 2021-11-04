using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Response{
    [Serializable]
    public class ResponseItem{
        [SerializeField] string title;
        [SerializeField] int upvote;
        [SerializeField] string dataType;
        [SerializeField] string description;
        [SerializeField] string gameId;
        [SerializeField] string creatorName;
        public ResponseItem(
            string title, int upvote, string dataType, string description, string gameId, string creatorName){
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
    [Serializable]
    public class ResponseData{
        [SerializeField] List<ResponseItem> Items;
        [SerializeField] int Count;
        [SerializeField] int ScannedCount;
        public ResponseData()
        {
            Items = new List<ResponseItem>();
            Count = Items.Count;
            ScannedCount = Items.Count;
        }
        public void AddItem(ResponseItem item){
            Items.Add(item);
            ++Count;
            ++ScannedCount;
        }
        public List<ResponseItem> GetDataList(){return Items;}
    }

    [SerializeField] int status;
    [SerializeField] string message;
    [SerializeField] List<ResponseData> data;

    public List<ResponseItem> GetDataList()
    {
        List<ResponseItem> result = new List<ResponseItem>();
        foreach(var responseData in data)
        {
            result.AddRange(responseData.GetDataList());
        }
        return result;
    }
}