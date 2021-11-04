using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class SandboxQuery : BaseQuery{
    public SandboxQuery(KeyCondition keyCondition, QueryFilter queryFilter) : base(keyCondition)
    {
       // this.keyCondition = keyCondition;
        this.queryFilter = queryFilter;
    }

    //[SerializeField] KeyCondition keyCondition;
    [SerializeField] QueryFilter queryFilter;
    
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