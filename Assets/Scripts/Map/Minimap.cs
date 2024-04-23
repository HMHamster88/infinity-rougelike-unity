using System;
using System.Linq;
using UnityEngine;

public class Minimap : InjectComponentBehaviour
{
    [GetComponent]
    MapBehaviour mapBehaviour;

    public Texture2D allMapTexture;

    public Texture2D texture;

    [SerializeField]
    private int viewRange = 7;
    [SerializeField]
    private Color floorColor = Color.gray;
    [SerializeField]
    private Color wallColor = Color.black;
    [SerializeField]
    private Color playerColor = Color.red;
    [SerializeField]
    private Color backgroundColor = Color.black;
    [SerializeField]
    private bool openAllMapOnInit = false;

    private Vector3Int lastPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame

    public void OpenAllMap()
    {
        openRegion(new Vector3Int(0, 0), new Vector3Int(allMapTexture.width, allMapTexture.height));
    }
    void Update()
    {
        var currentPosition = mapBehaviour.floorTileMap.WorldToCell(Camera.main.transform.position);
        if (currentPosition != lastPosition)
        {
            var lowerBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
            var upperBound = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

            var lowerBoundCell = mapBehaviour.floorTileMap.WorldToCell(lowerBound);
            var upperBoundCell = mapBehaviour.floorTileMap.WorldToCell(upperBound);
            openRegion(lowerBoundCell, upperBoundCell);
            allMapTexture.SetPixel(currentPosition.x, currentPosition.y, playerColor);
            lastPosition = currentPosition;
            allMapTexture.Apply();

            updateTexture(currentPosition);
        }
    }

    private void openRegion(Vector3Int lowerBoundCell, Vector3Int upperBoundCell)
    {
        for (int y = lowerBoundCell.y; y < upperBoundCell.y; y++)
        {
            for (int x = lowerBoundCell.x; x < upperBoundCell.x; x++)
            {
                var floorTile = mapBehaviour.floorTileMap.GetTile(new Vector3Int(x, y, 0));
                if (floorTile != null)
                {
                    allMapTexture.SetPixel(x, y, floorColor);
                }
                var wallTile = mapBehaviour.wallTileMap.GetTile(new Vector3Int(x, y, 0));
                if (wallTile != null)
                {
                    allMapTexture.SetPixel(x, y, wallColor);
                }
            }
        }
    }
    private void updateTexture(Vector3Int currentPosition)
    {
        var textureWidth = texture.width;
        fillTexture(texture, backgroundColor);
        var x = Math.Max(currentPosition.x - viewRange, 0);
        var y = Math.Max(currentPosition.y - viewRange, 0);
        var right = Mathf.Min(x + textureWidth, allMapTexture.width);
        var bottom = Mathf.Min(y + textureWidth, allMapTexture.height);
        Graphics.CopyTexture(allMapTexture, 0, 0, x, y, right - x, bottom - y, texture, 0, 0, 0, 0);
    }

    public void InitMinimap(int width, int height)
    { 
        allMapTexture = new Texture2D(width, height, TextureFormat.ARGB32, false);
        allMapTexture.filterMode = FilterMode.Point;
        fillTexture(allMapTexture, backgroundColor);
        texture = new Texture2D(viewRange * 2 + 1, viewRange * 2 + 1, TextureFormat.ARGB32, false);
        texture.filterMode = FilterMode.Point;
        fillTexture(texture, backgroundColor);

        if (openAllMapOnInit)
        {
            OpenAllMap();
        }
    }

    public void fillTexture(Texture2D texture, Color color)
    {
        Color[] pixels = Enumerable.Repeat(color, texture.width * texture.height).ToArray();
        texture.SetPixels(pixels);
        texture.Apply();
    }

}
