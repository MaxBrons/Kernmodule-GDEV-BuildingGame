using BuildingGame.Data;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingGame.Inventory
{
    // This data class contains all the needed data that the inventory system needs
    // to add/remove items from the inventory and create blueprint items via the
    // tool items section.
    [CreateAssetMenu(fileName = "Inventory Data", menuName = "Custom/Data/Inventory Data", order = 0)]
    public class InventoryData : BehaviourData
    {
        public List<InventoryItem> Items = new();
        public List<InventoryToolItem> ToolItems = new();
        public string InventoryTag = "Inventory";
        public string InventorySlotTag = "Slot";
        public string InventoryToolSlotTag = "Tool Slot";
        public string InventoryEquipedSlotTag = "Equped Slot";
        public string InventorySlotNameTag = "Slot Name";
        public string InventorySlotIconTag = "Slot Icon";
        public string InventorySlotCountTextTag = "Slot Count";
    }
}