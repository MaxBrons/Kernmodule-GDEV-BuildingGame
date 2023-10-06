using System.Collections.Generic;

namespace BuildingGame.Core
{
    public abstract class BehaviourManager : IBehaviourManager
    {
        // A list of behaviours to be updated by this manager.
        protected List<IMonoBehaviour> _behaviours = new();

        protected abstract void Initialize();

        // Add a behaviour to the list of behaviours to be updated.
        public bool Add(IMonoBehaviour behaviour)
        {
            bool notInList = !_behaviours.Contains(behaviour);

            if (behaviour != null && notInList) {
                _behaviours.Add(behaviour);
                behaviour.SetEnabled(true);
                behaviour.OnAwake();
                behaviour.OnEnable();
                behaviour.OnStart();
                return true;
            }

            return false;
        }

        // Remove a behaviour from the list of behaviours to be updated.
        public bool Remove(IMonoBehaviour behaviour)
        {
            if (behaviour != null && _behaviours.Contains(behaviour)) {
                behaviour.SetEnabled(false);
                behaviour.OnDisable();
                behaviour.OnDestroy();
                return _behaviours.Remove(behaviour);
            }

            return false;
        }

        #region Behaviour Update Methods
        public void OnAwake()
        {
            _behaviours = new();
            
            // Initialize the derived variant of this manager.
            Initialize();
            _behaviours.ForEach(behaviour => behaviour.OnAwake());
        }

        public void OnStart()
        {
            _behaviours.ForEach(behaviour => behaviour.OnStart());
        }

        public void OnUpdate()
        {
            _behaviours.ForEach(behaviour => behaviour.OnUpdate());
        }

        public void OnFixedUpdate()
        {
            _behaviours.ForEach(behaviour => behaviour.OnFixedUpdate());
        }

        public void OnLateUpdate()
        {
            _behaviours.ForEach(behaviour => behaviour.OnLateUpdate());
        }

        // Destroy all behaviours in the list of behaviours
        // and than clear the list.
        public void OnDestroy()
        {
            _behaviours.ForEach(behaviour => {
                behaviour.SetEnabled(false);
                behaviour.OnDisable();
                behaviour.OnDestroy();
            });

            _behaviours = new();
        }
        #endregion
    }
}
