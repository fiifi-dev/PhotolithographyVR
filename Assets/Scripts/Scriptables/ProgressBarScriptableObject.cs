using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ProgressBarScriptableObject", menuName = "ScriptableObject/ProgressBar")]
public class ProgressBarScriptableObject : ScriptableObject
{
    private GameObject ProgressObject;

    public static event Action<float> OnProgressChange;
    public static event Action OnComplete;


    public float DelaySeconds = 10;
    private int Count { set; get; }


    public void SetInstance(GameObject gameObject)
    {
        ProgressObject = gameObject;
    }

    public void Enable()
    {
        Count = 0;
        ProgressObject.SetActive(true);
    }

    public void Disable()
    {
        ProgressObject.SetActive(false);
    }


    public TextMeshProUGUI GetTextComponent()
    {
        var textComponent = ProgressObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        if (textComponent == null)
            throw new Exception("No text compnent was found. Check the order");

        return textComponent;
    }

    public Image GetProgressImage()
    {
        var imageComponent = ProgressObject.transform.GetChild(0).GetComponent<Image>();

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

    public IEnumerator Tween()
    {
        float amount = 0;
        float step = 1;

        SetText("Progressing...");

        while (true)
        {
            yield return new WaitForSeconds(step);
            amount = Count / DelaySeconds;
            Count++;
            SetProgress(amount);

            if (amount >= 1) {
                SetText("Done.");
                yield break; }

        }
    }

}
