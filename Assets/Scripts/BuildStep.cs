using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.XR.Interaction.Toolkit;

public class BuildStep : MonoBehaviour
{
    private class Action
    {
        public int actionId;
        public string title;
        public bool isDone;

        public Action(int actionId, string title)
        {
            this.actionId = actionId;
            this.title = title;
        }


        public void setIsDone()
        {
            isDone = true;
        }

    }

    private class Step
    {
        public int stepId;
        public string title;
        public int activeActionid = 1;
        public bool isActive;
        public List<Action> actionList = new();

        public Step(int stepId, string title)
        {
            this.stepId = stepId;
            this.title = title;
        }

        public void addAction(Action action)
        {
            actionList.Add(action);
        }


        public Action getAction()
        {
            return actionList[activeActionid];
        }



        public void setActiveActionId(int actionId)
        {
            activeActionid = actionId;
        }

    }

    private int activeStepId = 1;
    public GameObject canvasGameObject;
    private List<Step> stepList = new();
    public Texture checkedTexture;
    public Texture checkedDoneTexture;


    // Start is called before the first frame update
    void Start()
    {
        initiateSteps();

        generateStepActions();
    }



    private void setActiveStep(int stepId)
    {
        activeStepId = stepId;
    }


    private void initiateSteps()
    {
        var step1 = new Step(1, "Stage 1: Clean Wafer");

        step1.addAction(new Action(5, "Dry wafer with nitrogen gun"));
        step1.addAction(new Action(4, "Place wafer in DI Water petri dish"));
        step1.addAction(new Action(3, "Place wafer in IPA petri dish"));
        step1.addAction(new Action(2, "Place wafer in acetone petri dish"));
        step1.addAction(new Action(1, "Pick up wafer"));



        stepList.Add(step1);
    }



    private void createImageObject(string name, GameObject parentGameObject, Vector3 pos, bool isDone)
    {
        // Create the RawImage GameObject
        GameObject rawImageGameObject = new GameObject(name);
        rawImageGameObject.transform.SetParent(parentGameObject.transform, false);

        // Add a RawImage component
        RawImage rawImage = rawImageGameObject.AddComponent<RawImage>();

        // Set the texture
        rawImage.texture = isDone ? checkedDoneTexture : checkedTexture;

        // Optional: Add and configure a RectTransform
        RectTransform rectTransform = rawImageGameObject.GetComponent<RectTransform>();
        rectTransform.localPosition = pos;
        rectTransform.sizeDelta = new Vector2(8, 8); // Set the size
    }

    private void createTextObject(GameObject parentGameObject, string name, int size, Vector3 pos, string text, Color color)
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

    private Step findStep(int stepId)
    {
        for (int i = 0; i < stepList.Count; i++)
        {
            var step = stepList[i];

            if (step.stepId == stepId) return step;

        }

        return null;
    }


    public void generateStepActions()
    {
        var step = findStep(activeStepId);
        var stepPos = 50;

        createTextObject(
             canvasGameObject,
             "StepText" + step.stepId,
             10,
             new Vector3(50, stepPos, 0),
             step.title,
             Color.white
         );


        foreach (var action in step.actionList)
        {

            createImageObject(
                "ActionImage" + action.actionId,
                canvasGameObject,
                new Vector3(-45, stepPos - (action.actionId * 10), 0),
                action.isDone
            );

            createTextObject(
                canvasGameObject,
                "ActionText" + action.actionId,
                6,
                new Vector3(60, stepPos - (action.actionId * 10), 0),
                action.title,
                step.activeActionid == action.actionId ? Color.white : new Color(0.7f, 0.7f, 0.7f, 1f)
            );

        }
    }

    public void increaseActionId()
    {

        var step = findStep(activeStepId);

        if (step.activeActionid > step.actionList.Count) return;
        var action = step.getAction();
        action.setIsDone();

        step.activeActionid++;
    }

}
