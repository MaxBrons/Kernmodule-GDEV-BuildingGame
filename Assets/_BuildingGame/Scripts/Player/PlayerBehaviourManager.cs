using BuildingGame.Core;

namespace BuildingGame.Player
{
    public class PlayerBehaviourManager : BehaviourManager
    {
        protected override void Initialize()
        {
            _behaviours = new()
            {
                new PlayerMovement()
            };
        }
    }
}