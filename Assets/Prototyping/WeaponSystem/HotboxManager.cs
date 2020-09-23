using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotboxManager : MonoBehaviour
{
    Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    public void Enable()
    {
        col.enabled = true;
    }
    public void Disable()
    { 
        col.enabled = true;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        Entity e = other.GetComponent<Entity>();
        Debug.Log(e);
        if (e)
            e.Hit(Vector3.up * 100, 3);
    }
}
