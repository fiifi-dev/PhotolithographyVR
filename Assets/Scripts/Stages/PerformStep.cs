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
    private bool HasProgressBar { get; set; } = true;



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

        // Runs if wafer is dropped in petri 
        var step = StageScriptable.GetCurrentStep();
        step.IsDone = true;
        StageScriptable.SetNextStep();
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
        ProgressBarUtility.Disable();
        StopCoroutine(ProgressBarUtility.Tween());

    }

    public virtual bool CanPerformStep()
    {
        var step = StageScriptable.GetCurrentStep();
       
        return StageScriptable.ActiveStageId == 1 && !step.IsDone && step.StepId == 2;
    }


}