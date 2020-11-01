using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerEvents : MonoBehaviour
{
    public static PlayerEvents instance;
    public PlayerEvents() => instance = this;

    public UnityEvent onReload;

    public UnityEvent<Entity> onHitEnemy;

    public UnityEvent<Entity> onLaunchEnemy;

    public UnityEvent<Entity> onKillEnemy;

    public UnityEvent<StatusType> onInflictStatus;
}
