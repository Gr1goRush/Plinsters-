using System;
using UnityEngine;

public static class MoneyHelper
{
    public static Action<int> OnMoneyValueChanged;
    private static string key = "PlMoney";

    public static int Money
    {
        get
        {
            return PlayerPrefs.GetInt(key);
        }
        private set
        {
            PlayerPrefs.SetInt(key, value);
            OnMoneyValueChanged?.Invoke(value);
        }
    }

    public static void AddMoney(int value)
    {
        Money += value;
    }

    public static void RemoveMoney(int value)
    {
        Money -= value;
    }

    public static bool TryPurchase(int Cost)
    {
        if (Money < Cost) return false;

        Money -= Cost;
        return true;
    }
}
