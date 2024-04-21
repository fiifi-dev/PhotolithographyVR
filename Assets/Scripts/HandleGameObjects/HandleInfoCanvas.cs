using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HandleInfoCanvas : MonoBehaviour
{
    public StageScriptableObject Stage;
    public GameObject CanvasObject;
    public GameObject TextPrefab;
    private ProgressBarUtility ProgressUtility;
    // LabelComponents
    public GameObject LabelPrefab;
    public Texture CheckedTexture;
    public Texture CheckedDoneTexture;
    // Created prefabs
    private Queue<GameObject> LabelInstances = new();

    private void OnEnable()
    {
        new ProgressBarUtility(CanvasObject.transform.GetChild(0).gameObject, 10f);
    }

    private void OnDisable()
    {
        StopCoroutine(ProgressBarUtility.Tween());
    }


    private void Update()
    {
        RenderSteps();
    }


    public void RenderSteps()
    {
        // Clean created instances 
        while (LabelInstances.Count > 0)
        {
            Destroy(LabelInstances.Dequeue());
        }

        // Render new stage and steps
        var stage = Stage.GetCurrentStage();
        var pos = 10f;

        var titleObject =  AddTitleObject(stage.Title, new Vector3(50, pos, 0));
        LabelInstances.Enqueue(titleObject);

        foreach (var step in stage.Steps)
        {
            pos -= 8;
           var instance =  AddLabelObject(step, stage, new Vector3(75, pos, 0));
            LabelInstances.Enqueue(instance);
        }

    }

    GameObject AddTitleObject(string text, Vector3 pos)
    {
        // Upate text
        var titleObject = Instantiate(TextPrefab);
        var textComp = titleObject.GetComponent<TextMeshProUGUI>();
        textComp.text = text;


        // place component
        var transform = titleObject.GetComponent<RectTransform>();
        transform.localPosition = pos;

        // Add Object to Canvas parent
        titleObject.transform.SetParent(CanvasObject.transform, false);

        return titleObject;

    }




    GameObject AddLabelObject(Step step, Stage stage, Vector3 pos)
    {
        var labelObject = Instantiate(LabelPrefab);

        // Add Object to Canvas parent
        labelObject.transform.SetParent(CanvasObject.transform, false);

        // Gets text and image component
        var textObj = labelObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        var imageObj = labelObject.transform.GetChild(1).GetComponent<RawImage>();

        // Update content
        textObj.text = step.Title;
        imageObj.texture = step.IsDone ? CheckedDoneTexture : CheckedTexture;
        textObj.color = step.StepId == stage.ActiveStepId ? Color.white : Color.gray;

        // place component
        var transform = labelObject.GetComponent<Transform>();
        transform.localPosition = pos;

        return labelObject;

    }

 

}
