using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TemporaryGameEditorDataManager : MonoBehaviour
{
    [SerializeField] private GameObject ourObjectRoot, ourTileRoot;
    private List<ObjectPrimitiveData> datas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttachTheirObjects(GameObject theirObjectRoot)
    {
        foreach(Transform t in theirObjectRoot.transform.Cast<Transform>().ToList())
        {
            t.parent = ourObjectRoot.transform;
            t.gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.0f);
        }
    }

    public void AttachTheirTiles(GameObject theirTileRoot)
    {
        foreach(Transform t in theirTileRoot.transform.Cast<Transform>().ToList())
        {
            t.parent = ourTileRoot.transform;
            t.gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.2f);
        }
    }

    public void LoadObjectTypes(ObjectDataManager dataManager)
    {
        datas = dataManager.GetDatas();
    }

    public void RestoreObjectTypes(ObjectDataManager dataManager)
    {
        dataManager.SetDatas(datas);
    }

    public void FetchOurObjects(GameObject theirObjectRoot)
    {
        TouchController_obj touchController = 
        GameObject.Find("TouchController").GetComponent<TouchController_obj>();

        foreach(Transform t in ourObjectRoot.transform.Cast<Transform>().ToList())
        {
            if(t.gameObject.name != "currentObject")
            {
                t.parent = theirObjectRoot.transform;
                t.gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
                t.gameObject.GetComponent<ObjectInstanceController>().setTouchController(
                    touchController
                );
            }

        }
    }

    public void FetchOurTiles(GameObject theirTileRoot)
    {
        foreach(Transform t in ourTileRoot.transform.Cast<Transform>().ToList())
        {
            t.parent = theirTileRoot.transform;
            t.gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
        }
    }
}
