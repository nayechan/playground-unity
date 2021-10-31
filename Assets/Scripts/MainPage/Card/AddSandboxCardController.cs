using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MainPage.Window;

namespace MainPage.Card
{
    public class AddSandboxCardController : CardController
    {
        [SerializeField] WindowController windowToOpen;
        public override void OnClick()
        {
            windowToOpen.gameObject.SetActive(true);
            windowToOpen.GetComponent<WindowController>().OnActivateComponent();
        }
    }
}


