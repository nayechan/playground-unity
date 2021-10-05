using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TemporaryGameEditorDataManager : MonoBehaviour
{
    [SerializeField] private GameObject ourObjectRoot, ourTileRoot, ourBlockRoot;
    private static TemporaryGameEditorDataManager _dm;
    private List<ObjectPrimitiveData> datas;
    // Start is called before the first frame update

    void Awake(){
        _dm = this;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttachTheirObjects(GameObject theirObjectRoot)
    {
        foreach(Transform t in theirObjectRoot.transform.Cast<Transform>().ToList())
        {
            if(t.name == "currentObject") continue;
            t.parent = ourObjectRoot.transform;
            t.gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.0f);
            Component com = t.GetComponent<Collider>();
            if(com) Destroy(com);
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

    public void AttachTheirBlocks(GameObject theirBlockRoot)
    {
        foreach(Transform t in theirBlockRoot.transform.Cast<Transform>().ToList())
        {
            t.parent = ourBlockRoot.transform;
            // t.gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
            // block 객체에 SpriteRenderer가 없어서 보이지 않게 하는 다른 방법을 사용해야 할 듯.
        }
    }

    public void LoadObjectTypes(ObjectDataManager dataManager)
    {
        datas = dataManager.GetObjectPrimitiveDatas();
    }

    public void RestoreObjectTypes(ObjectDataManager dataManager)
    {
        dataManager.SetDatas(datas);
    }

    public void FetchOurObjects(GameObject theirObjectRoot)
    {
        TouchController_obj touchController = GameObject.FindObjectOfType<TouchController_obj>();

        foreach(Transform t in ourObjectRoot.transform.Cast<Transform>().ToList())
        {
            if(t.gameObject.name != "currentObject")
            {
                t.parent = theirObjectRoot.transform;
                t.gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
            }
            if(touchController != null){
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

    
    public void FetchOurBlocks(GameObject theirBlockRoot)
    {
        foreach(Transform t in ourBlockRoot.transform.Cast<Transform>().ToList())
        {
            t.parent = theirBlockRoot.transform;
        }
        EventEditorDeplomat edd = EventEditorDeplomat.GetEED();
        edd.RefreshObjects(ourObjectRoot, ourTileRoot);
        EventBlockController.GetEBC().SetRoots(ourObjectRoot,ourTileRoot);
    }

    static public TemporaryGameEditorDataManager GetDM(){
        return _dm;
    }
}