using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject uiShopPrefab;
    [SerializeField] private MarketItemsData marketItems;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Transform itemContent;
    private static Shop _instance;

    public static Shop Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<Shop>();
            return _instance;
        }
    }
    private void Start()
    {
        var itemCount = marketItems.items.Count;
        for (int i = itemCount - 1; i >= 0 ;i--)
        {
            var item = marketItems.GetItemByIndex(i);
            var ui_shop = Instantiate(uiShopPrefab,itemContent).GetComponent<UI_Shop>();
            ui_shop.UpdateFields(item.name,item.price,item.description,item.type,item.texture);
        }
    }

    public void OpenMarket()
    {
        gameObject.SetActive(true);
    }

    public void CloseMarket()
    {
        gameObject.SetActive(false);
    }
    
    public void OnItemPurchased(UI_Shop ui_shop)
    {
        playerData.UpdateData(ui_shop.itemType);
            
    }
    
    
}
