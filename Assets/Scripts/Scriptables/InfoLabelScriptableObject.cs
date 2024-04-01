using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "InfoLabelScriptableObject", menuName = "ScriptableObject/InfoLabel")]
public class InfoLabelScriptableObject : ScriptableObject
{
    public Texture CheckedTexture;
    public Texture CheckedDoneTexture;
    public GameObject Prefab;

    private TextMeshProUGUI GetTextComponent(GameObject labelObject)
    {
        var obj = labelObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if (obj == null) throw new Exception("Not a valid text component");
        return obj;
    }

    private RawImage GetImageComponent(GameObject labelObject)
    {
        var obj = labelObject.transform.GetChild(1).GetComponent<RawImage>();
        if (obj == null) throw new Exception("Not a valid image component");
        return obj;
    }

    private void SetText(GameObject labelObject, string text)
    {
        var comp = GetTextComponent(labelObject);
        comp.text = text;
    }


    private void SetImage(GameObject labelObject, bool isDone)
    {
        var imageComp = GetImageComponent(labelObject);
        if (isDone)
            imageComp.texture = CheckedDoneTexture;
        else
            imageComp.texture = CheckedTexture;
    }


    private void Attach(GameObject labelObject, GameObject baseGameObject, Vector3 pos)
    {
        labelObject.transform.SetParent(baseGameObject.transform, false);

        // Optional: Add and configure a RectTransform component
        RectTransform rectTransform = baseGameObject.GetComponent<RectTransform>();
        rectTransform.localPosition = pos;
    }


    public GameObject CreateLabelInstance(string text, bool isDone, GameObject canvasObject, Vector3 pos)
    {
        var instance = Instantiate(Prefab);

        SetText(instance, text);
        SetImage(instance, isDone);
        Attach(instance, canvasObject, pos);

        return instance;
    }


}
