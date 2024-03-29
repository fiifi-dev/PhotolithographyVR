using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ProgressbarUtility
{
    public static event Action<float, bool> OnProgressChange; // value, isCompleted

    public GameObject ProgressBarGameObject { set; get; }
    public ProgressbarUtility(GameObject progressBarGameObject)
    {
        ProgressBarGameObject = progressBarGameObject;
    }



    public TextMeshProUGUI GetTextComponent()
    {
        var textComponent = ProgressBarGameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        if (textComponent == null)
            throw new Exception("No text compnent was found. Check the order");

        return textComponent;
    }

    public Image GetProgressImage()
    {
        var imageComponent = ProgressBarGameObject.transform.GetChild(0).GetComponent<Image>();

        if (imageComponent == null || imageComponent.type != Image.Type.Filled)
            throw new Exception("No image compnent was found or type is not fillled. Check the order");

        return imageComponent;
    }


    public void SetText(string text)
    {
        var textComponent = GetTextComponent();
        textComponent.text = text;

    }

    public void SetProgress(float amount)
    {
        if(amount > 1) amount = 1;

        var imageComponent = GetProgressImage();
        imageComponent.fillAmount = amount;

        OnProgressChange?.Invoke(amount, amount >= 1);
    }

    public void AttachGameObject(GameObject parentGameObject, float height)
    {
        ProgressBarGameObject.transform.position = parentGameObject.transform.position + new Vector3(0, height, 0);
    }
}
