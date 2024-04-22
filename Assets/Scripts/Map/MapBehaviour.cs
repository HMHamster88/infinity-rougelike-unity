using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class MapBehaviour : MonoBehaviour
{
    [SerializeField]
    public Tilemap floorTileMap;
    [SerializeField]
    public Tilemap wallTileMap;
    [SerializeField]
    public Tilemap doorsTileMap;
    [SerializeField]
    public Tilemap fowTileMap;
    [SerializeField]
    public GameObject mapObjects;
    [SerializeField]
    public GameObject enemies;
    [SerializeField]
    public ItemGenerator itemGenerator;
    public GameObject player;

    public UnityEvent<Vector2> OnStartPositionChanged;

    private Vector2Int startPosition;

    public Texture2D Minimap;

    public Vector2Int StartPosition { 
        get => startPosition; 
        set
        {
            startPosition = value;
            OnStartPositionChanged.Invoke(floorTileMap.CellToWorld((Vector3Int)value));
        }
    }

    public void InteractWithMapObjects(Vector3 interactCharacterPosition, Vector3 mouseWorldPoint, float maxInteractDistance)
    {
        var cellPosition = doorsTileMap.WorldToCell(mouseWorldPoint);
        if ((cellPosition - interactCharacterPosition).magnitude < maxInteractDistance) 
        {
            var tile = doorsTileMap.GetTile(cellPosition);
            if (tile is InteractableTile doorTile)
            {
                doorTile.Interact(this, (Vector2Int)cellPosition);
            }
        }
    }
}
