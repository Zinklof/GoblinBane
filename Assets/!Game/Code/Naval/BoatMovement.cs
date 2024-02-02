using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BoatMovement : MonoBehaviour
{
    [Header("references")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform boatFront;
    [SerializeField] GameObject gruntPrefab;
    [SerializeField] GameObject archerPrefab;
    [SerializeField] GameObject beserkerPrefab;
    [SerializeField] GameObject cheiftanPrefab;
    [Header("Cargo")]
    [SerializeField] int grunts;
    [SerializeField] int archers;
    [SerializeField] int beserkers;
    [SerializeField] int cheiftans;
    [Header("Spawn Info")]
    [SerializeField] Transform cargoDeployment;
    [SerializeField] float spawnRateSeconds;
    [Header("Debug Variables")]
    [SerializeField] private float timeSinceSpawn;
    [SerializeField] private Transform iSpawnedHere;
    [SerializeField] private bool reachedDeploymentStage = false;
    [SerializeField] private bool finDeploy = false;

    public void SetCargo(int goblinGruntNum, int goblinArcherNum, int goblinBeserkerNum, int goblinCheiftanNum)
    {
        grunts = goblinGruntNum;
        archers = goblinArcherNum;
        beserkers = goblinBeserkerNum;
        cheiftans = goblinCheiftanNum;
    }

    public void setSpawnReference(Transform spawnPoint)
    {
        iSpawnedHere = spawnPoint;
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
        iSpawnedHere = transform;

        List<Transform> coastPoints = new List<Transform>();

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("CoastalPoint"))
        {
            coastPoints.Add(go.transform);
        }

        Transform objective = GetClosestCoastPoint(coastPoints);

        cargoDeployment = objective;
        agent.SetDestination(objective.position);
    }

    private void deployCargo()
    {
        agent.SetDestination(transform.position);
        if (timeSinceSpawn > spawnRateSeconds)
        {
            if (grunts > 0)
            {
                Instantiate(gruntPrefab, cargoDeployment.position, transform.rotation);
                grunts--;
                timeSinceSpawn = 0;
            }
            else if (archers > 0)
            {
                Instantiate(archerPrefab, cargoDeployment.position, transform.rotation);
                archers--;
                timeSinceSpawn = 0;
            }
            else if (beserkers > 0)
            {
                Instantiate(beserkerPrefab, cargoDeployment.position, transform.rotation);
                beserkers--;
                timeSinceSpawn = 0;
            }
            else if (cheiftans > 0)
            {
                Instantiate(cheiftanPrefab, cargoDeployment.position, transform.rotation);
                cheiftans--;
                timeSinceSpawn = 0;
            }
            else
            {
                agent.SetDestination(iSpawnedHere.position);
                finDeploy = true;
                reachedDeploymentStage = false;
            }
        }
        else
        {
            return;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "CoastalPoint" && !finDeploy)
        {
            reachedDeploymentStage = true;
        }
        if (collision.gameObject.tag == "Spawnpoint" && finDeploy)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        timeSinceSpawn += Time.deltaTime;

        if (timeSinceSpawn > 30f && !finDeploy)
        {
            reachedDeploymentStage = true;
        }
        if (timeSinceSpawn > 20f && finDeploy)
        {
            Destroy(gameObject);
        }

        if (reachedDeploymentStage)
        {
            deployCargo();
        }
    }
}
