using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObjectPanelController : MonoBehaviour
{
    private List<ObjectPrimitiveData> objectPrimitiveDatas;
    [SerializeField] private Transform contentPanel;
    [SerializeField] private GameObject objectExamplePrefab;
    void Awake()
    {
        objectPrimitiveDatas = new List<ObjectPrimitiveData>();
    }

    void Update()
    {
        
    }

    public void UIRefresh()
    {
        foreach(Transform transform in contentPanel)
        {
            if(transform.gameObject.name != "AddObject")
                Destroy(transform.gameObject);
        }

        int row = 0;
        int col = 1;

        
        foreach(ObjectPrimitiveData data in objectPrimitiveDatas)
        {
            GameObject gameObject = Instantiate(objectExamplePrefab,contentPanel);
            gameObject.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(70+340*col, -80-320*row);
            gameObject.GetComponent<ObjectTemplateController>().SetData(data);
            ++col;
            if(col>=4) {col=0; ++row;}
        }

        float sizeX = contentPanel.GetComponent<RectTransform>().sizeDelta.x;

        contentPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(
            sizeX,
            400 + row*320
        );
    }

    public void AddObject(ObjectPrimitiveData data)
    {
        objectPrimitiveDatas.Add(data);
        UIRefresh();
    }
}
