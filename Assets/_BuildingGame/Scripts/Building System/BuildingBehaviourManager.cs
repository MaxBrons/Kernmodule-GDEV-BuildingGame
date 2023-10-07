namespace BuildingGame.BuildingSystem
{
    public class BuildingBehaviourManager : Core.BehaviourManager
    {
        protected override void Initialize()
        {
            _behaviours = new()
            {
                new BuildingBehaviour(),
            };
        }
    }
}
