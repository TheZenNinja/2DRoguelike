using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class DungeonTile : MonoBehaviour
{
    public TileType type;
    public float size;
    public Vector2 gridPos;
    public int doorSize;
    public bool generated = false;
    public TileBase wallTile, floorTile, platformTile;
    public Tilemap wallMap;
    public List<TileLayout> layouts;
    public void SetPosition(Vector2 gridPos)
    {
        this.gridPos = gridPos;
        transform.localPosition = gridPos * size;
    }
    [ContextMenu("Check Walls")]

    public void GenerateRoom(List<Vector2> tilePos, TileType type = TileType.none)
    {
        CreateWalls(tilePos);
        GenerateInterior(type);
        this.type = type;
        wallMap.GetComponent<CompositeCollider2D>().GenerateGeometry();
        generated = true;
    }

    public void CreateWalls(List<Vector2> tilePos)
    {
        int halfSize = Mathf.RoundToInt(size / 2);
        // ensures a platform
        TileBase t = !tilePos.Contains(gridPos + Vector2.up) ? wallTile : platformTile;
        for (int i = -doorSize/2; i < doorSize/2; i++)
                wallMap.SetTile(new Vector3Int(i, halfSize - 1, 0), wallTile);

        if (!tilePos.Contains(gridPos - Vector2.up))
            for (int i = -doorSize / 2; i < doorSize / 2; i++)
                wallMap.SetTile(new Vector3Int(i, -halfSize, 0), wallTile);


        if (!tilePos.Contains(gridPos + Vector2.right))
            for (int i = -doorSize / 2; i < doorSize / 2; i++)
                wallMap.SetTile(new Vector3Int(halfSize -1, i, 0), wallTile);

        if (!tilePos.Contains(gridPos - Vector2.right))
            for (int i = -doorSize / 2; i < doorSize / 2; i++)
                wallMap.SetTile(new Vector3Int(-halfSize, i, 0), wallTile);
    }
    
    public void GenerateInterior(TileType type)
    {
        TileLayout tile;

        switch (type)
        {
            default:
            case TileType.none:
                do
                {
                    tile = layouts[Random.Range(0, layouts.Count)];
                } while (tile.type != TileType.none);
                break;
            case TileType.spawn:
            tile = layouts.Find(x => x.type == TileType.spawn);
                break;
            case TileType.exit:
            tile = layouts.Find(x => x.type == TileType.exit);
                break;
        }

        tile.gameObject.SetActive(true);
        tile.SetupTiles();
    }
    public TileLayout GetActiveLayout()
    {
        foreach (var t in layouts)
            if (t.gameObject.activeSelf)
                return t;
        return null;
    }
    public Vector3 GetSpecialPos()
    {
        if (type == TileType.none)
            throw new System.Exception("Not a special tile");
        return transform.position + GetActiveLayout().specialPos;
    }
}
