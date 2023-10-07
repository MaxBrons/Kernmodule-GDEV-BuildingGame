using System;
using System.Collections.Generic;

namespace BuildingGame.Inventory
{
    // The inventory interface for creating generic
    // inventory classes.
    public interface IInventory
    {
        // This is a pair used for serializing the 
        // item name and count in the editor.
        [Serializable]
        public class InventoryKeyValuePair
        {
            public string Name;
            public int Count;
        }

        public event Action<IInventoryItem> OnItemAdded;
        public event Action<IInventoryItem> OnItemRemoved;

        public Dictionary<string, IInventoryItem> Items { get; }

        public bool AddItem(IInventoryItem item);
        public bool RemoveItem(InventoryKeyValuePair item);
        public bool TryRemoveItems(List<InventoryKeyValuePair> item);
    }

    // A simple Inventory class for storing/removing inventory items
    // based on a string as key and an IInventoryItem as value
    public class Inventory : IInventory
    {
        public event Action<IInventoryItem> OnItemAdded;
        public event Action<IInventoryItem> OnItemRemoved;

        public Dictionary<string, IInventoryItem> Items => _items;

        private Dictionary<string, IInventoryItem> _items;

        public Inventory()
        {
            _items = new Dictionary<string, IInventoryItem>();
        }

        // Add an item if it does not yet exist in the inventory
        // or else increase the count of the item that is already
        // present in the inventory.
        public bool AddItem(IInventoryItem item)
        {
            if (item == null)
                return false;

            if (_items.ContainsKey(item.Name)) {
                // If the item already exists in the inventory, update the count
                _items[item.Name].Count += item.Count;
                OnItemAdded?.Invoke(_items[item.Name]);
                return true;
            }

            // Add the item to the inventory.
            _items[item.Name] = item;
            OnItemAdded?.Invoke(item);
            return true;
        }

        // Remove an amount of the item from the inventory if the count is
        // high enough or else remove the item from the inventory completely.
        public bool RemoveItem(IInventory.InventoryKeyValuePair item)
        {
            if (_items.ContainsKey(item.Name)) {
                // Check if there are enough items to remove
                if (_items[item.Name].Count >= item.Count) {
                    _items[item.Name].Count -= item.Count;

                    if (_items[item.Name].Count <= 0) {
                        // Remove the item from the inventory if count becomes zero
                        OnItemRemoved(_items[item.Name]);
                        _items.Remove(item.Name);
                        return true;
                    }

                    OnItemRemoved(_items[item.Name]);
                    return true;
                }
                return false;
            }
            return false;
        }

        // Try and remove a list of items and amount from the inventory. If one of the items
        // does not exist in the inventory or there are insufficient items present, the method
        // will return false. Otherwise all (amount of) items will be removed from the inventory.
        public bool TryRemoveItems(List<IInventory.InventoryKeyValuePair> itemsToRemove)
        {
            if (itemsToRemove.Count <= 0)
                return false;

            // Check if all the items in the list are present in the inventory.
            foreach (var item in itemsToRemove) {
                if (!_items.ContainsKey(item.Name))
                    return false;
            }

            // Check if there are enough items present of each item in the list.
            foreach (var item in itemsToRemove) {
                if (_items[item.Name].Count < item.Count)
                    return false;
            }

            // Remove the (amount of) items from the inventory.
            foreach (var item in itemsToRemove) {
                RemoveItem(item);
            }
            return true;
        }
    }
}
