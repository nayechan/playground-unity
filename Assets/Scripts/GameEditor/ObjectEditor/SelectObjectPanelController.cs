using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObjectPanelController : MonoBehaviour
{
    [SerializeField] private ObjectDataManager objectDataManager;
    [SerializeField] private Transform contentPanel;
    [SerializeField] private GameObject objectExamplePrefab;

    public void UIRefresh()
    {

        List<ObjectPrimitiveData> datas = objectDataManager.GetObjectPrimitiveDatas();
        foreach(Transform transform in contentPanel)
        {
            if(transform.gameObject.name != "AddObject")
                Destroy(transform.gameObject);
        }

        int row = 0;
        int col = 1;

        foreach(ObjectPrimitiveData data in datas)
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
}
