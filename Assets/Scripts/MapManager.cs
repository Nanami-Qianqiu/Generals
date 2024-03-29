using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    [Header("Tilemaps")]
    [SerializeField] private Tilemap tilemapColor;
    [SerializeField] private Tilemap tilemapCell;
    [SerializeField] private Tilemap tileCrusor;

    [Header("Textures")]
    [SerializeField] private Tile[] cellTiles;
    [SerializeField] private Tile[] colorTiles;

    [Header("Map Propeties")]
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;

    private Cell[,] map;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        GenerateMap();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        DrawMap();
    }


    void GenerateMap()
    {
        map = new Cell[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = new Cell();
                if (x == 0 && y == 0)
                {
                    map[x, y] = new Cell(Cell.CellType.CAPITAL, 0, 10);
                }
            }
        }
    }

    public void DrawMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tilemapCell.SetTile(new Vector3Int(x, y, 0), cellTiles[(int)map[x, y].Type]);
                tilemapColor.SetTile(new Vector3Int(x, y, 0), colorTiles[map[x, y].Owner]);
            }
        }
    }

    public void DrawCursor(Vector3Int pos, int mode)
    {
        int[] dx = {-1, 0, 1, 0};
        int[] dy = {0, 1, 0, -1};
        tileCrusor.SetTile(pos, cellTiles[5]);
        if (mode == 2)
        {
            tileCrusor.SetTile(pos, cellTiles[7]);
        }
        for (int i = 0; i < 4; i++)
        {
            Vector3Int newPos = new Vector3Int(pos.x + dx[i], pos.y + dy[i], 0);
            if (IsPositionInMap(newPos))
            {
                tileCrusor.SetTile(newPos, cellTiles[6]);
            }
        }

    }

    public void ClearCursor()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tileCrusor.SetTile(new Vector3Int(x, y, 0), null);
            }
        }
    }


    public Cell GetCell(Vector3Int pos)
    {
        return map[pos.x, pos.y];
    }

    public int GetCellArmy(int x, int y)
    {
        return map[x, y].ArmySize;
    }

    public int GetCellOwner(int x, int y)
    {
        return map[x, y].Owner;
    }

    public void SetCellArmy(int x, int y, int armySize)
    {
        map[x, y].ArmySize = armySize;
    }

    public void SetCellIsSelecting(int x, int y, bool isSelecting)
    {
        map[x, y].IsSelecting = isSelecting;
    }

    public void SetCell(int x, int y, Cell cell)
    {
        map[x, y] = cell;
    }

    public void SetCellOwner(int x, int y, int owner)
    {
        map[x, y].Owner = owner;
    }


    public bool IsPositionInMap(Vector3Int pos)
    {
        return pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height;
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public Vector3Int GetCellPosWithWorldPos(Vector3 worldPos)
    {
        return tilemapCell.WorldToCell(worldPos);
    }

    public Vector3 GetWorldPosWithCellPos(Vector3Int cellPos)
    {
        return tilemapCell.GetCellCenterWorld(cellPos);
    }

}

