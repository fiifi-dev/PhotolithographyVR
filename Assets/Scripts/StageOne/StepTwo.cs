using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StepTwo : MonoBehaviour
{
    public StageScriptableObject StageScriptable;
    private bool IsDone;

    private void OnEnable()
    {
        XRSocketTagInteractor.OnInside += HandleGrabEnter;
    }

    private void OnDisable()
    {
        XRSocketTagInteractor.OnInside -= HandleGrabEnter;
    }

    private void HandleGrabEnter(SelectEnterEventArgs args)
    {
        if(args.interactorObject.transform.tag != "Bowl1") return;

        if (!CanPerformStep()) return;


        // Runs if wafer is dropped in petri dish
        var stage = StageScriptable.GetCurrentStage();

        var step = stage.GetCurrentStep();
        step.IsDone = true;
        stage.ActiveStepId++;
        IsDone = true;

    }

    private bool CanPerformStep()
    {
        var stage = StageScriptable.GetCurrentStage();
        Debug.Log(stage.ActiveStepId);
        return !IsDone && stage.ActiveStepId == 2;
    }
}
