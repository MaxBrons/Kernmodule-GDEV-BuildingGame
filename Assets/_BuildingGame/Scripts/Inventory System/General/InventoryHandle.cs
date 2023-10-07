namespace BuildingGame.Inventory
{
    // This class handles the updating of the inventory ui when
    // an item gets added or removed from the inventory.
    public class InventoryHandle
    {
        public IInventory Inventory => _inventory;
        public IInventoryUI InventoryUI => _inventoryUI;

        private IInventory _inventory;
        private IInventoryUI _inventoryUI;

        public InventoryHandle(IInventory inventory, IInventoryUI inventoryUI)
        {
            _inventory = inventory;
            _inventoryUI = inventoryUI;

            _inventory.OnItemAdded += UpdateUI;
            _inventory.OnItemRemoved += UpdateUI;
        }

        ~InventoryHandle()
        {
            _inventory.OnItemAdded -= UpdateUI;
            _inventory.OnItemRemoved -= UpdateUI;
        }

        private void UpdateUI(IInventoryItem obj)
        {
            _inventoryUI.UpdateUI(_inventory);
        }
    }
}