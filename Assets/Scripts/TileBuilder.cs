using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileBuilder : MonoBehaviour
{
    private Dictionary<KeyValuePair<float,float>, GameObject> _tiles;
    public GameObject tile;
    // Start is called before the first frame update
    void Start()
    {
        _tiles = new Dictionary<KeyValuePair<float, float>, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GenerateTile(Vector3 cursor){
        KeyValuePair<float,float> pair = new KeyValuePair<float, float>(cursor.x, cursor.y);
        if(_tiles.ContainsKey(pair)){
            return false;
        }
        GameObject obj = Instantiate(tile, cursor, Quaternion.identity);
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
}
