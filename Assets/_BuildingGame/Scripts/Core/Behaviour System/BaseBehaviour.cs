
namespace BuildingGame.Core
{
    // This is a base class for all objects that normally would extend
    // from the Unity MonoBehaviour class.
    public abstract class BaseBehaviour : IMonoBehaviour
    {
        public bool Enabled => _enabled;

        protected bool _enabled = true;

        public virtual void OnStart()
        {
        }

        public virtual void OnAwake()
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnFixedUpdate()
        {
        }

        public virtual void OnLateUpdate()
        {
        }

        public virtual void OnDestroy()
        {
        }

        public virtual void OnEnable()
        {
        }

        public virtual void OnDisable()
        {
        }

        public void SetEnabled(bool value)
        {
            _enabled = value;
        }
    }
}
