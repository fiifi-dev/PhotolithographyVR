using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PhotolithographyVR;

public class BuildStage : MonoBehaviour
{
    private int ActiveStageId { get; set; }
    public GameObject CanvasGameObject;
    private List<Stage> Stages = new();
    public Texture CheckedTexture;
    public Texture CheckedDoneTexture;


    // Start is called before the first frame update
    void Start()
    {
        ActiveStageId = 1;
        InitiateStages();
        GenerateStepActions();
    }



    private void InitiateStages()
    {
        var stage1 = new Stage(1, "Stage 1: Clean Wafer");

        stage1.AddStep(new Step(5, "Dry wafer with nitrogen gun"));
        stage1.AddStep(new Step(4, "Place wafer in DI Water petri dish"));
        stage1.AddStep(new Step(3, "Place wafer in IPA petri dish"));
        stage1.AddStep(new Step(2, "Place wafer in acetone petri dish"));
        stage1.AddStep(new Step(1, "Pick up wafer"));



        Stages.Add(stage1);
    }



    private void CreateImageObject(string name, GameObject parentGameObject, Vector3 pos, bool isDone)
    {
        // Create the RawImage GameObject
        GameObject rawImageGameObject = new GameObject(name);
        rawImageGameObject.transform.SetParent(parentGameObject.transform, false);

        // Add a RawImage component
        RawImage rawImage = rawImageGameObject.AddComponent<RawImage>();

        // Set the texture
        rawImage.texture = isDone ? CheckedDoneTexture : CheckedTexture;

        // Optional: Add and configure a RectTransform
        RectTransform rectTransform = rawImageGameObject.GetComponent<RectTransform>();
        rectTransform.localPosition = pos;
        rectTransform.sizeDelta = new Vector2(8, 8); // Set the size
    }

    private void CreateTextObject(GameObject parentGameObject, string name, int size, Vector3 pos, string text, Color color)
    {
        // Create a new TextMeshPro Text GameObject
        var textGameObject = new GameObject(name);
        textGameObject.transform.SetParent(parentGameObject.transform, false);

        // Add a TextMeshProUGUI component to the GameObject
        var tmpText = textGameObject.AddComponent<TextMeshProUGUI>();

        // Set the text properties
        tmpText.text = text;
        tmpText.fontSize = size;
        tmpText.alignment = TextAlignmentOptions.Left;
        tmpText.color = color;

        // Optional: Add and configure a RectTransform component
        RectTransform rectTransform = textGameObject.GetComponent<RectTransform>();
        rectTransform.localPosition = pos;
    }

    private Stage FindStage(int stageId)
    {
        var stage = Stages.Find(x => x.StageId == stageId);
        return stage;
    }


    public void GenerateStepActions()
    {
        var stage = FindStage(ActiveStageId);
        var stagePos = 50;

        CreateTextObject(
             CanvasGameObject,
             "StageText" + stage.StageId,
             10,
             new Vector3(50, stagePos, 0),
             stage.Title,
             Color.white
         );


        foreach (var step in stage.Steps)
        {

            CreateImageObject(
                "StepImage" + step.StepId,
                CanvasGameObject,
                new Vector3(-45, stagePos - (step.StepId * 10), 0),
                step.IsDone
            );

            CreateTextObject(
                CanvasGameObject,
                "StepText" + step.StepId,
                6,
                new Vector3(60, stagePos - (step.StepId * 10), 0),
                step.Title,
                stage.ActiveStepId == step.StepId ? Color.white : new Color(0.6f, 0.6f, 0.6f, 1f)
            );

        }
    }

    public void IncreaseStepId()
    {

        var stage = FindStage(ActiveStageId);

        if (stage.ActiveStepId > stage.Steps.Count) return;
        var step = stage.GetCurrentStep();
        step.IsDone = true;
        stage.ActiveStepId++;
    }


    public Stage GetCurrentStage()
    {
        return Stages.Find(x => x.StageId == ActiveStageId);
    }

}
