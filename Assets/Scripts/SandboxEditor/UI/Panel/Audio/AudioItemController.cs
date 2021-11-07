using SandboxEditor.Data.Resource;
using SandboxEditor.Data.Storage;
using UnityEngine;
using UnityEngine.UI;

/*
AudioEditor에 표시될 각각의 Audio 선택요소를 관리하는 스크립트입니다.
*/
namespace SandboxEditor.UI.Panel.Audio
{
    public class AudioItemController : MonoBehaviour {

        //타입
        [SerializeField] private Text typeText;

        //제목
        [SerializeField] private Text titleText;

        //재생하기 위한 AudioSource
        private AudioSource audioSource;

        AudioData _audioData;

        //Prefab 형태이기 때문에, 인스펙터에서 추가하는 형식으로는 AudioSource에 대한 선택이 불가능하여 다음과 같이 구현하였습니다.
        private void Awake() {
            audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        }

        // 이 요소에 대응하는 AudioData를 설정합니다.
        public void SetAudioData(AudioData data)
        {
            _audioData = data;
            typeText.text = _audioData.type;
            titleText.text = _audioData.fileName;
        }  


        // 이 요소가 클릭하였을 때의 동작입니다.
        public void OnButtonClicked()
        {
            AudioStorage.audioStorage.audioSelectPanel.SetActive(false);
            AudioStorage.audioStorage.selectedAudioBlock.audioSource.clip = _audioData.audioClip;
            AudioStorage.audioStorage.selectedAudioBlock.audioName = _audioData.fileName;
        }
    }
}