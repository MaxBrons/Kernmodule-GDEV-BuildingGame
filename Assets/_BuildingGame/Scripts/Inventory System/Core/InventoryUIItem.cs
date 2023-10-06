using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BuildingGame.Inventory
{
    // A default interface for a inventory UI item
    public interface IInventoryUIItem
    {
        public TextMeshProUGUI Name { get; }
        public Image Image { get; }
        public TextMeshProUGUI CountText { get; }

        // Also store the owner for an easier reference later
        // to the parent object of the inventory UI item.
        public RectTransform Owner { get; }
    }

    // A default version of an inventory UI item to visualize
    // the inventory's data on screen to the user.
    public class InventoryUIItem : IInventoryUIItem
    {
        public TextMeshProUGUI Name { get; }
        public Image Image { get; }
        public TextMeshProUGUI CountText { get; }

        public RectTransform Owner { get; }

        public InventoryUIItem(TextMeshProUGUI name, Image image, TextMeshProUGUI countText, RectTransform owner)
        {
            Name = name;
            Image = image;
            CountText = countText;
            Owner = owner;
        }
    }
}
