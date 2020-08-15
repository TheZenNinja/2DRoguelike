using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Items/Basic")]
public class StaticItem : Item
{
    public string itemName;
    public Sprite sprite;
    public int ID;
    [TextArea(3, 100)]
    public string description = "";

    public override string GetName() => itemName;
    public override Sprite GetSprite() => sprite;
    public override string GetDescription() => description;

    public override bool SameItem(Item other)
    {
        return base.SameItem(other) && this.ID == ((StaticItem)other).ID;
    }
    public override Item Duplicate()
    {
        return this;
    }

    
}
