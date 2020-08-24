using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class DungeonTile : MonoBehaviour
{
    public float size;
    public Vector2 gridPos;
    public TileBase wallTile, floorTile;
    public Tilemap wallMap;
    public TileLayout[] layouts;
    public TileLayout spawn;
    public void SetPosition(Vector2 gridPos)
    {
        this.gridPos = gridPos;
        transform.localPosition = gridPos * size;
    }
    [ContextMenu("Check Walls")]

    public void GenerateRoom(List<Vector2> tilePos, bool spawnRoom = false)
    {
        CreateWalls(tilePos);
        GenerateInterior(spawnRoom);

        wallMap.GetComponent<CompositeCollider2D>().GenerateGeometry();
    }

    public void CreateWalls(List<Vector2> tilePos)
    {
        if (!tilePos.Contains(gridPos + Vector2.up))
            wallMap.SetTiles(new Vector3Int[] { new Vector3Int(-2, 7, 0), new Vector3Int(-1, 7, 0), new Vector3Int(0, 7, 0), new Vector3Int(1, 7, 0) },
                             new TileBase[] { wallTile, wallTile, wallTile, wallTile });
        if (!tilePos.Contains(gridPos - Vector2.up))
                wallMap.SetTiles(new Vector3Int[] { new Vector3Int(-2, -8, 0), new Vector3Int(-1, -8, 0), new Vector3Int(0, -8, 0), new Vector3Int(1, -8, 0) },
                                 new TileBase[] { wallTile, wallTile, wallTile, wallTile });

        if (!tilePos.Contains(gridPos + Vector2.right))
            wallMap.SetTiles(new Vector3Int[] { new Vector3Int(7, 1, 0), new Vector3Int(7, 0, 0), new Vector3Int(7, -1, 0), new Vector3Int(7, -2, 0) },
                             new TileBase[] { wallTile, wallTile, wallTile, wallTile });
        if (!tilePos.Contains(gridPos - Vector2.right))
            wallMap.SetTiles(new Vector3Int[] { new Vector3Int(-8, 1, 0), new Vector3Int(-8, 0, 0), new Vector3Int(-8, -1, 0), new Vector3Int(-8, -2, 0) },
                             new TileBase[] { wallTile, wallTile, wallTile, wallTile });
    }

    public void GenerateInterior(bool spawnRoom)
    {
        TileLayout tile;
        if (!spawnRoom)
            tile = layouts[Random.Range(0, layouts.Length)];
        else
            tile = spawn;

        tile.gameObject.SetActive(true);
        tile.SetupTiles();
    }
}
