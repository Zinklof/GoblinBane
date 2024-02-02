using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Linq;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string FileName;
    [SerializeField] private bool useEncryption;


    private GameData gameData;
    private List<IDataPersistance> dataPersistanceObjects;
    private FileDataHandeler dataHandeler;

    public static DataPersistanceManager Instance { get; private set; }

    private void Awake()
    {

        if (Instance != null)
        {
            Debug.LogError("Found more than one Data Persistance Manager in the scene. Please make sure there is only one Data Peristance Manger at a time.");
        }
        Instance = this;
    }

    private void Start()
    {
        this.dataHandeler = new FileDataHandeler(Application.persistentDataPath, FileName, useEncryption);
        this.dataPersistanceObjects = FindAllDataPersitanceObjects();

        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }
    
    public void LoadGame()
    {
        this.gameData = dataHandeler.Load();

        if (this.gameData == null)
        {
            Debug.Log("No save game found, initalizing to new game.");
            NewGame();
        }

        foreach (IDataPersistance dataPersistancsObj in dataPersistanceObjects)
        {
            dataPersistancsObj.LoadData(gameData);
        }
    }

    public void SaveGame() 
    {
        foreach (IDataPersistance dataPersistancsObj in dataPersistanceObjects)
        {
            dataPersistancsObj.SaveData(gameData);
        }

        dataHandeler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistance> FindAllDataPersitanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistanceObjects);
    }
}
