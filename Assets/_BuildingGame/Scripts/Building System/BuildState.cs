using UnityEngine;

namespace BuildingGame.BuildingSystem
{
    public class BuildState : IBuildingState
    {
        public void Exit(BuildingBehaviour buildingBehaviour)
        {
        }

        public void Start(BuildingBehaviour buildingBehaviour)
        {
            buildingBehaviour.CurrentSelectedStructure.SpawnPreviewStructure(PlayerCameraRaycast.RaycastForPoint()); 
        }

        public void Update(BuildingBehaviour buildingBehaviour)
        {
            Transform hitTransform = PlayerCameraRaycast.RaycastForTransform();
            Vector3 hitPosition = PlayerCameraRaycast.RaycastForPoint();

            Vector3 target;

            //Change target depending on you look at a structure or ground
            if(hitTransform.tag == "Structure") { target = hitTransform.position; }
            else { target = hitPosition; }

            if (buildingBehaviour.inputActions.Building.PlaceBluiding.WasPressedThisFrame())
            {
                buildingBehaviour.CurrentSelectedStructure.TryPlace(target);
            }
            else
            {
                buildingBehaviour.CurrentSelectedStructure.MoveStructure(target);
            } 
        }
    }
}
