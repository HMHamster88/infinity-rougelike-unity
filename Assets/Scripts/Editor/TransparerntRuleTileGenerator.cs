using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Progress;

#if UNITY_EDITOR
internal class TransparerntRuleTileGenerator: EditorWindow
{
    public Sprite[] tilesSprites = new Sprite[0];
    public string savePath = "Assets/RuleTile.asset";

    public List<Vector3Int> NeighborPositions = new List<Vector3Int>()
    {
        new Vector3Int(-1, 1, 0),
        new Vector3Int(0, 1, 0),
        new Vector3Int(1, 1, 0),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(-1, -1, 0),
        new Vector3Int(0, -1, 0),
        new Vector3Int(1, -1, 0),
    };

    [MenuItem("VinTools/Editor Windows/Transparernt Tile Generator")]
    public static void ShowWindow()
    {
        GetWindow<TransparerntRuleTileGenerator>("Transparernt Tile Generator");
    }

    private void OnGUI()
    {
        savePath = EditorGUILayout.TextField("Save Path", savePath);
        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty prp = so.FindProperty("tilesSprites");
        EditorGUILayout.PropertyField(prp, true); // True means show children
        so.ApplyModifiedProperties();
        defaultSprite = (Sprite)EditorGUILayout.ObjectField(defaultSprite, typeof(Sprite), false);
        if (GUILayout.Button("Generate"))
        {
            generate();
        }
    }

    public Sprite defaultSprite;
    public Tile.ColliderType colliderType = Tile.ColliderType.Sprite;
    public GameObject defaultGameobject;
    public bool addGameobjectsToRules;

    private void generate()
    {
        RuleTile tile = ScriptableObject.CreateInstance<RuleTile>();
        //set default tile
        tile.m_DefaultSprite = defaultSprite;
        tile.m_DefaultColliderType = colliderType;
        tile.m_DefaultGameObject = defaultGameobject;

        foreach (var tileSprite in tilesSprites)
        {
            RuleTile.TilingRule rule = new RuleTile.TilingRule();
            rule.m_Sprites[0] = tileSprite;
            rule.m_Neighbors = getNeighbors(tileSprite);
            rule.m_ColliderType = colliderType;
            if (addGameobjectsToRules) rule.m_GameObject = defaultGameobject;

            tile.m_TilingRules.Add(rule);

        }

        AssetDatabase.CreateAsset(tile, savePath);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = tile;
    }

    private List<int> getNeighbors(Sprite sprite)
    {
        Rect slice = sprite.rect;
        Color[] cols = sprite.texture.GetPixels((int)slice.x, (int)slice.y, (int)slice.width, (int)slice.height);

        //create texture
        Texture2D tex = new Texture2D((int)slice.width, (int)slice.height, TextureFormat.ARGB32, false);
        tex.SetPixels(0, 0, (int)slice.width, (int)slice.height, cols);
        tex.filterMode = FilterMode.Point;
        tex.Apply();

        var result = new List<int>();

        Vector2Int size = new Vector2Int(tex.width, tex.height);

        //set rules based on the color of the pixels
        foreach (var neighbor in NeighborPositions)
        {
            int xPos = 0;
            int yPos = 0;

            //get x pixel coordinate
            switch (neighbor.x)
            {
                case 0:
                    xPos = size.x / 2;
                    break;
                case 1:
                    xPos = size.x - 1;
                    break;
            }

            //get y pixel coordinate
            switch (neighbor.y)
            {
                case 0:
                    yPos = size.y / 2;
                    break;
                case 1:
                    yPos = size.y - 1;
                    break;
            }

            //get the pixel color
            Color c = tex.GetPixel(xPos, yPos);

            if (c.a == 0)
            {
                if (neighbor.x != 0 && neighbor.y != 0)
                {
                    result.Add(0);
                } 
                else
                {
                    result.Add(RuleTile.TilingRule.Neighbor.NotThis);
                }
            } 
            else
            {
                result.Add(RuleTile.TilingRule.Neighbor.This);
            }
        }
        return result;
    }

}
#endif
