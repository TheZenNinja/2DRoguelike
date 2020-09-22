using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

public class DungeonGenV1 : MonoBehaviour
{
    public GameObject TilePref;
    public float size = 1;
    public float scale = 1;
    public int cycles = 20;
    public List<Vector2> searchPoints = new List<Vector2>();
    public List<Vector2> tilePoints = new List<Vector2>();
    private List<DungeonTile> tiles = new List<DungeonTile>();

    [Space]
    public Vector3 spawnPos;

    [Range(0,1)]
    public float enemySpawnChance = 0.5f;
    [Range(0,1)]
    public float lootSpawnChance = 0.5f;

    public List<GameObject> spawnedEnemy;
    public List<GameObject> spawnedLoot;

    public GameObject enemy;
    public GameObject loot;

    void Start()
    {
        GenerateAllCycles();
    }
    public void GenerateAllCycles()
    {
        ClearTiles();
        for (int i = 0; i < cycles; i++)
            GenerateCycle();

        SpawnTiles();

        transform.localScale = Vector3.one * scale;
        spawnPos = tiles.Find(x => x.type == TileType.spawn).GetSpecialPos();
    }
    public void GenerateCycle()
    {
        Vector2 randomPos = searchPoints[Random.Range(0, searchPoints.Count)];

        Vector2[] newPoints = { randomPos + Vector2.up,
                                randomPos + Vector2.right,
                                randomPos - Vector2.up,
                                randomPos - Vector2.right};

        foreach (var p in newPoints)
            if (!searchPoints.Contains(p) && !tilePoints.Contains(p))
                searchPoints.Add(p);

        searchPoints.Remove(randomPos);
        tilePoints.Add(randomPos);
    }
    public void SpawnTiles()
    {
        foreach (var p in tilePoints)
        {
            DungeonTile d = Instantiate(TilePref, transform).GetComponent<DungeonTile>();
            d.SetPosition(p);
            tiles.Add(d);
        }

        DungeonTile tile;

        do
        {
            tile = tiles[Random.Range(0, tiles.Count)];
        } while (tile.generated);
        tile.GenerateRoom(tilePoints, TileType.spawn);

        do
        {
            tile = tiles[Random.Range(0, tiles.Count)];
        } while (tile.generated);
        tile.GenerateRoom(tilePoints, TileType.exit);

        foreach (var t in tiles)
            if (!t.generated)
                t.GenerateRoom(tilePoints);

    }
    public void ClearTiles()
    {
        if (tiles.Count > 0)
        foreach (var t in tiles)
        {
            if (Application.isEditor)
                DestroyImmediate(t.gameObject);
            else
                Destroy(t.gameObject);
        }
        tiles.Clear();
        tilePoints.Clear();
        searchPoints.Clear();


        foreach (var e in spawnedEnemy)
        {
            if (Application.isEditor)
                DestroyImmediate(e.gameObject);
            else
                Destroy(e.gameObject);
        }
        spawnedEnemy.Clear();

        foreach (var l in spawnedLoot)
        {
            if (Application.isEditor)
                DestroyImmediate(l.gameObject);
            else
                Destroy(l.gameObject);
        }
        spawnedLoot.Clear();
        transform.localScale = Vector3.one;
        searchPoints.Add(Vector3.zero);
    }
    /*public void SpawnEnemy(Vector3 pos)
    {
        if (Random.value <= enemySpawnChance)
            spawnedEnemy.Add(Instantiate(enemy, pos * scale, Quaternion.identity));
    }

    public void SpawnLoot(Vector3 pos)
    {
        if (Random.value <= lootSpawnChance)
            spawnedLoot.Add(Instantiate(loot, pos * scale, Quaternion.identity));
    }*/

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach (var p in searchPoints)
            Gizmos.DrawSphere(p * size, size/2);

        Gizmos.color = Color.green;
        foreach (var p in tilePoints)
            Gizmos.DrawSphere(p * size, size/2);
    }*/
}

[CustomEditor(typeof(DungeonGenV1))]
public class DungeonGenV1GUI : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DungeonGenV1 g = (DungeonGenV1)target;

        if (GUILayout.Button("Generate All Cycles"))
                g.GenerateAllCycles();
        if (GUILayout.Button("Generate Cycle"))
            g.GenerateCycle();
        if (GUILayout.Button("Clear Tiles"))
            g.ClearTiles();
    }
}