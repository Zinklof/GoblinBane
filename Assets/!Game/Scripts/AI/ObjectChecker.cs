using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Threading;
using UnityEngine;
using UnityEngine.Profiling;

static class ObjectChecker
{
    [SerializeField] public static List<Transform> buildings = new List<Transform>();
    [SerializeField] public static List<GameObject> goblins = new List<GameObject>();

    public static Transform findClosestObject(Transform obj)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = obj.position;
        foreach (Transform potentialTarget in buildings)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }

    public static GameObject findClosestGoblin(Transform obj)
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = obj.position;
        foreach (GameObject potentialTarget in goblins)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }

    public static void AddObject (Transform obj)
    {
        buildings.Add (obj);
    }

    public static void RemoveObject (Transform obj)
    {
        buildings.Remove (obj);
    }

    public static void AddGoblin(GameObject obj)
    {
        goblins.Add(obj);
    }

    public static void RemoveGoblin(GameObject obj)
    {
        goblins.Remove(obj);
    }

    public static void ResetLists()
    {
        buildings.Clear();
        goblins.Clear();

        if (buildings.Count != 0 ) 
        {
            Debug.LogError("ObjectChecker.cs - ResetLists() - Error reseting Buildings List");
        }
        if (goblins.Count != 0)
        {
            Debug.LogError("ObjectChecker.cs - ResetLists() - Error reseting Goblins List");
        }
    }
}
