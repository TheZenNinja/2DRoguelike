using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : ScriptableObject
{
    public abstract string GetName();

    public abstract Sprite GetSprite();

    public abstract string GetDescription();

    public virtual bool SameItem(Item other) => this.GetType() == other.GetType();

    public virtual Item Duplicate()
    {
        throw new System.NotImplementedException();
    }
}
