using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Market Items")]
public class MarketItemsData : ScriptableObject
{
    public List<MarketItem> items;

    public MarketItem GetItemByIndex(int index)
    {
        return items[index];
    }
}

[Serializable]
public class MarketItem
{
    public string name;
    public string description;
    public ItemType type;
    public string price;
    public Texture2D texture;
    public bool isPurchased;
}

