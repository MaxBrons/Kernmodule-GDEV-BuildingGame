using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BuildingGame.Inventory
{
    // A default interface for inventory UI classes
    public interface IInventoryUI
    {
        public void UpdateUI(IInventory inventory);
    }

    // This is the main inventory UI class that handles practically everything
    // for the inventory UI.
    public class InventoryUI : IInventoryUI
    {
        public event Action<string> OnItemSelected;
        public event Action<string> OnToolItemSelected;

        private List<IInventoryUIItem> _items = new();
        private List<IInventoryUIItem> _toolItems = new();
        private IInventoryUIItem _equipedItem;
        private UnityAction _onSlotButttonClickEventListener;
        private UnityAction _onToolSlotButttonClickEventListener;
        private InventoryData _data;

        // Initialize all variables and all the inventory UI slots on creation.
        public InventoryUI(IInventory inventory, IData data)
        {
            _data = (InventoryData)data;

            // Store all the different slots in a list for later use.
            _items = GetSlots(_data.InventorySlotTag);
            _toolItems = GetSlots(_data.InventoryToolSlotTag);
            _equipedItem = GetSlots(_data.InventoryEquipedSlotTag)[0];

            // Add the event listeners to the slot buttons.
            _onSlotButttonClickEventListener += OnSlotButttonClicked;
            _onToolSlotButttonClickEventListener += OnToolSlotButttonClicked;

            // WARNING: Yes this can break because I'm assuming that the owner of the item has a button component.
            _items.ForEach(item => item.Owner.GetComponent<Button>().onClick.AddListener(_onSlotButttonClickEventListener));
            _toolItems.ForEach(toolItem => toolItem.Owner.GetComponent<Button>().onClick.AddListener(_onToolSlotButttonClickEventListener));

            UpdateToolItems(inventory);
            UpdateEquipedItem(string.Empty, null, -1);
        }

        // Unbind from the button clicked events.
        ~InventoryUI()
        {
            _onSlotButttonClickEventListener -= OnSlotButttonClicked;
            _onToolSlotButttonClickEventListener -= OnToolSlotButttonClicked;
        }

        // Update the UI for the main inventory item slots
        public void UpdateUI(IInventory inventory)
        {
            int index = 0;
            var inventoryItems = inventory.Items.Values.ToList();

            foreach (var item in _items) {
                // Empty all the visuals for the slots that have no items in them.
                if (index >= inventoryItems.Count) {
                    _items[index].Name.text = string.Empty;
                    _items[index].Image.sprite = null;
                    _items[index].CountText.text = string.Empty;
                    index++;
                    continue;
                }

                // Set the name, sprite and count for the item slot.
                _items[index].Name.text = inventoryItems[index].Name;
                _items[index].Image.sprite = inventoryItems[index].Sprite;
                _items[index].CountText.text = inventoryItems[index].Count.ToString();
                index++;
            }
        }

        // Update the UI for the inventory item tool slots (blueprint slots)
        private void UpdateToolItems(IInventory inventory)
        {
            int index = 0;

            foreach (var item in _toolItems) {
                // Empty all the visuals for the slots that have no items in them.
                if (index >= _data.ToolItems.Count) {
                    _toolItems[index].Name.text = string.Empty;
                    _toolItems[index].Image.sprite = null;
                    _toolItems[index].CountText.text = string.Empty;
                    index++;
                    continue;
                }

                // Set the name, sprite and count for the item slot.
                _toolItems[index].Name.text = _data.ToolItems[index].Name;
                _toolItems[index].Image.sprite = _data.ToolItems[index].Sprite;
                _toolItems[index].CountText.text = _data.ToolItems[index].Count.ToString();
                index++;
            }
        }

        // Update the UI for the currently equiped item slot with an other Inventory UI Slot
        private void UpdateEquipedItem(IInventoryUIItem item)
        {
            _equipedItem.Name.text = item.Name.text;
            _equipedItem.Image.sprite = item.Image.sprite;
            _equipedItem.CountText.text = item.CountText.text;
        }

        // Update the UI for the currently equiped item slot with a name, sprite and count
        private void UpdateEquipedItem(string name, Sprite sprite, int count)
        {
            _equipedItem.Name.text = name;
            _equipedItem.Image.sprite = sprite;
            _equipedItem.CountText.text = count < 0 ? string.Empty : count.ToString();
        }

        // Get a list of Inventory UI Items that are tight to the UI components of the inventory UI slots
        // in the scene hierarchy and only retrieve those that are on a inventory UI slot with the given slot tag.

        private List<IInventoryUIItem> GetSlots(string slotTag)
        {
            // Get the inventory UI gameobject in the scene hierarchy and get all the children from it.
            Transform inventoryTransform = GameObject.FindGameObjectWithTag(_data.InventoryTag).transform;
            List<RectTransform> slots = inventoryTransform.GetComponentsInChildren<RectTransform>().ToList();

            // Filter the slots based on the given slot tag.
            slots = slots.FindAll(slot => slot.CompareTag(slotTag));

            List<IInventoryUIItem> uiItems = new List<IInventoryUIItem>();

            foreach (var item in slots) {
                // Get the UI components needed for displaying the name, icon and count of the inventory item.
                var comps = item.GetComponentsInChildren<RectTransform>().ToList();
                var name = comps.Find(item => item.CompareTag(_data.InventorySlotNameTag)).GetComponent<TextMeshProUGUI>();
                var image = comps.Find(item => item.CompareTag(_data.InventorySlotIconTag)).GetComponent<Image>();
                var countText = comps.Find(item => item.CompareTag(_data.InventorySlotCountTextTag)).GetComponent<TextMeshProUGUI>();

                // Create a new inventory UI item with the reference to the UI components and the parent transform as owner.
                uiItems.Add(new InventoryUIItem(name, image, countText, item));
            }

            return uiItems;
        }

        // Change the item that gets displayed in the currently equiped item slot when an item slot is selected.
        private void OnSlotButttonClicked()
        {
            // Get all the buttons from the items in the scene hierarchy.
            var buttons = _items.ConvertAll(item => item.Owner.GetComponent<Button>());

            // Get the button that is currently selected.
            Button currentClicked = buttons.FirstOrDefault(button => {
                return EventSystem.current != null && EventSystem.current.currentSelectedGameObject == button.gameObject;
            });

            if (currentClicked) {
                // Update the currently equiped item to display the newly selected item.
                UpdateEquipedItem(_items[buttons.IndexOf(currentClicked)]);
                OnItemSelected?.Invoke(_items[buttons.IndexOf(currentClicked)].Name.text);
            }
        }

        // Check if a tool slot is selected and invoke the corresponding event if the slot was selected.
        private void OnToolSlotButttonClicked()
        {
            // Get the button that is currently selected.
            var buttons = _toolItems.ConvertAll(item => item.Owner.GetComponent<Button>());
            Button currentClicked = buttons.FirstOrDefault(button => {
                return EventSystem.current != null && EventSystem.current.currentSelectedGameObject == button.gameObject;
            });

            if (currentClicked) {
                // Invoke the event and pass the currently selected tool item's name on to it.
                var toolItem = _toolItems.Find(item => item.Owner == currentClicked.transform);
                OnToolItemSelected?.Invoke(toolItem.Name.text);
            }
        }
    }
}