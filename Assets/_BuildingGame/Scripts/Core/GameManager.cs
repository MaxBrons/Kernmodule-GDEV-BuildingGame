using System.Collections.Generic;
using UnityEngine;

namespace BuildingGame.Core
{
    // This is the only MonoBehaviour class in the game and this
    // manager will manage practically every object in the game.
    public class GameManager : MonoBehaviour
    {
        private List<IBehaviourManager> _behaviourManagers;

        private void Awake()
        {
            // Add all the behaviour managers in this list
            // to be updated during runtime.
            _behaviourManagers = new()
            {
                new DebugBehaviourManager(),
                new BuildingSystem.BuildingBehaviourManager(),
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
