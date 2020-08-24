using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TileLayout : MonoBehaviour
{
    public bool spawn;
    public Vector3 spawnPos;
    public Vector3[] enemyPos;
    public Vector3[] lootPos;

    public void SetupTiles()
    {
        DungeonGenV1 gen = GetComponentInParent<DungeonGenV1>();
        if (spawn)
            gen.spawnPos = spawnPos + transform.position;

        foreach (var pos in enemyPos)
            gen.SpawnEnemy(pos + transform.position);

        foreach (var pos in lootPos)
            gen.SpawnLoot(pos + transform.position);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        if (spawn)
            Gizmos.DrawSphere(transform.position + spawnPos, 0.5f);

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
