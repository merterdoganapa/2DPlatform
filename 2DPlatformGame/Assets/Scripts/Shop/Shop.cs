using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject uiShopPrefab;
    [SerializeField] private MarketItemsData marketItems;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Transform itemContent;
    
    private static Shop _instance;
    public List<UI_Shop> items = new List<UI_Shop>();
    public static Shop Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<Shop>();
            return _instance;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            CoinController.Instance.IncreaseCoinAmount(1000);
        }
    }

    private void Start()
    {
        var itemCount = marketItems.items.Count;
        for (int i = itemCount - 1; i >= 0 ;i--)
        {
            var item = marketItems.GetItemByIndex(i);
            var ui_shop = Instantiate(uiShopPrefab,itemContent).GetComponent<UI_Shop>();
            items.Add(ui_shop);
            ui_shop.UpdateFields(item.name,item.price,item.description,item.type,item.texture);
        }
    }
    
    private void OnEnable()
    {
        CoinController.Instance.OnCoinChanged += CheckItems;
    }

    private void OnDisable()
    {
        CoinController.Instance.OnCoinChanged -= CheckItems;
    }

    private void CheckItems(int currentCoinAmount) 
    {
        foreach (var item in items)
        {
            item.CheckItem(currentCoinAmount);
        }
    }

    public void OpenMarket()
    {
        panel.SetActive(true);
    }

    public void CloseMarket()
    {
        panel.SetActive(false);
    }
    
    public void OnItemPurchased(UI_Shop ui_shop)
    {
        playerData.UpdateData(ui_shop.itemType);
        CoinController.Instance.DecreaseCoinAmount(ui_shop.GetPrice());
    }
    
    
}
