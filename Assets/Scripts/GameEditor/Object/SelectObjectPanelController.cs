using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEditor.Data;

public class SelectObjectPanelController : MonoBehaviour
{
    [SerializeField] private Transform contentPanel;
    [SerializeField] private GameObject objectExamplePrefab;

    public void UIRefresh(List<ToyData> toysData)
    {
        foreach(Transform transform in contentPanel)
        {
            if(transform.gameObject.name != "AddObject")
            {
                Destroy(transform.gameObject);
            }
        }

        int row = 0;
        int col = 1;

        foreach(ToyData toyData in toysData)
        {
            GameObject gameObject = Instantiate(objectExamplePrefab,contentPanel);
            gameObject.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(70+340*col, -80-320*row);
            gameObject.GetComponent<ObjectItemController>().SetDisplayInstanceData(toyData);
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
