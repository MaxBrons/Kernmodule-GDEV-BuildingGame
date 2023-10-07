using System;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingGame.Inventory
{
    // This is an inventory item with an extra list of required items. This class is
    // used in the InventoryData scriptable object as a blueprint item that can create
    // a new item based on a list of required items, a.k.a. a blueprint.
    [Serializable]
    public class InventoryToolItem : InventoryItem
    {
        public List<IInventory.InventoryKeyValuePair> RequiredItems => _requiredItems;

        [SerializeField] private List<IInventory.InventoryKeyValuePair> _requiredItems = new();

        public InventoryToolItem(string name, Sprite sprite, int count, List<IInventory.InventoryKeyValuePair> requiredItems) : base(name, sprite, count)
        {
            _requiredItems = requiredItems;
        }
    }
}
