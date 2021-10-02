using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDataManager : MonoBehaviour
{
    [SerializeField] SelectObjectPanelController selectObjectPanel;
    [SerializeField] private List<ObjectPrimitiveData> objectPrimitiveDatas;
    private void Awake() {
        
        objectPrimitiveDatas = new List<ObjectPrimitiveData>();
    }
    public List<ObjectPrimitiveData> GetObjectPrimitiveDatas()
    {
        return objectPrimitiveDatas;
    }
    public void AddObject(ObjectPrimitiveData data)
    {
        Debug.Log(JsonUtility.ToJson(data));
        objectPrimitiveDatas.Add(data);
        selectObjectPanel.UIRefresh();
    }

    public List<ObjectPrimitiveData> GetDatas()
    {
        return objectPrimitiveDatas;
    }
    // public string GetDataAsJson()
    // {
    //     string jsonResult = JsonUtility.ToJson(objectPrimitiveDatas);
    //     Debug.Log(jsonResult);
    //     return jsonResult;
    // }

    // public void SetDataByJson(string jsonData)
    // {
    //     objectPrimitiveDatas = JsonUtility.FromJson<List<ObjectPrimitiveData>>(jsonData);
    //     foreach(ObjectPrimitiveData data in objectPrimitiveDatas)
    //     {
    //         data.ReloadImageFromPath();
    //     }
    //     selectObjectPanel.UIRefresh();
    // }

    public void SetDatas(List<ObjectPrimitiveData> datas)
    {
        if(datas != null)
        {
            objectPrimitiveDatas = datas;
            foreach(ObjectPrimitiveData data in objectPrimitiveDatas)
            {
                data.ReloadImageFromPath();
            }
            selectObjectPanel.UIRefresh();
        }
        
    }
}
