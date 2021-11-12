using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomImage : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;
    
    private void Awake() {
        if(sprites.Count > 0)
        {
            int index = Random.Range(0, sprites.Count);
            GetComponent<Image>().sprite = sprites[index];
        }
    }
}
