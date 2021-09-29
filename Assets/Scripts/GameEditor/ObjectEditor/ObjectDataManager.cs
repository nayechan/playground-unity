using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 얘가 하는일
 *  1. 임포트된 이미지를 프리미티브 데이터형태로 받아서
 * 리스트에 넣는거
 *  2. 딴애들이 리스트 필요하면 전달하는거. (get)
 *  3. 데이터 리스트를 받으면 자기의 리스트를 그걸로 바꾸는거.(set)
 * [ ] 그런데 ReloadImage ... 랑 UIRefresh가 무슨일하는지 확인 팰요
 *  -> ReloadImage는 PrimitiveObject가 갖고있는 SpritePath를 갖고 Sprite List를 새로 로드하는 과정임.
 *  -> UIRefresh는 selecObjectPanel이 자기가 연결되 있는 ObjectManager의 primitiveDatas를 갖고
 *  UI를 갱신하는 과정임. 이중간에 objectExamplePrefab 이 갖고 있는 ~~Template
 * 컴포넌트를 사용하는데 더 찾아봐야 겠음.
 */
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
