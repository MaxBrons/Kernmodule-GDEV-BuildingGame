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

            if (Input.GetMouseButtonDown(0)) // TODO: Maak beter
            {
                buildingBehaviour.CurrentSelectedStructure.TryPlace(hitPosition);
            }
            else
            {
                if (PlayerCameraRaycast.RaycastForTransform().tag == "Structure")
                {
                    buildingBehaviour.CurrentSelectedStructure.MoveStructure(hitTransform.position + new Vector3(2, 0, 0));
                }
                else
                {
                    buildingBehaviour.CurrentSelectedStructure.MoveStructure(PlayerCameraRaycast.RaycastForPoint());
                }

            }  
        }
    }
}
