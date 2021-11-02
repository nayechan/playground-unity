using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Query{
    public Query(KeyCondition keyCondition, QueryFilter queryFilter)
    {
        this.keyCondition = keyCondition;
        this.queryFilter = queryFilter;
    }

    [SerializeField] KeyCondition keyCondition;
    [SerializeField] QueryFilter queryFilter;

    [Serializable]
    public class KeyCondition{
        public KeyCondition(QueryData<string> dataType, QueryData<string> gameId)
        {
            this.dataType = dataType;
            this.gameId = gameId;
        }
        [SerializeField] QueryData<string> dataType;
        [SerializeField] QueryData<string> gameId;
    }
    
    [Serializable]
    public class QueryFilter{
        public QueryFilter(QueryData<string> title, QueryData<string> creatorName, QueryData<string> description, QueryData<int> upvote)
        {
            this.title = title;
            this.creatorName = creatorName;
            this.description = description;
            this.upvote = upvote;
        }
        [SerializeField] QueryData<string> title;
        [SerializeField] QueryData<string> creatorName;
        [SerializeField] QueryData<string> description;
        [SerializeField] QueryData<int> upvote;
    }

}

[Serializable]
public class QueryData<T>{
    [SerializeField] string ComparisonOperator;
    [SerializeField] List<T> AttributeValueList;
    public QueryData(string ComparisonOperator)
    {
        this.ComparisonOperator = ComparisonOperator;
        AttributeValueList = new List<T>();
    }
    public void AddData(T data)
    {
        AttributeValueList.Add(data);
    }

}