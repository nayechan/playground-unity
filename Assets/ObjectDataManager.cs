using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDataManager : MonoBehaviour
{
    [SerializeField] SelectObjectPanelController selectObjectPanel;
    private List<ObjectPrimitiveData> objectPrimitiveDatas;
    private void Awake() {
        
        objectPrimitiveDatas = new List<ObjectPrimitiveData>();
    }
    public List<ObjectPrimitiveData> GetObjectPrimitiveDatas()
    {
        return objectPrimitiveDatas;
    }
    public void AddObject(ObjectPrimitiveData data)
    {
        objectPrimitiveDatas.Add(data);
        selectObjectPanel.UIRefresh();
    }
}
