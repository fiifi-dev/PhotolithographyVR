using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressbarUtility
{
    private GameObject ProgressBarGameObject { set; get; }
    public ProgressbarUtility(GameObject progressBarGameObject)
    {
        ProgressBarGameObject = progressBarGameObject;
    }

    public TextMeshProUGUI GetTextComponent()
    {
        var textComponent = ProgressBarGameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        if (textComponent == null)
            throw new System.Exception("No text compnent was found. Check the order");

        return textComponent;
    }

    public Image GetProgressImage()
    {
        var imageComponent = ProgressBarGameObject.transform.GetChild(0).GetComponent<Image>();

        if (imageComponent == null)
            throw new System.Exception("No image compnent was found. Check the order");

        return imageComponent;
    }


    public void SetText(string text)
    {
        var textComponent = GetTextComponent();
        textComponent.text = text;

    }

    public void SetFilled(float amount)
    {
        var imageComponent = GetProgressImage();
        imageComponent.fillAmount = amount;
    }

    public void AttachGameObject(GameObject parentGameObject, float height)
    {
        ProgressBarGameObject.transform.position = parentGameObject.transform.position + new Vector3(0, height, 0);
    }



}
