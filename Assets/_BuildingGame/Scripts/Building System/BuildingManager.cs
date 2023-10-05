using BuildingGame.Input;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BuildingGame.BuildingSystem
{
    public class BuildingBehaviour : Core.BaseBehaviour
    {
        public InputActionsCore inputActions = new();

        private IBuildingState _buildingState;
        private IBuildingState _destroyState;

        // Loads all structure scriptable objects
        private Structure[] structures = Resources.LoadAll(
            "ScriptableObjects/Structures", typeof(Structure)).Cast<Structure>().ToArray();

        private int _currentSelectedStructureIndex = 0;


        // field and property to manage current building state
        private IBuildingState currentBuildingState;
        private IBuildingState CurrentBuildingState
        {
            get { return currentBuildingState; }
            set
            {
                currentBuildingState?.Exit(this);
                currentBuildingState = value;
                currentBuildingState.Start(this);
            }
        }

        // field and property to manage current selected structure
        private Structure currentSelectedStructure;
        public Structure CurrentSelectedStructure
        {
            get { return currentSelectedStructure; }
            set { currentSelectedStructure = value; }
        }

        public override void OnStart()
        {
            // Initialize all states
            _buildingState = new BuildState();
            _destroyState = new DestroyState();

            inputActions.Enable();

            // Select the first structure and start in building state
            CurrentSelectedStructure = structures[_currentSelectedStructureIndex];
            CurrentBuildingState = _buildingState;
        }

        public override void OnUpdate()
        {
            CurrentBuildingState?.Update(this);
        }

        // Selects next structure
        public void SelectNextStructure()
        {
            CurrentSelectedStructure.DestroyPreviewStructure();

            _currentSelectedStructureIndex += 1;

            if(_currentSelectedStructureIndex > structures.Length - 1) { _currentSelectedStructureIndex = 0; }

            CurrentSelectedStructure = structures[_currentSelectedStructureIndex];
        }
    }
}

