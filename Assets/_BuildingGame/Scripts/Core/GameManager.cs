using BuildingGame.Data;
using BuildingGame.Inventory;
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
                LostAndFound.Add(data.ID, data.Data);
            }

            // Add all the behaviour managers in this list
            // to be updated during runtime.
            _behaviourManagers = new()
            {
                new DebugBehaviourManager(),
                new InventoryManager(),
            };

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
        }
    }
}
