using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Transform player;
    Entity playerEntity;
    public Slider hpBar;
    // Start is called before the first frame update
    void Start()
    {
        playerEntity = player.GetComponent<Entity>();
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = playerEntity.healthPercent;
    }
}
