using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour
{
    public static CursorControl instance;
    public CursorControl() => instance = this;

    public bool lookAtCursor;
    public Transform playerHandDir;

    private void Start()
    {
        lookAtCursor = true;
    }
    public Vector2 GetDirFromHand()
    {
        return GetMouseDir(playerHandDir.position);
    }
    public float GetAngleFromHand()
    {
        return GetMouseAngle(playerHandDir.position);
    }

    public void Update()
    {
        if (lookAtCursor)
            playerHandDir.eulerAngles = new Vector3(0,0, GetAngleFromHand());
        else
            //playerHandDir.localEulerAngles =  Vector3.zero;
            playerHandDir.localEulerAngles = PlayerControl.instance.isFlipped ? Vector3.forward * 180 : Vector3.zero;
    }
    public void SetLookAtCursor(bool value)
    {
        lookAtCursor = value;
    }

    public static Vector2 GetMouseDir(Vector3 pos)
    {
        return (Camera.main.ScreenToWorldPoint(Input.mousePosition) - pos).normalized;
    }
    public static float GetMouseAngle(Vector3 pos)
    {
        Vector3 mousePos = GetMouseDir(pos);
        return Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
    }

}
