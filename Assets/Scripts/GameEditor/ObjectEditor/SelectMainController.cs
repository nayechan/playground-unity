using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMainController : MonoBehaviour
{
    private string currentMode;
    [SerializeField] private GameObject selectObjectPanel, createObjectPanel;
    // Start is called before the first frame update
    void Start()
    {
        currentMode = "Select";
        UIRefresh();
    }

    public void SetCurrentMode(string mode)
    {
        switch(currentMode)
        {
            case "Select":
            selectObjectPanel.SetActive(false);
            break;
            case "Create":
            createObjectPanel.SetActive(false);
            break;
        }
        currentMode = mode;
        UIRefresh();
    }

    void UIRefresh()
    {
        switch(currentMode)
        {
            case "Select":
            selectObjectPanel.SetActive(true);
            break;
            case "Create":
            createObjectPanel.SetActive(true);
            break;
        }
    }
}
