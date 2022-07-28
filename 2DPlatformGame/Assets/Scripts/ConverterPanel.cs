using System;
using System.Collections;
using System.Collections.Generic;
using PlatformGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConverterPanel : MonoBehaviour
{
    [SerializeField] private int minimumStepAmount;
    [SerializeField] private Button convertButton;
    [SerializeField] private Image footImage;
    [SerializeField] private Image goldImage;
    [SerializeField] private TextMeshProUGUI text;

    private void Update()
    {
        if (gameObject.activeInHierarchy && convertButton.interactable == false)
        {
            UpdateContent();
        }
    }

    private void UpdateContent(bool checkStepAmount = true)
    {
        bool canConvert = false;
        if (checkStepAmount)
        {
            canConvert = StepController.Instance.IsHaveEnoughSteps(minimumStepAmount);
        }
        convertButton.interactable = canConvert;
        UpdateColors(!canConvert);
    }

    public void Open()
    {
        UpdateContent();
        gameObject.SetActive(true);
    }

    private void UpdateColors(bool fade)
    {
        float value = fade ? .4f : 1;
        footImage.color = new Color(footImage.color.r, footImage.color.g, footImage.color.b, value);
        goldImage.color = new Color(footImage.color.r, footImage.color.g, footImage.color.b, value);
        text.color = new Color(text.color.r, text.color.g, text.color.b, value);
    }
    
    public void OnConvertButtonClick()
    {
        GameController.Instance.ConvertStepsToCoin();
        UpdateContent(false);
    }

    public void OnBackButtonClick()
    {
        gameObject.SetActive(false);
    }
    
}
