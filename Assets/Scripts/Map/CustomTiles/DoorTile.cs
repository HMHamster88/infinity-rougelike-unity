using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorTile : InteractableTile
{
    [SerializeField]
    private List<AudioClip> openDoorSounds = new List<AudioClip>();

    public override void Interact(MapBehaviour map,Vector2Int position)
    {
        map.wallTileMap.SetTile((Vector3Int)position, null);
        map.doorsTileMap.SetTile((Vector3Int)position, null);
        if (openDoorSounds.Count != 0)
        {
            AudioSource.PlayClipAtPoint(openDoorSounds.GetRandomElement(), map.doorsTileMap.CellToWorld((Vector3Int)position));
        }
    }

#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a RoadTile Asset
    [MenuItem("Assets/Create/DoorTile")]
    public static void CreateRoadTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Door Tile", "Door Tile", "Asset", "Save Door Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<DoorTile>(), path);
    }
#endif
}
