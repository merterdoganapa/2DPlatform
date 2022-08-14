using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{
    [SerializeField] private RawImage image;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Button purchaseButton;
    
    public  ItemType itemType;
    
    public void UpdateFields(string itemName,string itemPrice,string itemDescription,ItemType itemType,Texture2D texture2D)
    {
        float maxLength = image.GetComponent<RectTransform>().sizeDelta.x;
        float aspectRatio = (float)texture2D.width / texture2D.height;

        image.rectTransform.sizeDelta = aspectRatio >= 1
            ? new Vector2(maxLength, maxLength * (1 / aspectRatio))
            : new Vector2(maxLength * aspectRatio, maxLength);
        
        image.texture = texture2D;
        name.text = itemName;
        price.text = itemPrice;
        description.text = itemDescription;
        this.itemType = itemType;
    }

    public void CheckItem(int coinAmount)
    {
        var itemPrice = Convert.ToInt16(price.text);
        if (coinAmount >= itemPrice) // player can buy this item
        {
            purchaseButton.interactable = true;
            price.color = Color.white;
        }
        else
        {
            purchaseButton.interactable = false;
            price.color = Color.red;
        }
    } 

    public void OnPurchaseButtonClick()
    {
        Shop.Instance.OnItemPurchased(this);
    }

    public int GetPrice()
    {
        var itemPrice = Convert.ToInt16(price.text);
        return itemPrice;
    }
    
}
