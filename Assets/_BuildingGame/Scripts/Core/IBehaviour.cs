
namespace BG.Core
{
    public interface IBehaviour
    {
        public void OnAwake();
        public void OnStart();
        public void OnUpdate();
        public void OnFixedUpdate();
        public void OnLateUpdate();
        public void OnDestroy();
    }
}
