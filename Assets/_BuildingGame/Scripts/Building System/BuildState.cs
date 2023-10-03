using UnityEngine;

namespace BuildingGame.BuildingSystem
{
    public class BuildState : IBuildingState
    {
        private int currentSnappingPoint;

        public void Exit(BuildingBehaviour buildingBehaviour)
        {
        }

        public void Start(BuildingBehaviour buildingBehaviour)
        {
            buildingBehaviour.CurrentSelectedStructure.SpawnPreviewStructure(PlayerCameraRaycast.RaycastForPoint()); 
            currentSnappingPoint = 0;
        }

        public void Update(BuildingBehaviour buildingBehaviour)
        {
            Transform hitTransform = PlayerCameraRaycast.RaycastForTransform();
            Vector3 hitPosition = PlayerCameraRaycast.RaycastForPoint();

            Vector3 target;

            if(buildingBehaviour.inputActions.Building.Rotate.WasPressedThisFrame()) 
            { 
                currentSnappingPoint += 1;
                if (currentSnappingPoint > 3) { currentSnappingPoint = 0; }
            }

            //Change target depending on you look at a structure or ground
            if(hitTransform.tag == "Structure") 
            { 
                //target = hitTransform.position + 
                  //  buildingBehaviour.CurrentSelectedStructure.snappingPoints[currentSnappingPoint]; 
            }
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
