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
        HandleProgressBar.OnComplete += HandleOnComplete;
    }

    private void HandleOnComplete()
    {
        // Runs if wafer is dropped in petri dish
        var stage = StageScriptable.GetCurrentStage();

        var step = stage.GetCurrentStep();
        step.IsDone = true;
        stage.ActiveStepId++;
        IsDone = true;
    }

    private void OnDisable()
    {
        XRSocketTagInteractor.OnOutside -= HandleGrabEnter;
        XRSocketTagInteractor.OnInside -= HandleGrabExit;
        HandleProgressBar.OnComplete -= HandleOnComplete;
    }

    private void HandleGrabExit(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.tag != "Bowl1") return;
        EnablepProgress();
    }

    private void EnablepProgress()
    {
        ProgressBarScriptable.Enable();
        StartCoroutine(ProgressBarScriptable.Tween());
    }

    private void HandleGrabEnter(SelectExitEventArgs args)
    {
        DisableProgress();

        if (args.interactorObject.transform.tag != "Bowl1") return;

        if (!CanPerformStep()) return;


    }

    private void DisableProgress()
    {
        StopCoroutine(ProgressBarScriptable.Tween());
        ProgressBarScriptable.Disable();
    }

    private bool CanPerformStep()
    {
        var stage = StageScriptable.GetCurrentStage();
        return !IsDone && stage.ActiveStepId == 2;
    }
}
