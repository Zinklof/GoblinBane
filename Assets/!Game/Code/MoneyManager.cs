using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private int cash;
    [SerializeField] private TMP_Text moneyUI;

    public bool SpendMoney(int money)
    {
        if (cash >= money)
        {
            cash -= money;
            moneyUI.text = "" + cash;
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
    }


}
