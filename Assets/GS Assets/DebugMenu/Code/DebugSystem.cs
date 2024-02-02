using UnityEngine;
using TMPro;
using UnityEngine.Profiling;

public class DebugSystem : MonoBehaviour
{
    [Header("Primary Operation Values")]
    [SerializeField] private TMP_Text sysText;
    [SerializeField] private TMP_Text OSText;
    [SerializeField] private TMP_Text basicText;
    [SerializeField] private GameObject DebugMenu;

    [Header("Debug Values")]
    [SerializeField] private bool menuOpen = false;

    public int CountPlayers()
    {
        int playerCounter = 0;
        GameObject[] playerCount = GameObject.FindGameObjectsWithTag("Player"); //finds all objects tagged as a player

        foreach (GameObject go in playerCount) //adds one to player counter for each player present in scene
        {
            playerCounter++;
        }

        return playerCounter;
    }
    public int CountLights()
    {
        //int lightCounter = 0;
        //GameObject[] lightCount = GameObject.FindGameObjectsWithTag("Light"); //finds all objects tagged as a light/light emitting object

        //foreach (GameObject go in lightCount) //adds one to light counter for each light present in the scene
        {
            //lightCounter++;
        }

        //return lightCounter;
        return 999;
    }
    public int CountCameras()
    {
        int cameraCounter = 0;
        GameObject[] cameraCount = GameObject.FindGameObjectsWithTag("Camera"); //finds all objects tagged as a camera

        foreach (GameObject go in cameraCount) //adds one to camera counter for each camera present in the scene
        {
            cameraCounter++;
        }

        //cameraCounter++; //Depreciated this funtionality, any solid program shouldn't 100% need the main camera to be tagged as such.
        return cameraCounter;
    }
    void OpenMenu() // misleading name it actually opens and closes the menu
    {
        if(menuOpen) // if menu already open then close
        {
            menuOpen = false;
            DebugMenu.SetActive(false);
        }
        else if(!menuOpen) // if menu closed then open
        {
            menuOpen = true;
            DebugMenu.SetActive(true);
        }
    }
    void SystemStats()
    {
        string processor = SystemInfo.processorType; //gets processor name
        float clockSpeedMath = SystemInfo.processorFrequency * 0.001f; //gets processor base clock in MGHz and trasnferes to GHz
        string clockspeed = clockSpeedMath.ToString(); //turns the prevous math into a string
        string coreCount = SystemInfo.processorCount.ToString(); //gets core count including digital cores

        string totalAllocatedRam = "" + Mathf.Round(Profiler.GetTotalAllocatedMemoryLong() * 0.000001f) * 0.01f + " GB,"; // gets total allocated ram in KB and transfers to GB with two decimal places
        string totalRam = "" + Mathf.Round(Profiler.GetTotalReservedMemoryLong() * 0.000001f) * 0.01f + " GB,"; // gets total reserved ram in KB and transfers to GB with two decimal places
        //string unusedRAM = "" + Mathf.Round(Profiler.GetTotalUnusedReservedMemoryLong() * 0.000001f) * 0.01f + " GB"; // DEPRECIATED no need to get this information to an end user // gets unsed reserved ram in KB and transfers to GB with two decimal places

        //string ram = "" + SystemInfo.systemMemorySize * 0.001f + " GB,"; // gets systems total ram //DEPRECIATED no need to get this information to an end user
        string GPUram = "" + SystemInfo.graphicsMemorySize * 0.001f + " GB,"; // gets gpus total ram

        string GPU = SystemInfo.graphicsDeviceName; //gets gpu name

        sysText.text = (processor + "<br>" + clockspeed + " (base clock speed in GHz)" + "<br>" + coreCount + " (includes Digital Cores)" + "<br> <br>" + /*ram + " " +*/ totalAllocatedRam + " " + totalRam + /*" " + unusedRAM +*/ " (Used Ram, Total Reserved Ram) <br>" + GPUram + "<br> <br>" + GPU); // displayes aformentioned statistics on debug menu
    }

    void Update()
    {
        SystemStats();
        OSText.text = (SystemInfo.operatingSystem.ToString()); // gets OS
        basicText.text = (CountPlayers().ToString() + "<br>" + CountLights().ToString() + "<br>" + CountCameras().ToString()); //displays player, light, and camera count

        if(Input.GetKeyUp(KeyCode.BackQuote) || Input.GetKeyUp(KeyCode.Tilde))
        {
            OpenMenu();
        }
    }
}
