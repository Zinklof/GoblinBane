using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ZinklofDev.Console;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private static int cash = 300;
    [SerializeField] private static bool infiniteMoney = false;
    [SerializeField] private static TMP_Text _moneyUI;
    [SerializeField] private TMP_Text moneyUI;

    public static Command<int> ADDMONEY = new Command<int>("0001x000000090", "addmoney", "adds x ammount of money", true, (t1) =>
    {
        AddMoney(t1);
    });

    public static Command<int> SETMONEY = new Command<int>("0001x000000091", "setmoney", "sets money to x", true, (t1) =>
    {
        cash = t1;
        _moneyUI.text = "" + cash;
        Log.LogResponse("Set money to " + t1);
    });

    public static Command<bool> INFMONEY = new Command<bool>("0001x0000000092", "infmoney", "toggles infinite money", true, (t1) =>
    {
        infiniteMoney = t1;
        if (t1)
        Log.LogResponse("Infinite Money is now on");
        else 
        Log.LogResponse("Infinite Money is now off");
    });

    public static void AddMoney(int money)
    {
        cash += money;
        if (money > 0)
        {
            Log.LogResponse("Added " + money + " to your ballance");
        }
        else if (money < 0)
        {
            Log.LogResponse("Removed " + money + " from your ballance");
        }
        else
        {
            Log.LogWarning("0 was entered into the addmoney command, this means it doesn't do anything. user error?");
        }
        _moneyUI.text = "" + cash;
    }

    public static bool SpendMoney(int money)
    {
        if (infiniteMoney)
        {
            _moneyUI.text = "Infinite";
            return true;
        }
        else if (cash >= money)
        {
            cash -= money;
            _moneyUI.text = "" + cash;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Start()
    {
        moneyUI.text = "" + cash;
        _moneyUI = moneyUI;
    }

    private void Awake()
    {
        Shell.RegisterCommand(INFMONEY);
        Shell.RegisterCommand(ADDMONEY);
        Shell.RegisterCommand(SETMONEY);
    }
}
