using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerEvents : MonoBehaviour
{
    public static PlayerEvents instance;
    public PlayerEvents() => instance = this;

    public UnityEvent onReload;

    public UnityEvent<StandardEntity> onHitEnemy;

    public UnityEvent<StandardEntity> onLaunchEnemy;

    public UnityEvent<StandardEntity> onKillEnemy;

    public UnityEvent<StatusType> onInflictStatus;
}
