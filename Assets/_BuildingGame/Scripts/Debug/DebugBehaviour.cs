using BuildingGame.Core;
using UnityEngine;

namespace BuildingGame
{
    // This serves as an example class of how to implement
    // this behaviour system.
    public sealed class DebugBehaviourManager : BehaviourManager
    {
        protected override void Initialize()
        {
            _behaviours = new()
            {
                new DebugBehaviour()
            };
        }
    }

    // This class serves to log when the game has started and ended
    public class DebugBehaviour : BaseBehaviour
    {
#if DEBUG
        private const string LOG_PREFIX = "[GAME] ";
        private const string LOG_GAME_START = "Started";
        private const string LOG_GAME_END = "End";
        public override void OnAwake()
        {
            Debug.Log(LOG_PREFIX + LOG_GAME_START);
        }

        public override void OnDestroy()
        {
            Debug.Log(LOG_PREFIX + LOG_GAME_END);
        }
#endif
    }
}
