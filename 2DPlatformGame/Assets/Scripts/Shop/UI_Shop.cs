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

    public void OnPurchaseButtonClick()
    {
        var itemPrice = Convert.ToInt16(price.text);
        //if (!CoinController.Instance.IsHaveEnoughCoin(itemPrice)) return;
        //CoinController.Instance.DecreaseCoinAmount(itemPrice);
        Shop.Instance.OnItemPurchased(this);
    }
    
}
