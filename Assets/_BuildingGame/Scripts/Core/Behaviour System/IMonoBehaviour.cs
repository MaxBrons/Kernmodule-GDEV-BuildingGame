
namespace BuildingGame.Core
{
    // An interface for the default behaviour of a Unity MonoBehaviour object
    // (to enable and disable the behaviour).
    public interface IMonoBehaviour : IBehaviour
    {
        public bool Enabled { get; }

        public void OnEnable();
        public void OnDisable();
        public void SetEnabled(bool value);
    }
}
