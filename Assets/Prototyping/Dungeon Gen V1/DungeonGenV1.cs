using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

public class DungeonGenV1 : MonoBehaviour
{
    public GameObject TilePref;
    public float size = 1;

    public List<Vector3> searchPoints = new List<Vector3>();
    public List<Vector3> tilePoints = new List<Vector3>();

    void Start()
    {
        ClearTiles();
    }
    public void GenerateCycle()
    {
        Vector3 randomPos = searchPoints[Random.Range(0, searchPoints.Count)];
        Debug.Log(randomPos);
        Vector3[] newPoints = { randomPos + Vector3.up * size,
                                randomPos + Vector3.right * size,
                                randomPos - Vector3.up * size,
                                randomPos - Vector3.right * size};

        foreach (var p in newPoints)
            if (!searchPoints.Contains(p) && !tilePoints.Contains(p))
                searchPoints.Add(p);

        searchPoints.Remove(randomPos);
        tilePoints.Add(randomPos);
    }
    public void ClearTiles()
    {
        tilePoints.Clear();
        searchPoints.Clear();
        searchPoints.Add(Vector3.zero);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var p in searchPoints)
            Gizmos.DrawSphere(p, size/2);

        Gizmos.color = Color.green;
        foreach (var p in tilePoints)
            Gizmos.DrawSphere(p, size/2);
    }
}

[CustomEditor(typeof(DungeonGenV1))]
public class DungeonGenV1GUI : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DungeonGenV1 g = (DungeonGenV1)target;

        if (GUILayout.Button("Generate Cycle"))
            g.GenerateCycle();
        if (GUILayout.Button("Clear Tiles"))
            g.ClearTiles();
    }
}