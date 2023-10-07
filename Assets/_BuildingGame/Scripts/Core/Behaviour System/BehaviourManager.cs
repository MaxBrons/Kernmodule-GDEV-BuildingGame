using System.Collections.Generic;

namespace BuildingGame.Core
{
    public abstract class BehaviourManager : IBehaviourManager
    {
        public bool Enabled => _enabled;
        private bool _enabled = true;

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
            if (!_enabled)
                return;

            _behaviours.ForEach(behaviour => {
                if (behaviour.Enabled)
                    behaviour.OnUpdate();
            });
        }

        public void OnFixedUpdate()
        {
            if (!_enabled)
                return;

            _behaviours.ForEach(behaviour => {
                if (behaviour.Enabled)
                    behaviour.OnFixedUpdate();
            });
        }

        public void OnLateUpdate()
        {
            if (!_enabled)
                return;

            _behaviours.ForEach(behaviour => {
                if (behaviour.Enabled)
                    behaviour.OnLateUpdate();
            });
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

        public virtual void OnEnable()
        {
        }

        public virtual void OnDisable()
        {
        }

        // Set this behaviour manager enabled or disabled and also set
        // its behaviours enabled or disabled.
        public void SetEnabled(bool enabled)
        {
            _enabled = enabled;
            _behaviours.ForEach(behaviour => behaviour.SetEnabled(enabled));

            if (_enabled) {
                OnEnable();
                return;
            }

            OnDisable();
        }
        #endregion
    }
}
