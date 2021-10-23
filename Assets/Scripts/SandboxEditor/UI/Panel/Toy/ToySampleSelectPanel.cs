using System.Collections.Generic;
using GameEditor.Data;
using GameEditor.Panel;
using UnityEngine;

namespace GameEditor.Object
{
    public class ToySampleSelectPanel : MonoBehaviour
    {
        [SerializeField] private GameObject toySamplePrefab;

        public void RefreshPanel(List<ToyData> toysData)
        {
            DestroyAllSample();
            var row = 0;
            var col = 1;
            BuildToySample(toysData, ref row, ref col);
            LocateToySample(ref row);
        }
        private void DestroyAllSample()
        {
            foreach(Transform sample in transform)
                if(sample.gameObject.name != "AddObject")
                    Destroy(sample.gameObject);
        }
        private void BuildToySample(List<ToyData> toysData, ref int row, ref int col)
        {
            foreach(var toyData in toysData)
            {
                var toySample = Instantiate(toySamplePrefab, transform);
                toySample.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2(70+340*col, -80-320*row);
                toySample.GetComponent<ToySample>().SetDisplayInstanceData(toyData);
                ++col;
                if (col < 4) continue;
                col=0; ++row;
            }
        }
        private void LocateToySample(ref int row)
        {
            var sizeX = GetComponent<RectTransform>().sizeDelta.x;
            GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX, 400 + row*320);
        }
    }
}
