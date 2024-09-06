using System;
using UnityEngine;

[Serializable]
public struct StoreItemData
{
    public bool IsUnlocked
    {
        get { return (PlayerPrefs.GetInt(StoreKey) == 10 || UnlockFromStart); }
        private set { return; }
    }

    public ItemType Type;
    public int ItemGameID;
    public string StoreKey;
    public Sprite ItemSprite;
    public bool UnlockFromStart;
    public int ItemCost;

    public void Unlock()
    {
        PlayerPrefs.SetInt(StoreKey, 10);
    }
}

public enum ItemType
{
    Background,
    Music
}
