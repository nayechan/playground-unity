using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DesiredColorChangeAction : DesiredUIPropertyChangeAction{
    [SerializeField] Graphic graphic;
    [SerializeField] Color color;

    public override void Action(){
        graphic.color = color;
    }
}