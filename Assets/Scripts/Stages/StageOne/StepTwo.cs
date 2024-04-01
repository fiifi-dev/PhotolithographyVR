using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StepTwo : MonoBehaviour
{
    public StageScriptableObject StageScriptable;
    public ProgressBarScriptableObject ProgressBarScriptable;

    private bool IsDone;

    private void OnEnable()
    {
        ProgressBarScriptable.SetInstance(gameObject.transform.parent.parent.GetChild(2).gameObject);
        XRSocketTagInteractor.OnOutside += HandleGrabEnter;
        XRSocketTagInteractor.OnInside += HandleGrabExit;
    }

    private void OnDisable()
    {
        XRSocketTagInteractor.OnOutside -= HandleGrabEnter;
        XRSocketTagInteractor.OnInside -= HandleGrabExit;
    }

    private void HandleGrabExit(SelectEnterEventArgs args)
    {
        ProgressBarScriptable.Enable();
        StartCoroutine(ProgressBarScriptable.Tween());
    }

    private void HandleGrabEnter(SelectExitEventArgs args)
    {
        StopCoroutine(ProgressBarScriptable.Tween());
        ProgressBarScriptable.Disable();

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
        return !IsDone && stage.ActiveStepId == 2;
    }
}
