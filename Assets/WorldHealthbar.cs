using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZenUtil;

public class WorldHealthbar : MonoBehaviour
{
    public StandardEntity entity;
    public Timer hideTimer;
    public GameObject uiObj;
    public Slider hpBar;

    private void Start()
    {
        if (!entity)
            Debug.LogWarning("There is no entity to track HP");
        else
        {
            entity.onDamage += hideTimer.Restart;
        }
    }

    void Update()
    {
        if (entity)
        {
            //hideTimer.Update(Time.deltaTime);
            uiObj.SetActive(!hideTimer.finished);
            hpBar.value = entity.healthPercent;
        }
    }
}
