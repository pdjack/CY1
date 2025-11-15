using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Item Database")]
public class ItemDbObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] items;
    public Dictionary<ItemObject, int> GetId = new Dictionary <ItemObject, int>();
    public Dictionary<int, ItemObject> GetItem = new Dictionary <int, ItemObject>();

    public void OnAfterDeserialize()
    {
        GetId.Clear();
        GetItem.Clear();
        for (int i = 0; i < items.Length; i++)
        {
            GetId.Add(items[i], i);
            GetItem.Add(i, items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        
    }
}
