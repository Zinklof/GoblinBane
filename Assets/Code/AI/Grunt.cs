using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grunt : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int health = 100;
    [SerializeField] int damage = 15;
    [SerializeField] float attackSpeedSeconds = 0.25f;
    [SerializeField] float attackDistance = 1;
    [SerializeField] float stoppingDistance = 0;
    [SerializeField] int moneyGain = 25;
    [Header("References")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] WaveManager waveManager;
    [SerializeField] MoneyManager moneyManager;
    [SerializeField] BuildingHealth buildingHealth = null;
    [SerializeField] Transform building = null;
    [Header("Debug Variables")]
    [SerializeField] float attackDelay;
    [SerializeField] float distanceFromTarget;

    public void freeze()
    {

    }

    public void Poison()
    {

    }

    public void DamageGoblin(int damage)
    {
        health -= damage;
    }

    private void Awake()
    {
        agent.stoppingDistance = stoppingDistance;
        ObjectChecker.AddGoblin(gameObject);
        GameObject temp = GameObject.FindGameObjectWithTag("Scriptoid");
        waveManager = temp.GetComponent<WaveManager>();
        moneyManager = temp.GetComponent<MoneyManager>();
    }

    private void getObjective()
    {
        building = ObjectChecker.findClosestObject(transform);

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

    private void FixedUpdate()
    {
        if (building != null)
        {
            distanceFromTarget = Vector3.Distance(building.position, transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            waveManager.GoblinDied();
            ObjectChecker.RemoveGoblin(gameObject);
            moneyManager.SpendMoney(moneyGain);
            Destroy(gameObject);
        }

        if (building == null)
        {
            getObjective();
        }

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
