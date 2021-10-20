using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEditor.Data;

public class SelectToySamplePanelController : MonoBehaviour
{
    [SerializeField] private Transform samplePanel;
    [SerializeField] private GameObject toySamplePrefab;

    public void UIRefresh(List<ToyData> toysData)
    {
        foreach(Transform transform in samplePanel)
        {
            if(transform.gameObject.name != "AddObject")
            {
                Destroy(transform.gameObject);
            }
        }
        int row = 0;
        int col = 1;

        BuildSampleToys(toysData, ref row, ref col);
        RelocateToyAddButton(ref row);

    }

    private void BuildSampleToys(List<ToyData> toysData, ref int row, ref int col)
    {
        foreach(ToyData toyData in toysData)
        {
            GameObject toySample = Instantiate(toySamplePrefab, samplePanel);
            toySample.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(70+340*col, -80-320*row);
            toySample.GetComponent<ToySample>().SetDisplayInstanceData(toyData);
            ++col;
            if(col>=4) {col=0; ++row;}
        }
    }

    private void RelocateToyAddButton(ref int row)
    {
        float sizeX = samplePanel.GetComponent<RectTransform>().sizeDelta.x;
        samplePanel.GetComponent<RectTransform>().sizeDelta = new Vector2(
            sizeX,
            400 + row*320
        );
    }
}
