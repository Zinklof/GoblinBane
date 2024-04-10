using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] float moveSpeed;
    [SerializeField] float speedRegen;
    [Header("References")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] AudioSource audioManager;
    [SerializeField] WaveManager waveManager;
    [SerializeField] BuildingHealth buildingHealth = null;
    [SerializeField] Transform building = null;
    [Header("Audio")]
    [SerializeField] int idleSoundOdds;
    [SerializeField] float idleSoundTime;
    [SerializeField] AudioClip[] idleSounds;
    [Header("Debug Variables")]
    [SerializeField] float attackDelay;
    [SerializeField] float idleDelay;
    [SerializeField] float distanceFromTarget = 999;

    private float timeSinceDistanceCheck = 0;
    private float startSpeed;

    public void freeze()
    {
        moveSpeed = 0;
        agent.speed = 0;
    }

    public void Poison()
    {

    }

    public void DamageGoblin(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            killGoblin();
        }
    }

    private void killGoblin()
    {
        buildingHealth.OnTowerDestroyed -= this.GetObjective;
        waveManager.GoblinDied();
        ObjectChecker.RemoveGoblin(gameObject);
        MoneyManager.SpendMoney(-moneyGain);
        Destroy(gameObject);
    }

    private void Awake()
    {
        attackDistance = attackDistance * attackDistance;
        startSpeed = moveSpeed;
        agent.stoppingDistance = stoppingDistance;
        ObjectChecker.AddGoblin(gameObject);
        GameObject temp = GameObject.FindGameObjectWithTag("Scriptoid");
        waveManager = temp.GetComponent<WaveManager>();
        WaveManager.WaveCleared += this.killGoblin;
        GetObjective();
    }

    private void GetObjective()
    {
        building = ObjectChecker.findClosestObject(transform);

        buildingHealth = building.GetComponent<BuildingHealth>();
        buildingHealth.OnTowerDestroyed += this.GetObjective;

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

    void IdleSoundManager()
    {
        int tempRNGSound = Random.Range(0, 100);
        tempRNGSound++;
        if (tempRNGSound < idleSoundOdds)
        {
            audioManager.pitch = Random.Range(0.8f, 1.2f);
            audioManager.clip = idleSounds[Random.Range(0, idleSounds.Length)];
            audioManager.Play();
        }
    }    

    private void FixedUpdate()
    {
        if (idleDelay > idleSoundTime)
        {
            IdleSoundManager();
            idleDelay = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float tempTime = Time.deltaTime;

        idleDelay = idleDelay + tempTime;
        timeSinceDistanceCheck = timeSinceDistanceCheck + tempTime;

        if (building != null && timeSinceDistanceCheck > .5f)
        {
            distanceFromTarget = (float)ZinklofDev.Utils.MathZ.VectorDistanceSquared(building.position, transform.position);
            timeSinceDistanceCheck = 0;
        }

        if (distanceFromTarget < attackDistance)
        {
            attackDelay = attackDelay - tempTime;
            attack();
        }

        if (moveSpeed < startSpeed)
        {
            moveSpeed += speedRegen * Time.deltaTime;
            if (moveSpeed > startSpeed)
            {
                moveSpeed = startSpeed;
                agent.speed = moveSpeed;
            }
            else
            {
                agent.speed = moveSpeed;
            }
        }
    }
}
