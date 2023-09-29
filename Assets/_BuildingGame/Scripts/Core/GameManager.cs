using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BG.Core
{
    // This is the only MonoBehaviour class in the game and this
    // manager will manage practically every object in the game.
    public class GameManager : MonoBehaviour
    {
        private IBehaviour _behaviourManager;

        private void Awake()
        {
            _behaviourManager = new BehaviourManager();
            _behaviourManager.OnAwake();
        }

        private void Start()
        {
            _behaviourManager.OnStart();
        }

        private void Update()
        {
            _behaviourManager.OnUpdate();
        }

        private void FixedUpdate()
        {
            _behaviourManager.OnFixedUpdate();
        }

        private void LateUpdate()
        {
            _behaviourManager.OnLateUpdate();
        }
    }
}
