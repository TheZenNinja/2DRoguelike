using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(GunScript))]
public class GunScriptGUI : Editor
{
    /*private void OnSceneGUI()
    {
        GunScript g = (GunScript)target;

        Handles.color = new Color(1,0,0,0.5f);

        float newAngle = g.transform.eulerAngles.z + g.accuracyAngle/2;

        Vector3 dir = new Vector3(Mathf.Cos(newAngle * Mathf.Deg2Rad), Mathf.Sin(newAngle * Mathf.Deg2Rad));

        Handles.DrawSolidArc(g.GetBarrelPos(), -Vector3.forward, dir, g.accuracyAngle, Mathf.Clamp(g.speed/10+1,1,10));
    }*/
}
