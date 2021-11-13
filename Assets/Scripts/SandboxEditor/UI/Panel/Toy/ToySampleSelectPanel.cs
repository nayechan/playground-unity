using System.Linq;
using SandboxEditor.Data.Storage;
using SandboxEditor.UI.Panel.Image;
using UnityEngine;

namespace SandboxEditor.UI.Panel.Toy
{
    public class ToySampleSelectPanel : MonoBehaviour
    {
        [SerializeField] private GameObject toySamplePrefab;
        public GameObject addButton;
        [SerializeField] Transform contentPanel;
        private const int SAMPLE_MAX = 100;
        private const int COL_MAX = 4;

        public void RefreshPanel()
        {
            DestroyAllSample();
            PreSetPanelSize();
            BuildAndSetPositionOfToySample();
            SetPositionOfAddButton();
        }
        private void DestroyAllSample()
        {
            foreach(Transform sample in contentPanel.transform)
                if(sample.gameObject.name != "AddToy")
                    Destroy(sample.gameObject);
        }
        
        private void PreSetPanelSize()
        {
            contentPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(
                contentPanel.GetComponent<RectTransform>().sizeDelta.x,
                400 + SAMPLE_MAX/COL_MAX*320
            );
        }
        
        private void BuildAndSetPositionOfToySample()
        {
            foreach(var(toyData, i) in ToyPrefabDataStorage.ToysData.Select(((data, i) => (data, i))))
            {
                var toySample = Instantiate(toySamplePrefab, contentPanel);
                toySample.GetComponent<RectTransform>().anchoredPosition = ImageSamplePanel.GetNthAnchoredPosition(i);
                toySample.GetComponent<ToySample>().SetToySample(toyData);
            }
        }

        private void SetPositionOfAddButton()
        {
            addButton.GetComponent<RectTransform>().anchoredPosition = ImageSamplePanel.GetNthAnchoredPosition(ToyPrefabDataStorage.Count);
        }
    }
}
