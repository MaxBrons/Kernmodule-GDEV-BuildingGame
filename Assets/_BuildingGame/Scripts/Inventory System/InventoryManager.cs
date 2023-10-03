using BuildingGame.Core;
using BuildingGame.Data;
using System;
using UnityEngine;

namespace BuildingGame.Inventory
{
    public class InventoryManager : BehaviourManager
    {
        protected override void Initialize()
        {
            InventoryData data = LostAndFound.Retrieve("InventoryData") as InventoryData;
            if (data != null) {
                IBehaviour behaviour = Activator.CreateInstance(data.Script.GetClass()) as IBehaviour;
                behaviour?.OnAwake();
            }
        }
    }
}