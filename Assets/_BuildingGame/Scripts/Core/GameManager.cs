using BuildingGame.BuildingSystem;
using BuildingGame.Data;
using BuildingGame.Input;
using BuildingGame.Inventory;
using BuildingGame.Player;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingGame.Core
{
    // This is the only MonoBehaviour class in the game and this
    // manager will manage practically every object in the game.
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private List<BehaviourDataSet> _behaviourDataSets = new();
        private List<IBehaviourManager> _behaviourManagers;

        private void Awake()
        {
            // This adds all the scriptable object data sets
            // to the scratchpad (LostAndFound) for later
            // retrieval by the behaviours.
            foreach (var data in _behaviourDataSets) {
                LostAndFound.Add(data.ID, Instantiate(data.Data));
            }

            // Initialize the input and enable it.
            InputManager.Initialize();

            // Add all the behaviour managers in this list
            // to be updated during runtime.
            _behaviourManagers = new()
            {
                new DebugBehaviourManager(),
                new InventoryManager(),
                new BuildingBehaviourManager(),
                new PlayerBehaviourManager(),
            };

            InputHandler handler = new InputHandler(InputManager.InputActionAsset.Inventory.Toggle,
                                                    new() { _behaviourManagers[1] },
                                                    new() { _behaviourManagers[2], _behaviourManagers[3] }
                                                    );
            _behaviourManagers.ForEach(manager => manager.OnAwake());
        }

        private void Start()
        {
            _behaviourManagers.ForEach(manager => manager.OnStart());
        }

        private void Update()
        {
            _behaviourManagers.ForEach(manager => manager.OnUpdate());
        }

        private void FixedUpdate()
        {
            _behaviourManagers.ForEach(manager => manager.OnFixedUpdate());
        }

        private void LateUpdate()
        {
            _behaviourManagers.ForEach(manager => manager.OnLateUpdate());
        }

        private void OnDestroy()
        {
            _behaviourManagers.ForEach(manager => manager.OnDestroy());

            // This removes all the scriptable object data sets
            // from the scratchpad (LostAndFound).
            foreach (var data in _behaviourDataSets) {
                LostAndFound.Remove(data.ID);
            }

            // Deinitialize the input and disable it.
            InputManager.Deinitialize();
        }
    }
}
