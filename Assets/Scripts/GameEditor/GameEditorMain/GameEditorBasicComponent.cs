using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEditorBasicComponent : MonoBehaviour
{
    private void Awake() {
        DontDestroyOnLoad(gameObject);   
    }
}
