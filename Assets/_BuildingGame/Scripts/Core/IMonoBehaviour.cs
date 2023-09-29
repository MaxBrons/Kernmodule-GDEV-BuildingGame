
namespace BG.Core
{
    public interface IMonoBehaviour : IBehaviour
    {
        public bool Enabled { get; }

        public void OnEnable();
        public void OnDisable();
        public void SetEnabled(bool value);
    }
}
