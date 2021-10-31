using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DesiredTextStyleChangeAction : DesiredUIPropertyChangeAction{
    [SerializeField] Text text;
    [SerializeField] FontStyle fontStyle;

    public override void Action(){
        text.fontStyle = fontStyle;
    }
}