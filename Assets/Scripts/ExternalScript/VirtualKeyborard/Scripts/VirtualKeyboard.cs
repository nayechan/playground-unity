using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace VirtualKeyboard
{
    public class VirtualKeyboard : MonoBehaviour
    {
        public KeyboardFunc keyboard;
        public InputField inputAnswer;
        public CanvasGroup inputArea;

        char[] _currentText = new char[0];
        private void Awake() {
            inputArea = transform.parent.GetComponent<CanvasGroup>();
        }
        void Start()
        {
            //inputAnswer.onFocusSelectAll = false;

            keyboard.gameObject.SetActive(true);
            keyboard.onKeyPressEvent = (string character) => {
                inputAnswer.text += character;
            };
            keyboard.onBackKeyPressEvent = () =>
            {
                inputAnswer.text = inputAnswer.text.Remove(inputAnswer.text.Length - 1);
            };
            keyboard.onEnterKeyPressEvent = () =>
            {
                inputAnswer.text += "\n";
            };
        }

        // Update is called once per frame
        void Update()
        {
            // if (inputAnswer != null && inputAnswer.isFocused == false)
            // {
            //     EventSystem.current.SetSelectedGameObject(inputAnswer.gameObject, null);
            //     inputAnswer.OnPointerClick(new PointerEventData(EventSystem.current));
            // }
        }

        private void OnInputFieldSubmit(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                // some stuff with message
                inputAnswer.text = "";
            }
        }

        public void ShowKeyboard(bool flag)
        {
            if (flag)
            {
                AnimationHelper.lerpMe(0.2f, (float percent) => {
                    inputArea.alpha = Mathf.Lerp(0, 1, percent);
                }, () => {
                    inputArea.interactable = true;
                    inputArea.blocksRaycasts = true;
                });
                inputAnswer.ActivateInputField();
                inputAnswer.Select();
                inputAnswer.shouldHideMobileInput = true;
                //inputAnswer.shouldHideSoftKeyboard = false;
                keyboard.ShowKeyboard(true, false);
            }
            else
            {
                inputArea.interactable = false;
                inputArea.blocksRaycasts = false;
                inputArea.alpha = 0;
                inputAnswer.text = "";
                inputAnswer.shouldHideMobileInput = true;
                inputAnswer.DeactivateInputField();
                EventSystem.current.SetSelectedGameObject(null);
                keyboard.ShowKeyboard(false, false);
            }
        }

        public void OnInputFieldChange()
        {
            string wholdWord = inputAnswer.text;
            char[] listChars = wholdWord.ToCharArray();

            // backup 
            _currentText = listChars;
            inputAnswer.caretPosition = _currentText.Length;

        }

        public void SetInputField(InputField inputField)
        {
            if(inputAnswer != null)
                inputAnswer.onSubmit.RemoveAllListeners();
            this.inputAnswer = inputField;
            inputAnswer.onSubmit.AddListener(OnInputFieldSubmit);
            
            keyboard.gameObject.SetActive(true);
        }

        public void LoseFocus()
        {
            if(inputAnswer != null)
            {
                inputAnswer.onSubmit.RemoveAllListeners();
                keyboard.gameObject.SetActive(false);
            }
        }


    }
}