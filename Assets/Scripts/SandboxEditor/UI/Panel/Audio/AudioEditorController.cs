using System.Collections.Generic;
using SandboxEditor.Data.Resource;
using UnityEngine;

/*
AudioEditor를 관리하는 스크립트입니다.
*/
namespace SandboxEditor.UI.Panel.Audio
{
    public class AudioEditorController : MonoBehaviour
    {

        [SerializeField] GameObject audioItemPrefab;
        [SerializeField] Transform contentPanel;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        // UI를 Refresh 합니다 (다른 곳에서 AudioData 리스트를 넘겨서 호출하면, 그걸 바탕으로 UI를 재생성)
        public void RefreshUI(List<AudioData> audioDatas)
        {
            foreach(Transform transform in contentPanel)
            {
                if(transform.gameObject.name != "AddObject")
                    Destroy(transform.gameObject);
            }

            int row = 0;
            int col = 1;

            foreach(AudioData data in audioDatas)
            {
                GameObject gameObject = Instantiate(audioItemPrefab,contentPanel);
                gameObject.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2(70+340*col, -80-320*row);

                gameObject.GetComponent<AudioItemController>().SetAudioData(data);

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
}
