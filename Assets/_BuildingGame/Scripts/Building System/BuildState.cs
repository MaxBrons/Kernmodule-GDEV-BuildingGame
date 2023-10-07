using BuildingGame.Data;
using BuildingGame.Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingGame.BuildingSystem
{
    public class BuildState : IBuildingState
    {
        private List<SnappingPoint> _snappingPoints;
        private int _currentSelectedSnappingPoint;
        private int _maxSnappingPoint;
        private IInventory _inventory;

        private const string INVENTORY_NAME = "Inventory";
        private const string FOUNDATION_NAME = "Foundation";

        public void Exit(BuildingBehaviour buildingBehaviour)
        {
            buildingBehaviour.CurrentSelectedStructure.DestroyPreviewStructure();
        }

        public void Start(BuildingBehaviour buildingBehaviour)
        {
            buildingBehaviour.CurrentSelectedStructure.SpawnPreviewStructure(PlayerCameraRaycast.RaycastForPoint());
            _currentSelectedSnappingPoint = 0;

            _inventory = LostAndFound.Retrieve<IInventory>(INVENTORY_NAME);
        }

        public void Update(BuildingBehaviour buildingBehaviour)
        {
            // Switching the currently selected structure
            if (buildingBehaviour.inputActions.Building.SwapBuilding.WasPressedThisFrame()) {
                buildingBehaviour.SelectNextStructure();
                buildingBehaviour.CurrentSelectedStructure.SpawnPreviewStructure(PlayerCameraRaycast.RaycastForPoint());
            }

            // Getting the target transform and pos to use later
            Transform hitTransform = PlayerCameraRaycast.RaycastForTransform();
            Vector3 hitPosition = PlayerCameraRaycast.RaycastForPoint();

            Vector3 targetPos = new Vector3();
            Vector3 targetRot = new Vector3();

            if (buildingBehaviour.inputActions.Building.Rotate.WasPressedThisFrame()) {
                _currentSelectedSnappingPoint += 1;
                if (_currentSelectedSnappingPoint > 3) { _currentSelectedSnappingPoint = 0; }
            }

            if (hitTransform != null) {
                // Change target depending on you look at a structure or ground
                if (hitTransform.tag == "Structure") {
                    _snappingPoints = new List<SnappingPoint>();

                    // Apply the snapping points offset
                    // TODO: Make it nicer instead of using strings and switch case (might be harder without the use of monobehaviours)
                    switch (hitTransform.name) {
                        case "Foundation(Clone)":
                            foreach (SnappingPoint snappingPoint in buildingBehaviour.CurrentSelectedStructure.snappingPoints) {
                                if (snappingPoint.allowedStructures.Equals(AllowedStructures.Foundation)) { _snappingPoints.Add(snappingPoint); }
                            }

                            _maxSnappingPoint = _snappingPoints.Count - 1;
                            if (_currentSelectedSnappingPoint > _maxSnappingPoint) { _currentSelectedSnappingPoint = _maxSnappingPoint; }
                            targetPos = hitTransform.position + _snappingPoints[_currentSelectedSnappingPoint].posistion;
                            targetRot = _snappingPoints[_currentSelectedSnappingPoint].rotation;
                            break;

                        case "Wall(Clone)":

                            foreach (SnappingPoint snappingPoint in buildingBehaviour.CurrentSelectedStructure.snappingPoints) {
                                if (snappingPoint.allowedStructures.Equals(AllowedStructures.Wall)) { _snappingPoints.Add(snappingPoint); }
                            }

                            _maxSnappingPoint = _snappingPoints.Count - 1;
                            if (_currentSelectedSnappingPoint > _maxSnappingPoint) { _currentSelectedSnappingPoint = _maxSnappingPoint; }
                            targetPos = hitTransform.position + _snappingPoints[_currentSelectedSnappingPoint].posistion;
                            targetRot = _snappingPoints[_currentSelectedSnappingPoint].rotation;

                            break;
                    }

                }
                // When not looking at a structure make the target the point you're looking at
                else { targetPos = hitPosition; }

                // Place building when clicking
                if (buildingBehaviour.inputActions.Building.PlaceBluiding.WasPressedThisFrame()) {
                    if (_inventory == null)
                        return;

                    if (!_inventory.Items.ContainsKey(FOUNDATION_NAME) || _inventory.Items[FOUNDATION_NAME].Count <= 0)
                        return;

                    if (buildingBehaviour.CurrentSelectedStructure.TryPlace(targetPos, targetRot)) {
                        _inventory.RemoveItem(new() { Name = FOUNDATION_NAME, Count = 1 });
                    }
                }
                else {
                    buildingBehaviour.CurrentSelectedStructure.MoveStructure(targetPos, targetRot);
                }
            }
        }
    }
}
