using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PerformStep : MonoBehaviour, IProgressBar, IObjectLocation
{
    public StageScriptableObject StageScriptable;

    public bool IsDone { get; set; }
    public bool IsWorkingStep { get; set; }
    protected virtual string BOWL_TAG { get; }
    private bool HasProgressBar { get; set; }



    private void OnEnable()
    {
        XRSocketTagInteractor.OnOutside += HandleOnObjectOutside;
        XRSocketTagInteractor.OnInside += HandleOnObjectInside;
        ProgressBarUtility.OnComplete += HandleOnComplete;
    }

    private void OnDisable()
    {
        XRSocketTagInteractor.OnOutside -= HandleOnObjectOutside;
        XRSocketTagInteractor.OnInside -= HandleOnObjectInside;
        ProgressBarUtility.OnComplete -= HandleOnComplete;
    }

    public virtual void HandleOnComplete()
    {
        if (!IsWorkingStep || !CanPerformStep()) return;

        // Runs if wafer is dropped in petri dish
        var stage = StageScriptable.GetCurrentStage();

        var step = stage.GetCurrentStep();
        step.IsDone = true;
        stage.ActiveStepId++;
        IsDone = true;
    }


    public virtual void HandleOnObjectInside(SelectEnterEventArgs args)
    {
        var tag = args.interactorObject.transform.tag;
        IsWorkingStep = tag == BOWL_TAG;

        if (!IsWorkingStep) return;

        EnablepProgress();
    }

    public virtual void HandleOnObjectOutside(SelectExitEventArgs args)
    {
        DisableProgress();
    }


    public virtual void EnablepProgress()
    {
        if (!IsWorkingStep) return;
        ProgressBarUtility.Enable();
        StartCoroutine(ProgressBarUtility.Tween());
    }

    public virtual void DisableProgress()
    {
        if (!IsWorkingStep || !HasProgressBar) return;
        StopCoroutine(ProgressBarUtility.Tween());
        ProgressBarUtility.Disable();
    }

    public virtual bool CanPerformStep()
    {
        var stage = StageScriptable.GetCurrentStage();
        return !IsDone && stage.ActiveStepId == 2;
    }


}