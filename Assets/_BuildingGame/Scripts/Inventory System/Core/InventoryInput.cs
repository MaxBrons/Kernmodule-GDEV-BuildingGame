using BuildingGame.Input;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BuildingGame.Inventory
{
    // This is the class for handling the inventory UI input.
    public class InventoryInput
    {
        public event Action<bool> OnInventoryToggled;

        private IInventoryUI _inventoryUI;
        private bool _toggled = false;

        // Disable the UI and bind to the input for toggling the UI.
        public InventoryInput(IInventoryUI inventoryUI)
        {
            _inventoryUI = inventoryUI;
            if (_inventoryUI == null)
                return;

            SetInventoryUIEnabled(false);

            InputManager.AddListener(OnToggle);
            InputManager.SetInputEnabled(InputManager.InputActionAsset.Inventory.Toggle, true);
        }

        // Disable the UI and unbind to the input for toggling the UI.
        ~InventoryInput()
        {
            if (_inventoryUI == null)
                return;

            SetInventoryUIEnabled(false);

            InputManager.RemoveListener(OnToggle);
        }

        public void OnToggle(InputAction.CallbackContext context)
        {
            _toggled = !_toggled;
            SetInventoryUIEnabled(_toggled);
            OnInventoryToggled?.Invoke(_toggled);
        }

        // Enable/disable the UI and the cursor.
        private void SetInventoryUIEnabled(bool enable)
        {
            Cursor.lockState = enable ? CursorLockMode.Confined : CursorLockMode.Locked;
            Cursor.visible = enable;
            _inventoryUI.SetEnabled(enable);
        }
    }
}