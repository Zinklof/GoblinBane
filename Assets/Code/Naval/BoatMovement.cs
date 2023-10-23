using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoatMovement : MonoBehaviour
{
    [Header("references")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject gruntPrefab;
    [SerializeField] GameObject archerPrefab;
    [SerializeField] GameObject beserkerPrefab;
    [SerializeField] GameObject cheiftanPrefab;

    public void SetCargo(int goblinGruntNum, int goblinArcherNum, int goblinBeserkerNum, int goblinCheiftanNum)
    {
    }

    Transform GetClosestCoastPoint(List<Transform> coastPoints)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in coastPoints)
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

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        List<Transform> coastPoints = new List<Transform>();

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("CoastalPoint"))
        {
            coastPoints.Add(go.transform);
        }

        Transform objective = GetClosestCoastPoint(coastPoints);

        agent.SetDestination(objective.position);
    }
}
