using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingGame.BuildingSystem
{
    public class BaseBuildingManager : MonoBehaviour
    {
        private IBuildingState buildingState;
        private IBuildingState destroyState;

        [SerializeField] Structure foundationStructure;

        // field and property to manage current building state
        private IBuildingState currentBuildingState;
        private IBuildingState CurrentBuildingState
        {
            get { return this.currentBuildingState; }
            set
            {
                this.currentBuildingState?.Exit(this);
                this.currentBuildingState = value;
                this.currentBuildingState.Start(this);
            }
        }

        // field and property to manage current selected structure
        private Structure currentSelectedStructure;
        public Structure CurrentSelectedStructure
        {
            get { return currentSelectedStructure; }
            set { currentSelectedStructure = value; }
        }

        private void Awake()
        {
            buildingState = new BuildState();
            destroyState = new DestroyState();

            CurrentSelectedStructure = foundationStructure;
            CurrentBuildingState = buildingState;
        }

        private void Update()
        {
            CurrentBuildingState?.Update(this);
        }
    }
}

