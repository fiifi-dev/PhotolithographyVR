using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class HandleProgressBar : MonoBehaviour
{
    public static event Action<float> OnProgressChange;
    public static event Action OnComplete;
    public string ProgressText = "100%";
    public float DelaySeconds = 10;


    private void OnEnable()
    {
        SetText(ProgressText);
        SetProgress(0);
        StartCoroutine(Tween());
    }

    private void OnDisable()
    {
        SetText(ProgressText);
        SetProgress(0);
        StopCoroutine(Tween());
    }

    public TextMeshProUGUI GetTextComponent()
    {
        var textComponent = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        if (textComponent == null)
            throw new Exception("No text compnent was found. Check the order");

        return textComponent;
    }

    public Image GetProgressImage()
    {
        var imageComponent = gameObject.transform.GetChild(0).GetComponent<Image>();

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
        if (amount > 1) amount = 1;

        if (amount == 1) OnComplete?.Invoke();

        var imageComponent = GetProgressImage();
        imageComponent.fillAmount = amount;

        OnProgressChange?.Invoke(amount);
    }

    IEnumerator Tween()
    {
        float count = 0;
        float amount = 0;
        float step = 1;

        while (true)
        {
            yield return new WaitForSeconds(step);
            amount = count / DelaySeconds;
            count++;
            SetProgress(amount);

            if (amount >= 1) yield break;

        }
    }

}
