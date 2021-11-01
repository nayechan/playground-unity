using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MainPage.Panel;

public class SearchInputField : MonoBehaviour
{
    [SerializeField] float delayTime;
    [SerializeField] SearchPanelController searchPanelController;
    string searchQuery = "";
    public void OnInputOccured()
    {
        searchQuery = GetComponent<InputField>().text;
        StartCoroutine("SendQuery");
    }

    IEnumerator SendQuery()
    {
        yield return new WaitForSeconds(delayTime);
        searchPanelController.UpdateComponent();
    }
}
