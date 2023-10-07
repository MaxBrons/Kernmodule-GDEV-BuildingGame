
using BuildingGame.Core;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace BuildingGame.Input
{
    public class InputHandler
    {
        private InputAction _triggerAction;
        private List<IBehaviourManager> _managersToEnable;
        private List<IBehaviourManager> _managersToDisable;
        private bool _toggled = false;

        public InputHandler(InputAction action, List<IBehaviourManager> managersToEnable, List<IBehaviourManager> managersToDisable)
        {
            _managersToEnable = managersToEnable;
            _managersToDisable = managersToDisable;
            _triggerAction = action;
            _triggerAction.performed += OnActionTriggered;
        }

        private void OnActionTriggered(InputAction.CallbackContext context)
        {
            bool triggered = context.ReadValueAsButton();
            if (!triggered)
                return;

            _toggled = !_toggled;

            _managersToEnable.ForEach(manager => manager.SetEnabled(_toggled));
            _managersToDisable.ForEach(manager => manager.SetEnabled(!_toggled));
        }

        ~InputHandler()
        {
            _triggerAction.performed -= OnActionTriggered;
        }
    }
}