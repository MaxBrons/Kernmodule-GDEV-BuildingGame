using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuildingManager : MonoBehaviour
{
    private IBuildingState currentBuildingState;



    private void Update()
    {
        currentBuildingState?.Update(this);
    }

    public void SetCurrentState(IBuildingState newState)
    {
        currentBuildingState.Exit(this);
        currentBuildingState = newState;
        currentBuildingState.Start(this);
    }
}
