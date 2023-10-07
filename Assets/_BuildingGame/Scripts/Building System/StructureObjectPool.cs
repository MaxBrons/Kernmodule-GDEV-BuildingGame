using System.Collections.Generic;
using UnityEngine;

namespace BuildingGame.BuildingSystem
{
    // ---------------------------------------------------------------------------
    // UNUSED CODE
    // ---------------------------------------------------------------------------

    //public static class StructureObjectPool
    //{
    //    public static Structure foundation;
    //    public static Structure wall;

    //    private static List<GameObject> activeObjects;
    //    private static List<GameObject> inactiveObjects;

    //    //TODO: Improve without string based tags
    //    public static GameObject GetInstance(string tag)
    //    {
    //        foreach(GameObject obj in inactiveObjects)
    //        {
    //             if (obj.tag == tag)
    //            {
    //                inactiveObjects.Remove(obj);
    //                activeObjects.Add(obj);
    //                return obj;
    //            }
    //        }

    //        // No item in list contains a gameobject with tag
    //        switch(tag)
    //        {
    //            case "Wall":
    //                return CreateNewInstance(wall);

    //            case "Foundation":
    //                return CreateNewInstance(foundation);
    //        }

    //        return null;
    //    }

    //    public static void ReturnInstance(GameObject returnedObject)
    //    {
    //        activeObjects.Remove(returnedObject);
    //        returnedObject.SetActive(false);
    //        inactiveObjects.Add(returnedObject);
    //    }

    //    private static  GameObject CreateNewInstance(Structure structure)
    //    {
    //        GameObject newObj = Object.Instantiate(structure.structurePrefab);
    //        activeObjects.Add((newObj));
    //        return newObj;
    //    }
    //}
}

