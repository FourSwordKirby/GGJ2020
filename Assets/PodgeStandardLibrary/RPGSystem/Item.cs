using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// This specifies the basic template for identifying an item. 
/// By default 2 items are equal if they share the same type, regardless of other factors (ie durability, modifiers, etc)
/// </summary>
public abstract class Item
{
    public string ItemName { get; }
    public string ItemDescription { get; }


    /// <summary>
    /// returns a string that distinctly identifies this item
    /// used for equality, by default 2 items are equal if they share the same type, regardless of other factors (ie durability, modifiers, etc)
    /// </summary>
    /// <returns></returns>
    public abstract string GetItemType();

    public override int GetHashCode()
    {
        return ItemDescription.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (!(obj is Item))
            return false;

        return Equals((Item)obj);
    }

    public bool Equals(Item other)
    {
        return GetItemType() == other.GetItemType();
    }

    public static bool operator ==(Item item1, Item item2)
    {
        return item1.Equals(item2);
    }

    public static bool operator !=(Item item1, Item item2)
    {
        return !item1.Equals(item2);
    }
}

public class ConsumableItem: Item
{
    public ConsumableItemType type;

    public override string GetItemType()
    {
        return Enum.GetName(typeof(ConsumableItemType), type);
    }
}

public class KeyItem: Item
{
    public KeyItemType type;

    public override string GetItemType()
    {
        return Enum.GetName(typeof(ConsumableItemType), type);
    }
}

public class EquipableItem : Item
{
    public ConsumableItemType type;

    public override string GetItemType()
    {
        return Enum.GetName(typeof(ConsumableItemType), type);
    }
}
