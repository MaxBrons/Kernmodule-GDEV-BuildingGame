using System;
using UnityEngine;

namespace BuildingGame.Inventory
{
    // A default interface for inventory items.
    public interface IInventoryItem
    {
        public string Name { get; }
        public Sprite Sprite { get; }
        public int Count { get; set; }
    }

    // A default version of an inventory item to be serialized
    // in the editor.
    [Serializable]
    public class InventoryItem : IInventoryItem
    {
        public string Name => _name;
        public Sprite Sprite => _sprite;
        public int Count { get => _count; set => _count = value; }

        [SerializeField] private string _name;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private int _count;

        public InventoryItem(string name, Sprite sprite, int count)
        {
            _name = name;
            _sprite = sprite;
            _count = count;
        }
    }
}
