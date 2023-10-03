using UnityEngine;

namespace BuildingGame.BuildingSystem
{
    public class BuildState : IBuildingState
    {
        public void Exit(BaseBuildingManager baseBuildManager)
        {
        }

        public void Start(BaseBuildingManager baseBuildManager)
        {
            Debug.Log("Started Building");

            baseBuildManager.CurrentSelectedStructure.SpawnPreviewStructure(PlayerCameraRaycast.RaycastForPoint());
        }

        public void Update(BaseBuildingManager baseBuildManager)
        {
            baseBuildManager.CurrentSelectedStructure.MoveStructure(PlayerCameraRaycast.RaycastForPoint());
        }
    }
}
