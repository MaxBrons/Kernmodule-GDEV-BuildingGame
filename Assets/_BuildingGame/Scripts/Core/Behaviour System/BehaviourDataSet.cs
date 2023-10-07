using BuildingGame.Data;
using System;

namespace BuildingGame.Core
{
    // A serialized struct for storing Behaviour Data
    // in the scratchpad called the LostAndFound.
    [Serializable]
    public struct BehaviourDataSet
    {
        public string ID;
        public BehaviourData Data;
    }
}