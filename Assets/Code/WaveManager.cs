using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [Header("references")]
    [SerializeField] GameObject boatPrefab;
    [SerializeField] TMP_Text gobCountText; 
    [SerializeField] List<GameObject> spawnpoints = new List<GameObject>();
    [Header("Debug Variables")]
    [SerializeField] int wave;
    [SerializeField] GameObject spawnpoint;
    [SerializeField] int goblinGrunts;
    [SerializeField] int goblinArchers;
    [SerializeField] int goblinBeserkers;
    [SerializeField] int goblinCheiftans;
    [Header("Debug Variables RNG")]
    [SerializeField] int goblinGruntOdds;
    [SerializeField] int goblinArcherOdds;
    [SerializeField] int goblinBeserkerOdds;
    [SerializeField] int goblinCheiftanOdds;
    [Header("wave info")]
    [SerializeField] int goblinCount;

    private int goblinArcherTrueOdds;
    private int goblinBeserkerTrueOdds;
    private int maxGoblins = 20;

    public void GoblinDied()
    {
        goblinCount--;
        gobCountText.text = "Goblins: " + goblinCount;
    }

    private void Start()
    {
        StartWave();
    }

    public void StartWave()
    {
        GetSpawns();
        wave++;

        goblinCount = BoatNumber() * 21;
        gobCountText.text = "Goblins: " + goblinCount;

        DefineEnemyOdds();

        SpawnBoat(spawnpoints, BoatNumber());
    }

    private void GetSpawns()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Spawnpoint"))
        {
            spawnpoints.Add(go);
        }
    }

    private void DefineEnemyOdds()
    {
        if (wave <= 2)
        {
            goblinGruntOdds = 100;
            goblinArcherOdds = 0;
            goblinBeserkerOdds = 0;
            goblinCheiftanOdds = 0;
        }
        else if(wave <= 4)
        {
            goblinGruntOdds = 75;
            goblinArcherOdds = 25;
            goblinBeserkerOdds = 0;
            goblinCheiftanOdds = 0;
        }
        else if(wave <= 6)
        {
            goblinGruntOdds = 55;
            goblinArcherOdds = 35;
            goblinBeserkerOdds = 10;
            goblinCheiftanOdds = 0;
        }
        else if(wave <= 10)
        {
            goblinGruntOdds = 30;
            goblinArcherOdds = 30;
            goblinBeserkerOdds = 20;
            goblinCheiftanOdds = 20;
        }
        else if(wave <= 20)
        {
            goblinGruntOdds = 20;
            goblinArcherOdds = 30;
            goblinBeserkerOdds = 25;
            goblinCheiftanOdds = 25;
        }
        else if(wave <= 30)
        {
            goblinGruntOdds = 18;
            goblinArcherOdds = 27;
            goblinBeserkerOdds = 30;
            goblinCheiftanOdds = 25;
        }
        else if(wave <= 45)
        {
            goblinGruntOdds = 12;
            goblinArcherOdds = 23;
            goblinBeserkerOdds = 40;
            goblinCheiftanOdds = 25;
        }
        else if(wave <= 60)
        {
            goblinGruntOdds = 10;
            goblinArcherOdds = 20;
            goblinBeserkerOdds = 45;
            goblinCheiftanOdds = 25;
        }
        else if(wave <= 100)
        {
            goblinGruntOdds = 0;
            goblinArcherOdds = 20;
            goblinBeserkerOdds = 55;
            goblinCheiftanOdds = 25;
        }
        else if (wave <= 150)
        {
            goblinGruntOdds = 0;
            goblinArcherOdds = 5;
            goblinBeserkerOdds = 70;
            goblinCheiftanOdds = 25;
        }

        goblinArcherTrueOdds = goblinArcherOdds + goblinGruntOdds;
        goblinBeserkerTrueOdds = goblinArcherOdds - goblinGruntOdds + goblinBeserkerOdds;
    }

    private int BoatNumber()
    {
        if (wave < 3)
            return 1;
        else if (wave < 5)
            return 2;
        else if (wave < 9)
            return 3;
        else if (wave < 12)
            return 4;
        else if (wave < 16)
            return 5;
        else if (wave < 20)
            return 6;
        else if (wave < 26)
            return 7;       
        else if (wave < 32)
            return 8;
        else if (wave < 40)
            return 9;
        else if (wave < 50)
            return 10;
        else if (wave < 70)
            return 11;
        else
        {
            maxGoblins = 40;
            return 12;
        }
    }

    private void GenerateCargo()
    {
        goblinGrunts = 0;
        goblinArchers = 0;
        goblinBeserkers = 0;
        goblinCheiftans = 0;


        for (int i = 0; i <= maxGoblins; i++) 
        {
            int rng = Random.Range(0, 100);
            rng++;

            if (rng <= goblinGruntOdds)
                goblinGrunts++;
            else if (rng <= goblinArcherTrueOdds)
                goblinArchers++;
            else if (rng <+ goblinBeserkerTrueOdds)
                goblinBeserkers++;
            else
                goblinCheiftans++;
        }
    }

    private void SpawnBoat(List<GameObject> availableSpawnpoints, int number)
    {
        for (int i = 0; i < number; i++)
        {

            int temp = Random.Range(0, availableSpawnpoints.Count);

            spawnpoint = availableSpawnpoints[temp];

            availableSpawnpoints.RemoveAt(temp);

            GameObject boat = Instantiate(boatPrefab, spawnpoint.transform.position, spawnpoint.transform.rotation);

            BoatMovement boatMovementScript = boat.GetComponent<BoatMovement>();

            boatMovementScript.setSpawnReference(spawnpoint.transform);

            GenerateCargo();

            boatMovementScript.SetCargo(goblinGrunts, goblinArchers, goblinBeserkers, goblinCheiftans);
        }
    }

    private void Update()
    {
        if (goblinCount <= 0)
        {
            StartWave();
        }
    }
}
