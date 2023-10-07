using BuildingGame.Core;
using BuildingGame.Data;

namespace BuildingGame.Inventory
{
    // This manager creates and controlls the Inventory and InventoryUI classes.
    // When an item gets added to the inventory, the inventory UI will be updated.
    // At the beginning of the game, the list of items from the InventoryData scriptable
    // object gets added to the inventory.
    public class InventoryManager : BehaviourManager
    {
        private InventoryHandle _inventoryHandle;
        private InventoryInput _inventoryInput;
        private IData _data;
        private string _currentSelectedItem;

        private const string INVENTORY_DATA = "Inventory Data";
        private const string INVENTORY_NAME = "Inventory";

        // Unbind from all the UI selection events
        ~InventoryManager()
        {
            InventoryUI inventoryUI = (InventoryUI)_inventoryHandle.InventoryUI;
            inventoryUI.OnItemSelected -= InventoryUIOnItemSelected;
            inventoryUI.OnToolItemSelected -= InventoryUIOnToolItemSelected;
        }

        // Retrieve the InventoryData scriptable object, create the inventory
        // and the inventory UI classes, update the UI and bind to the events.
        protected override void Initialize()
        {
            // Retrieve the InventoryData from the scratchpad.
            _data = LostAndFound.Retrieve<InventoryData>(INVENTORY_DATA);
            var data = (InventoryData)_data;

            if (data == null)
                return;

            // Create the inventory handle (that handles updating the ui when the inventory gets updated)
            // and add the default items to the inventory.
            var inventory = new Inventory();
            var inventoryUI = new InventoryUI(inventory, data);
            _inventoryHandle = new InventoryHandle(inventory, inventoryUI);
            data.Items.ForEach(item => _inventoryHandle.Inventory.AddItem(item));

            LostAndFound.Add(INVENTORY_NAME, _inventoryHandle.Inventory);

            // Create and setup the inventory input
            _inventoryInput = new InventoryInput(inventoryUI);

            // These events are for when an inventory UI item is selected.
            inventoryUI.OnItemSelected += InventoryUIOnItemSelected;
            inventoryUI.OnToolItemSelected += InventoryUIOnToolItemSelected;
        }

        // Add a new blueprint item to the inventory when the blueprint item's slot is pressed.
        // If there are not enough recources or the removing has failed, abort.
        private void InventoryUIOnToolItemSelected(string name)
        {
            // Cast the data and find the pressed blueprint item.
            var data = (InventoryData)_data;
            var toolItem = data.ToolItems.Find(item => item.Name == name);

            if (toolItem == null)
                return;

            // Try and remove all the needed items from the inventory
            if (_inventoryHandle.Inventory.TryRemoveItems(toolItem.RequiredItems)) {

                // Add the newly created blueprint item to the inventory
                bool success = _inventoryHandle.Inventory.AddItem(new InventoryItem(toolItem.Name, toolItem.Sprite, 1));
                if (!success) {
                    // If the adding somehow did not work, re-add all the items back to the inventory.
                    foreach (var requiredItem in toolItem.RequiredItems) {
                        var newItem = data.Items.Find(item => item.Name == requiredItem.Name);
                        newItem.Count = requiredItem.Count;
                        _inventoryHandle.Inventory.AddItem(newItem);
                    }
                }
            }
        }

        private void InventoryUIOnItemSelected(string name)
        {
            // The current selected item had to be stored to
            // remove this item when the remove button is pressed.
            _currentSelectedItem = name;
        }
    }
}