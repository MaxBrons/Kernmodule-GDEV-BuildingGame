namespace BuildingGame.BuildingSystem
{
    public interface IBuildingState
    {
        public void Start(BaseBuildingManager baseBuildManager);
        public void Update(BaseBuildingManager baseBuildManager);
        public void Exit(BaseBuildingManager baseBuildManager);
    }
}
