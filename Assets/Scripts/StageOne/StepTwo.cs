using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StepTwo : MonoBehaviour
{
    public BuildStage BuildStage;
    private bool IsDone;
    private bool IsTimedCorrectly;
    private GameObject ProgressGameObject;

    private void OnEnable()
    {
        XRSocketTagInteractor.OnInside += HandleGrabEnter;
        XRSocketTagInteractor.OnOutside += HandleGrabExit;
        HandleProgressBar.OnComplete += HandleProgressComplete;
        ProgressGameObject = GetProgressBarObject();
    }

    private void HandleProgressComplete()
    {
       IsTimedCorrectly = true;
    }

    private void OnDisable()
    {
        XRSocketTagInteractor.OnInside -= HandleGrabEnter;
        XRSocketTagInteractor.OnOutside -= HandleGrabExit;
        HandleProgressBar.OnComplete -= HandleProgressComplete;
    }

    private void HandleGrabExit(SelectExitEventArgs args)
    {
        ProgressGameObject.SetActive(false);
    }


    private void HandleGrabEnter(SelectEnterEventArgs args)
    {
        ProgressGameObject.SetActive(true);
        if (!CanPerformStep()) return;


        // Runs if wafer is dropped in petri dish
        var stage = BuildStage.GetCurrentStage();

        var step = stage.GetCurrentStep();
        step.IsDone = true;

        stage.ActiveStepId++;
        BuildStage.GenerateStepActions();
        IsDone = true;

    }

    GameObject GetProgressBarObject()
    {
        return gameObject.transform.parent.parent.GetChild(2).gameObject;
    }

    private bool CanPerformStep()
    {
        var stage = BuildStage.GetCurrentStage();
        return !IsDone && stage.ActiveStepId == 2 && !IsTimedCorrectly;
    }
}
