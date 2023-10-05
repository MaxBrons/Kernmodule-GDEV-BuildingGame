using UnityEngine;

namespace BuildingGame.BuildingSystem
{
    public class BuildState : IBuildingState
    {
        private int _currentSnappingPoint;

        public void Exit(BuildingBehaviour buildingBehaviour)
        {
        }

        public void Start(BuildingBehaviour buildingBehaviour)
        {
            buildingBehaviour.CurrentSelectedStructure.SpawnPreviewStructure(PlayerCameraRaycast.RaycastForPoint()); 
            _currentSnappingPoint = 0;
        }

        public void Update(BuildingBehaviour buildingBehaviour)
        {
            if (buildingBehaviour.inputActions.Building.SwapBuilding.WasPressedThisFrame())
            {
                buildingBehaviour.SelectNextStructure();
                buildingBehaviour.CurrentSelectedStructure.SpawnPreviewStructure(PlayerCameraRaycast.RaycastForPoint());
            }


            Transform hitTransform = PlayerCameraRaycast.RaycastForTransform();
            Vector3 hitPosition = PlayerCameraRaycast.RaycastForPoint();

            Vector3 target = Vector3.one;

            if(buildingBehaviour.inputActions.Building.Rotate.WasPressedThisFrame()) 
            { 
                _currentSnappingPoint += 1;
                if (_currentSnappingPoint > 3) { _currentSnappingPoint = 0; }
            }

            // Change target depending on you look at a structure or ground
            if(hitTransform.tag == "Structure") 
            { 
                // Apply the snapping points offset
                // TODO: Make it nicer instead of using strings and switch case
                switch(hitTransform.name)
                {
                    case "Foundation(Clone)":
                        target = hitTransform.position +
                            buildingBehaviour.CurrentSelectedStructure.foundationSnappingPoints[_currentSnappingPoint];
                        break;
                    case "Wall(Clone)":
                        target = hitTransform.position +
                            buildingBehaviour.CurrentSelectedStructure.wallSnappingPoints[_currentSnappingPoint];
                        break;
                }
                 
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
