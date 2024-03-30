using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PerformStep : MonoBehaviour
{
    public BuildStage BuildStage;
    protected bool IsDone;
    protected bool IsTimedCorrectly;
    protected GameObject ProgressGameObject;



    void OnEnable()
    {
        XRSocketTagInteractor.OnInside += HandleItemInsideBowl;
        XRSocketTagInteractor.OnOutside += HandleItemOutsideBowl;
        HandleProgressBar.OnComplete += HandleProgressComplete;
        ProgressGameObject = GetProgressBarObject();
    }

    void HandleProgressComplete()
    {
        IsTimedCorrectly = true;
    }

    void OnDisable()
    {
        XRSocketTagInteractor.OnInside -= HandleItemInsideBowl;
        XRSocketTagInteractor.OnOutside -= HandleItemOutsideBowl;
        HandleProgressBar.OnComplete -= HandleProgressComplete;
    }

    void HandleItemOutsideBowl(SelectExitEventArgs args)
    {
        ProgressGameObject.SetActive(false);
    }


    void HandleItemInsideBowl(SelectEnterEventArgs args)
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

   public virtual bool CanPerformStep()
    {
        var stage = BuildStage.GetCurrentStage();
        return !IsDone && stage.ActiveStepId == 2 && !IsTimedCorrectly;
    }
}
