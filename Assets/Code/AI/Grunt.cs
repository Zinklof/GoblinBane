using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grunt : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int damage = 15;
    [SerializeField] float attackSpeedSeconds = 0.25f;
    [SerializeField] float attackDistance = 1;
    [Header("References")]
    [SerializeField] List<Transform> buildings = new List<Transform>();
    [SerializeField] NavMeshAgent agent;
    [SerializeField] BuildingHealth buildingHealth = null;
    [Header("Debug Variables")]
    [SerializeField] float attackDelay;
    [SerializeField] float distanceFromTarget;

    Transform GetClosestBuilding()
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
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

    private void Awake()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("building"))
        {
            buildings.Add(go.transform);
        }
    }

    private void FixedUpdate()
    {
        buildings.Clear();

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("building"))
        {
            buildings.Add(go.transform);
        }
    }

    private void getObjective()
    {
        Transform building = GetClosestBuilding();

        distanceFromTarget = Vector3.Distance(building.position, transform.position);

        buildingHealth = building.GetComponent<BuildingHealth>();

        agent.SetDestination(building.position);
    }

    private void attack()
    {
        if (attackDelay <= 0)
        {
            attackDelay = attackSpeedSeconds;

            buildingHealth.Damage(damage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        getObjective();

        if (distanceFromTarget < attackDistance)
        {
            attack();
            attackDelay = attackDelay - Time.deltaTime;
        }
        else
        {
            attackDelay = attackSpeedSeconds;
        }

        if (attackDelay < 0)
        {
            attackDelay = 0;
        }
    }
}
