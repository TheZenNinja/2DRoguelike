using UnityEngine;
using System;

[RequireComponent(typeof(PlatformEffector2D))]
public class Platform : MonoBehaviour
{
    public static Action enableDropThru, disableDropThru;
    private PlatformEffector2D effector;

    private void Awake()
    {
        effector = GetComponent<PlatformEffector2D>();
        enableDropThru  += EnableDrop ;
        disableDropThru += DisableDrop;
        DisableDrop();
    }

    private void OnDestroy()
    {
        enableDropThru  -= EnableDrop;
        disableDropThru -= DisableDrop;
        DisableDrop();
    }
    private void EnableDrop() => effector.rotationalOffset = 180;
    private void DisableDrop() => effector.rotationalOffset = 0;
}
