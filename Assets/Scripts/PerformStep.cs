using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PerformStep : MonoBehaviour
{
    public BuildStage BuildStage;
    private bool IsDone;
    private bool IsTimedCorrectly;
    private GameObject ProgressGameObject;

    private void OnEnable()
    {
        XRSocketTagInteractor.OnInside += HandleItemInsideBowl;
        XRSocketTagInteractor.OnOutside += HandleItemOutsideBowl;
        HandleProgressBar.OnComplete += HandleProgressComplete;
        ProgressGameObject = GetProgressBarObject();
    }

    private void HandleProgressComplete()
    {
        IsTimedCorrectly = true;
    }

    private void OnDisable()
    {
        XRSocketTagInteractor.OnInside -= HandleItemInsideBowl;
        XRSocketTagInteractor.OnOutside -= HandleItemOutsideBowl;
        HandleProgressBar.OnComplete -= HandleProgressComplete;
    }

    private void HandleItemOutsideBowl(SelectExitEventArgs args)
    {
        ProgressGameObject.SetActive(false);
    }


    private void HandleItemInsideBowl(SelectEnterEventArgs args)
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
