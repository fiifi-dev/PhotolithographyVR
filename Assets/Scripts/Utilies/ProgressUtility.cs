using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProgressUtility : MonoBehaviour
{
    public UnityEvent OnComplete;
    public GameObject ProgressObject;
    private float DelaySeconds { get; set; } = 5f;
    private static int Count { set; get; }



    private TextMeshProUGUI GetTextComponent()
    {
        var textComponent = ProgressObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        if (textComponent == null)
            throw new Exception("No text compnent was found. Check the order");

        return textComponent;
    }

    private Image GetProgressImage()
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

    private void SetProgress(float amount)
    {
        if (amount >= 1)
        {
            amount = 1;
            ResetProgress();
        }

        if (amount == 1) OnComplete?.Invoke();

        var imageComponent = GetProgressImage();
        imageComponent.fillAmount = amount;
    }

    private IEnumerator Tween()
    {
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
                ResetProgress();
                yield break;
            }

        }


    }

    private void ResetProgress()
    {
        Count = 0;
        var imageComponent = GetProgressImage();
        imageComponent.fillAmount = 0;
    }

    public void StartProgress()
    {
        Debug.Log("Start Progress");
        StartCoroutine(Tween());
    }

    public void StopProgress()
    {
        Debug.Log("Stop Progress");
        StopAllCoroutines();
        ResetProgress();
    }
}
