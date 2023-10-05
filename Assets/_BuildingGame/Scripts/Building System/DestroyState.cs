using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingGame.BuildingSystem
{
    public class DestroyState : IBuildingState
    {
        public void Exit(BuildingBehaviour buildingBehaviour)
        {
        }

        public void Start(BuildingBehaviour buildingBehaviour)
        {
        }

        public void Update(BuildingBehaviour buildingBehaviour)
        {
            if (buildingBehaviour.inputActions.Building.PlaceBluiding.WasPerformedThisFrame())
            {
                Transform hit = PlayerCameraRaycast.RaycastForTransform();

                if (hit?.tag == "Structure")
                {
                    Object.Destroy(hit.gameObject);
                }
            }
        }
    }
}
