using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    int imgIndex = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(sprites.Count > 0)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[imgIndex];
            ++imgIndex;
            if(imgIndex >= sprites.Count)
                imgIndex = 0;
        }
        
    }

    void SetSprites(List<Sprite> sprites){this.sprites = sprites;}
}
