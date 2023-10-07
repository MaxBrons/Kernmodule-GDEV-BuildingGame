using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BuildingGame.Input
{
    // This is the static Input Manager that can be used to bind to
    // all the different inputs that the player can receive.
    public static class InputManager
    {
        public static InputActionsCore InputActionAsset => s_inputActionAsset;

        private static InputActionsCore s_inputActionAsset;
        private static List<InputAction> s_inputActions = new();

        // Add all the possible bindings to a list for later retrieval.
        public static void Initialize()
        {
            s_inputActionAsset = new InputActionsCore();
            s_inputActions = new();

            foreach (var actionMap in s_inputActionAsset.asset.actionMaps.ToArray()) {
                foreach (var binding in actionMap.actions) {
                    s_inputActions.Add(binding);
                }
            }
            s_inputActionAsset.Enable();
        }

        // Free the static memory.
        public static void Deinitialize()
        {
            s_inputActionAsset = null;
            s_inputActions = null;
        }

        // Bind the given methods to the input actions with the same name.
        public static void AddListener(params Action<InputAction.CallbackContext>[] inputActionEvents)
        {
            if (inputActionEvents.Length == 0)
                return;

            foreach (var action in inputActionEvents) {
                if (action != null) {

                    InputAction inputAction = GetInputAction(action.Method.Name);
                    if (inputAction != null) {
                        inputAction.started += action;
                        inputAction.performed += action;
                        inputAction.canceled += action;
                        continue;
                    }

                    Debug.LogError("Could not subscribe to input action with action: " + action.Method.Name);
                }
            }
        }

        // Unbind the given methods from the input actions with the same name.
        public static void RemoveListener(params Action<InputAction.CallbackContext>[] inputActionEvents)
        {
            if (inputActionEvents.Length == 0)
                return;

            foreach (var action in inputActionEvents) {
                if (action != null) {

                    InputAction inputAction = GetInputAction(action.Method.Name);
                    if (inputAction != null) {
                        inputAction.started -= action;
                        inputAction.performed -= action;
                        inputAction.canceled -= action;
                        continue;
                    }

                    Debug.LogError("Could not unsubscribe to input action with action: " + action.Method.Name);
                }
            }
        }

        // Enable or disable a specific input action.
        public static void SetInputEnabled(InputAction action, bool enabled)
        {
            if (action == null)
                return;

            var actionToEnable = s_inputActionAsset.FindAction(action.name);
            if (actionToEnable == null)
                return;

            if (enabled) {
                actionToEnable.Enable();
                return;
            }
            actionToEnable.Disable();
        }

        // Read the value of the action with the given action name.
        public static T ReadActionValue<T>(string inputActionName)
        {
            var action = GetInputAction(inputActionName);
            if (action != null) {
                return (T)action?.ReadValueAsObject();
            }
            return default;
        }

        // Get the input action with the given name.
        private static InputAction GetInputAction(string actionName)
        {
            var inputAction = s_inputActions.Find(action => {
                var name = MakeTypeName(action.name);
                return "On" + name == actionName;
            });

            if (inputAction != null) {
                return inputAction;
            }

            Debug.LogError("No InputAction found to subscribe to");
            return null;
        }

        #region Create typename methods
        /// <summary>
        /// Copied from Unity's <see cref="CSharpCodeHelpers"/> class
        /// </summary>
        private static string MakeIdentifier(string name, string suffix = "")
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (char.IsDigit(name[0]))
                name = "_" + name;

            // See if we have invalid characters in the name.
            var nameHasInvalidCharacters = false;
            for (var i = 0; i < name.Length; ++i) {
                var ch = name[i];
                if (!char.IsLetterOrDigit(ch) && ch != '_') {
                    nameHasInvalidCharacters = true;
                    break;
                }
            }

            // If so, create a new string where we remove them.
            if (nameHasInvalidCharacters) {
                var buffer = new StringBuilder();
                for (var i = 0; i < name.Length; ++i) {
                    var ch = name[i];
                    if (char.IsLetterOrDigit(ch) || ch == '_')
                        buffer.Append(ch);
                }

                name = buffer.ToString();
            }

            return name + suffix;
        }

        /// <summary>
        /// Copied from Unity's <see cref="CSharpCodeHelpers"/> class
        /// </summary>
        private static string MakeTypeName(string name, string suffix = "")
        {
            var symbolName = MakeIdentifier(name, suffix);
            if (char.IsLower(symbolName[0]))
                symbolName = char.ToUpper(symbolName[0]) + symbolName.Substring(1);
            return symbolName;
        }
        #endregion
    }
}
