using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FowController : MonoBehaviour
{
    public Tilemap fowTileMap;
    public TileBase fowTile;

    public void fillMap(RectInt rect)
    {
        fowTileMap.BoxFill((Vector3Int)rect.min, fowTile, rect.x, rect.y, rect.xMax, rect.yMax);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
