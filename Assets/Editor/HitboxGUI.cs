using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Hitbox))]
public class HitboxGUI : Editor
{
    private void OnSceneGUI()
    {
        Hitbox h = (Hitbox)target;

        Handles.color = new Color(1,0,0,0.5f);

        Handles.DrawSolidArc(h.transform.position, -Vector3.forward, h.GetHalfDir(), h.angle, h.range);
    }
}
