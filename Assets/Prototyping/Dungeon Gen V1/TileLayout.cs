using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TileLayout : MonoBehaviour
{
    public TileType type;
    public Vector3 specialPos;
    public Vector3[] enemyPos;
    public Vector3[] lootPos;

    public void SetupTiles()
    {

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        if (type != TileType.none)
                Gizmos.DrawSphere(transform.position + specialPos, 0.5f);

        Gizmos.color = Color.red;
        if (enemyPos.Length > 0)
            foreach (var pos in enemyPos)
                Gizmos.DrawSphere(transform.position + pos, 0.5f);

        Gizmos.color = Color.green;
        if (enemyPos.Length > 0)
            foreach (var pos in lootPos)
                Gizmos.DrawSphere(transform.position + pos, 0.5f);
    }
}
