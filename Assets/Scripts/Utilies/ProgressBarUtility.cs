using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ProgressBarUtility
{
    private static GameObject ProgressObject;

    public static event Action<float> OnProgressChange;
    public static event Action OnComplete;
    public static event Action OnStart;
    public static bool IsEnabled = false;


    public static float DelaySeconds { get; set; }
    private static int Count { set; get; }


    public ProgressBarUtility(GameObject progressGameObject, float delaySeconds)
    {
        ProgressObject = progressGameObject;
        DelaySeconds = delaySeconds;
    }


    public static void Enable()
    {
        Count = 0;
        IsEnabled = true;
        //ProgressObject.SetActive(true);
    }

    public static void Disable()
    {
        Count = 0;
        IsEnabled = false;
        //ProgressObject.SetActive(false);
    }



    public static TextMeshProUGUI GetTextComponent()
    {
        var textComponent = ProgressObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        if (textComponent == null)
            throw new Exception("No text compnent was found. Check the order");

        return textComponent;
    }

    public static Image GetProgressImage()
    {
        var imageComponent = ProgressObject.transform.GetChild(0).GetComponent<Image>();

        if (imageComponent == null || imageComponent.type != Image.Type.Filled)
            throw new Exception("No image compnent was found or type is not fillled. Check the order");

        return imageComponent;
    }


    public static void SetText(string text)
    {
        var textComponent = GetTextComponent();
        textComponent.text = text;

    }

    public static void ResetObject()
    {
        var imageComponent = GetProgressImage();
        imageComponent.fillAmount = 0;
    }

    public static void SetProgress(float amount)
    {
        if (!IsEnabled) amount = 0;
        if (amount > 1) amount = 1;

        if (amount == 1) OnComplete?.Invoke();

        var imageComponent = GetProgressImage();
        imageComponent.fillAmount = amount;

        OnProgressChange?.Invoke(amount);
    }

    public static IEnumerator Tween()
    {

        OnStart?.Invoke();

        float amount;
        float step = 0.5f;

        while (true)
        {
            yield return new WaitForSeconds(step);
            amount = Count / DelaySeconds;
            Count++;
            SetProgress(amount);

            if (amount >= 1)
            {
                ResetObject();
                yield break;
            }

        }


    }

}
