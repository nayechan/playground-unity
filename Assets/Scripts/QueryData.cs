using UnityEngine;
using System;
using System.Collections.Generic;

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