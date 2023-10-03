namespace BuildingGame.BuildingSystem
{
    public interface IBuildingState
    {
        public void Start(BuildingBehaviour buildingBehaviour);
        public void Update(BuildingBehaviour buildingBehaviour);
        public void Exit(BuildingBehaviour buildingBehaviour);
    }
}
