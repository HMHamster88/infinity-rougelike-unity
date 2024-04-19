using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "StoneDungeonGeneratior", menuName = "Scriptable Objects/StoneDungeonGeneratior")]
public class StoneDungeonMapGenerator : MapGenerator
{
    [SerializeField]
    [Range(0, 10000)]
    private int Width = 64 + 1;
    [SerializeField]
    [Range(0, 10000)]
    private int Height = 64 + 1;
    [SerializeField]
    [Range(0, 10000)]
    private int RoomTries = 100;
    [SerializeField]
    [Range(1, 128)]
    private int RoomSize = 2;
    [SerializeField]
    [Range(0, 100)]
    private int WindingPercent = 50;
    [SerializeField]
    [Range(0, 100)]
    private int ExtraConnectorChance = 1;
    [SerializeField]
    private bool AddTestObjectsToFirstRoom = false;

    [SerializeField]
    private List<TileBase> WallTiles = new ();
    [SerializeField]
    private List<TileBase> FloorTiles = new ();
    [SerializeField]
    private Tile ClosedDoorTile = null;

    [SerializeField]
    private List<ContainerGenerationChance> containerGenerationChances = new ();

    [SerializeField]
    private List<CharacterGenerationChance> characterGenerationChances = new ();

    bool[,] grid;
    int[,] regions;
    int currentRegion = 0;
    RectInt bounds;

    Vector2Int[] dirs =
            {
                Vector2Int.up,
                Vector2Int.right,
                Vector2Int.down,
                Vector2Int.left
            };

    List<Vector2Int> doors = new List<Vector2Int>();
    List<RectInt> rooms = new List<RectInt>();

    public override void GenerateMap(MapBehaviour mapBehaviour, int level)
    {
        currentRegion = 0;
        bounds = new RectInt(0, 0, Width, Height);
        grid = new bool[Width, Height];
        regions = new int[Width, Height];
        doors = new List<Vector2Int>();
        rooms = new List<RectInt>();
        createRooms();
        for (int y = 1; y < Height; y += 2)
        {
            for (int x = 1; x < Width; x += 2)
            {
                if (!grid[x, y])
                {
                    growMaze(new Vector2Int(x, y));
                }
            }
        }

        connectRegions();

        removeDeadEnds();

        var wallTileMap = mapBehaviour.wallTileMap;
        var floorTileMap = mapBehaviour.floorTileMap;
        var doorsTileMap = mapBehaviour.doorsTileMap;

        var tiles = new Tile[Width, Height];
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                var position = new Vector2Int(x, y);
                if (doors.Contains(new Vector2Int(x, y)))
                {
                    doorsTileMap.SetTile((Vector3Int)position, ClosedDoorTile);
                    setTile(position, wallTileMap, WallTiles);
                    setTile(position, floorTileMap, FloorTiles);
                }
                else if (grid[x, y])
                {
                    setTile(position, floorTileMap, FloorTiles);
                    
                }
                else
                {
                    setTile(position, wallTileMap, WallTiles);
                    setTile(position, floorTileMap, FloorTiles);
                }
            }
        }

        fillRooms(mapBehaviour, level);

        if (AddTestObjectsToFirstRoom)
        {
            addContainer(mapBehaviour, rooms[0], level, containerGenerationChances.First());
            addEnemy(mapBehaviour, rooms[0], level, characterGenerationChances[0].ChraracterGenerationRule);
        }
        
        mapBehaviour.StartPosition = Vector2Int.RoundToInt(rooms.First().center);
    }

    private void fillRooms(MapBehaviour mapBehaviour, int level)
    {
        foreach(var room in rooms.Skip(1))
        {
            foreach (var containerGenerationChance in containerGenerationChances) {
                if (RandomEx.Chance(containerGenerationChance.PerRoomChance))
                {
                    addContainer(mapBehaviour, room, level, containerGenerationChance);
                }
            }
            foreach (var charcterGenerationChance in characterGenerationChances)
            {
                if (RandomEx.Chance(charcterGenerationChance.PerRoomChance))
                {
                    addEnemy(mapBehaviour, room, level, charcterGenerationChance.ChraracterGenerationRule);
                }
            }
        }
    }
    
    private void addContainer(MapBehaviour mapBehaviour, RectInt room, int level, ContainerGenerationChance containerGenerationChance)
    {
        var mapCoord = (Vector3Int) RandomEx.PointInRectInclusive(room.ExtendRect(-2));
        var worldCoords = mapBehaviour.floorTileMap.CellToWorld(mapCoord) + new Vector3(0.5f, 0.5f);
        var container = Instantiate(containerGenerationChance.ContainerPrefab, worldCoords, Quaternion.identity, mapBehaviour.mapObjects.transform);
        var mapObjectContainer = container.gameObject.GetComponent<MapObjectContainer>();
        mapObjectContainer.Level = level;
        var lootGeneartor = container.AddComponent<LootGenerator>();
        lootGeneartor.ItemGenerator = mapBehaviour.itemGenerator;
        lootGeneartor.LootGenerationRule = containerGenerationChance.LootGenerationRule;
    }

    private void addEnemy(MapBehaviour mapBehaviour, RectInt room, int level, CharacterGenerationRule characterGenerationRule)
    {
        var mapCoord = (Vector3Int)RandomEx.PointInRectInclusive(room.ExtendRect(-2));
        var worldCoords = mapBehaviour.floorTileMap.CellToWorld(mapCoord) + new Vector3(0.5f, 0.5f);
        var enemy = characterGenerationRule.Generate(worldCoords, mapBehaviour.enemies.transform, level, mapBehaviour.itemGenerator);
        var characterComponent = enemy.GetComponent<Character>();
        if (characterComponent != null)
        {
            characterComponent.Target = mapBehaviour.player;
        }
        var lootGeneartor = enemy.AddComponent<LootGenerator>();
        lootGeneartor.ItemGenerator = mapBehaviour.itemGenerator;
        lootGeneartor.LootGenerationRule = characterGenerationRule.LootGenerationRule;
    }

    private void setTile(Vector2Int position, Tilemap tilemap, List<TileBase> tiles)
    {
        Vector3Int pos2d = (Vector3Int) position;
        tilemap.SetTile(pos2d, getRandonTile(tiles));
    }

    private TileBase getRandonTile(List<TileBase> tiles)
    {
        return tiles[UnityEngine.Random.Range(0, tiles.Count)];
    }

    void removeDeadEnds()
    {
        var done = false;
        while (!done)
        {
            done = true;
            for (int y = 1; y < Height - 1; y++)
            {
                for (int x = 1; x < Width - 1; x++)
                {
                    var pos = new Vector2Int(x, y);
                    if (!grid[pos.x, pos.y])
                    {
                        continue;
                    }

                    var exits = 0;
                    foreach (var dir in dirs)
                    {
                        var p = pos + dir;
                        if (grid[p.x, p.y])
                        {
                            exits++;
                        }
                    }

                    if (exits != 1)
                    {
                        continue;
                    }

                    done = false;
                    grid[pos.x, pos.y] = false;
                }
            }
        }
    }

    void connectRegions()
    {
        var connectorRegions = new Dictionary<Vector2Int, HashSet<int>>();
        for (int y = 1; y < Height - 1; y++)
        {
            for (int x = 1; x < Width - 1; x++)
            {
                var pos = new Vector2Int(x, y);
                if (grid[pos.x, pos.y])
                {
                    continue;
                }

                var regs = new HashSet<int>();
                foreach (var dir in dirs)
                {
                    var p = pos + dir;
                    var region = regions[p.x, p.y];
                    if (region != 0)
                    {
                        regs.Add(region);
                    }
                }

                if (regs.Count < 2)
                {
                    continue;
                }

                connectorRegions[pos] = regs;
            }
        }

        var connectors = connectorRegions.Keys.ToList();

        var merged = new Dictionary<int, int>();
        var openRegions = new HashSet<int>();
        for (int i = 0; i <= currentRegion; i++)
        {
            merged[i] = i;
            openRegions.Add(i);
        }

        while (openRegions.Count > 1 && connectors.Count > 0)
        {
            var connector = connectors[UnityEngine.Random.Range(0, connectors.Count)];
            addJunction(connector);

            var regs = connectorRegions[connector].Select(r => merged[r]);

            var dest = regs.First();
            var sources = regs.Skip(1).ToList();

            for (var i = 0; i <= currentRegion; i++)
            {
                if (sources.Contains(merged[i]))
                {
                    merged[i] = dest;
                }
            }

            for (int i = 0; i < sources.Count; i++)
            {
                openRegions.Remove(sources[i]);
            }

            connectors.RemoveAll(pos =>
            {
                if ((connector - pos).magnitude < 2)
                {
                    return true;
                }

                var longSpanRegions = connectorRegions[pos].Select((region) => merged[region]).Distinct();

                if (longSpanRegions.Count() > 1)
                {
                    return false;
                }

                if (UnityEngine.Random.Range(0, 100) < ExtraConnectorChance)
                {
                    addJunction(pos);
                }

                return true;
            });
        }
    }

    void addJunction(Vector2Int pos)
    {
        grid[pos.x, pos.y] = true;
        doors.Add(pos);
    }

    void startRegion()
    {
        currentRegion++;
    }

    void carve(Vector2Int pos)
    {
        grid[pos.x, pos.y] = true;
        regions[pos.x, pos.y] = currentRegion;
    }

    bool canCarve(Vector2Int pos, Vector2Int dir)
    {
        if (!bounds.Contains(pos + dir * 3))
        {
            return false;
        }

        var p = pos + dir * 2;
        return grid[p.x, p.y] == false;
    }

    void growMaze(Vector2Int pos)
    {
        var cells = new List<Vector2Int>();
        startRegion();
        cells.Add(pos);
        carve(pos);

        Vector2Int lastDir = new Vector2Int(0, 0);

        while (cells.Count > 0)
        {
            var cell = cells.Last();
            var unmadeCells = new List<Vector2Int>();
            foreach (var dir in dirs)
            {
                if (canCarve(cell, dir))
                {
                    unmadeCells.Add(dir);
                }
            }

            if (unmadeCells.Count > 0)
            {
                Vector2Int dir;
                if (unmadeCells.Contains(lastDir) && UnityEngine.Random.Range(0, 100) > WindingPercent)
                {
                    dir = lastDir;
                }
                else
                {
                    dir = unmadeCells[UnityEngine.Random.Range(0, unmadeCells.Count)];
                }

                carve(cell + dir);
                carve(cell + dir * 2);

                cells.Add(cell + dir * 2);

                lastDir = dir;
            }
            else
            {
                cells.RemoveAt(cells.Count - 1);
                lastDir = new Vector2Int(0, 0);
            }
        }
    }

    private void createRooms()
    {
        rooms = new List<RectInt>();

        for (var i = 0; i < RoomTries; i++)
        {
            var roomWidth = UnityEngine.Random.Range(1, 3 + RoomSize) * 2 + 1;
            var roomHeight = UnityEngine.Random.Range(1, 3 + RoomSize) * 2 + 1;

            var roomX = UnityEngine.Random.Range(0, (Width - roomWidth) / 2) * 2 + 1;
            var roomY = UnityEngine.Random.Range(0, (Height - roomHeight) / 2) * 2 + 1;

            var room = new RectInt(roomX, roomY, roomWidth, roomHeight);

            var overlaps = rooms.Find(r => r.Overlaps(room));

            if (overlaps.width != 0)
            {
                continue;
            }

            rooms.Add(room);

            startRegion();
            for (int y = room.y; y < room.yMax; y++)
            {
                for (int x = room.x; x < room.xMax; x++)
                {
                    carve(new Vector2Int(x, y));
                }
            }
        }

    }
}