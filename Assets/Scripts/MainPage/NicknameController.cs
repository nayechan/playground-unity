using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NicknameController : MonoBehaviour
{
    [SerializeField] GameObject blackPanel, nickNameWindow;
    [SerializeField] InputField nickNameInputField;
    [SerializeField] Text welcomeText;
    string nickName;

    private void Awake() {
        nickName = PlayerPrefs.GetString("myNickName","");

        Debug.Log(nickName);

        if(nickName != "")
        {
            welcomeText.text = string.Format("{0} 님, <b>환영합니다!</b>",nickName);
        }
        else
        {
            blackPanel.SetActive(true);
            nickNameWindow.SetActive(true);
        }
    }
    
    public void InputNickName()
    {
        if(nickNameInputField.text != "")
        {
            nickName = nickNameInputField.text;
        }
        else
        {
            nickName = "User"+Random.Range(10000000,99999999);
        }
        welcomeText.text = string.Format("{0} 님, <b>환영합니다!</b>",nickName);
        PlayerPrefs.SetString("myNickName",nickName);

        blackPanel.SetActive(false);
        nickNameWindow.SetActive(false);
    }
}
