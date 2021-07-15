using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileBuilder : MonoBehaviour
{
    private Dictionary<KeyValuePair<float,float>, GameObject> _tiles;
    public GameObject selectedTile;
    public GameObject tiles;
    // Start is called before the first frame update
    void Start()
    {
        _tiles = new Dictionary<KeyValuePair<float, float>, GameObject>();
    }

    public bool GenerateTile(Vector3 cursor){
        KeyValuePair<float,float> pair = new KeyValuePair<float, float>(cursor.x, cursor.y);
        if(_tiles.ContainsKey(pair)){
            return false;
        }
        GameObject obj = Instantiate(selectedTile, cursor, Quaternion.identity, tiles.transform);
        _tiles.Add(pair, obj);
        return true;
    }
    
    public bool RemoveTile(Vector3 cursor){
        KeyValuePair<float,float> pair = new KeyValuePair<float, float>(cursor.x, cursor.y);
        if(!_tiles.ContainsKey(pair)){
            return false;
        }
        Destroy(_tiles[pair]);
        _tiles.Remove(pair);
        return true;
    }

    public void SelectTile(GameObject _tile){
        selectedTile = _tile;
    }
}
