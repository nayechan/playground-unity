using UnityEngine;
using System.Collections;
 
[ExecuteInEditMode]
public class TextMeshSort : MonoBehaviour
{
    public string LayerName = "Block";
    public int SortingOrder = 99;
 
    void Start()
    {
        GetComponent<TextMesh>().GetComponent<MeshRenderer>().sortingLayerName = LayerName;
        GetComponent<TextMesh>().GetComponent<MeshRenderer>().sortingOrder = SortingOrder;
    }
}