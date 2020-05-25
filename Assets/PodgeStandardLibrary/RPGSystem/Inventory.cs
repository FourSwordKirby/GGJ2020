using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Inventory
{
    Dictionary<Item, int> ItemDatabase = new Dictionary<Item, int>();

    /// <summary>
    /// Returns the count for an item is available, otherwise returns -1
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int GetItemCount(Item item)
    {
        return ItemDatabase.Keys.Contains(item) ? ItemDatabase[item] : -1;
    }
}
