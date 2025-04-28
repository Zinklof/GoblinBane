using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ZinklofDev.ConsoleV2;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private static int cash = 300;
    [SerializeField] private static bool infiniteMoney = false;
    [SerializeField] private static TMP_Text _moneyUI;
    [SerializeField] private TMP_Text moneyUI;

    [Command("Adds X money to your ballance")]
    public static void AddMoney(int money)
    {
        cash += money;
        if (money > 0)
        {
            Console.Log("Added " + money + " to your ballance", "MoneyManager");
        }
        else if (money < 0)
        {
            Console.Log("Removed " + money + " from your ballance", "MoneyManager");
        }
        else
        {
            Console.Log("0 was entered into the addmoney command, this means it doesn't do anything. user error?", "MoneyManager", "ff0000");
        }
        _moneyUI.text = "" + cash;
    }

    [Command("Sets your money to X")]
    public static void SetMoney(int t1)
    {
        cash = t1;
        _moneyUI.text = "" + cash;
        Console.Log("Set money to " + t1, "MoneyManager");
    }

    [Command("Toggles Infinite Money")]
    public static void InfMoney()
    {
        infiniteMoney = !infiniteMoney;
        Console.Log("InfMoney was toggled " + infiniteMoney, "MoneyManager");

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
        cash = 300;
        moneyUI.text = "" + cash;
        _moneyUI = moneyUI;
    }
}
