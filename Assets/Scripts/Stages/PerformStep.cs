using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PerformStep : MonoBehaviour
{
    public StageScriptableObject StageScriptable;
    private ProgressBarUtility ProgressUtility;

    public bool IsDone { get; set; }
    public bool IsWorkingContainer { get; set; }
    protected virtual string BOWL_TAG { get; }
    private bool HasProgressBar { get; set; }



    private void OnEnable()
    {
        SetProgress();
        XRSocketTagInteractor.OnOutside += HandleOnObjectOutside;
        XRSocketTagInteractor.OnInside += HandleOnObjectInside;
        HandleProgressBar.OnComplete += HandleOnComplete;
    }

    private void OnDisable()
    {
        XRSocketTagInteractor.OnOutside -= HandleOnObjectOutside;
        XRSocketTagInteractor.OnInside -= HandleOnObjectInside;
        HandleProgressBar.OnComplete -= HandleOnComplete;
    }

    public virtual void HandleOnComplete()
    {
        if (!IsWorkingContainer || !CanPerformStep()) return;

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
        IsWorkingContainer = tag == BOWL_TAG;

        if (!IsWorkingContainer) return;

        EnablepProgress();
    }

    public virtual void HandleOnObjectOutside(SelectExitEventArgs args)
    {
        DisableProgress();
    }


    public virtual void SetProgress()
    {
        var parentObj = gameObject.transform.parent.parent;
        var progressObj = parentObj.GetChild(2).gameObject;
        if (progressObj == null) return;
        HasProgressBar = true;
        ProgressUtility = new ProgressBarUtility(progressObj);
    }

    public virtual void EnablepProgress()
    {
        if (!IsWorkingContainer || !HasProgressBar) return;
        ProgressUtility.Enable();
        StartCoroutine(ProgressUtility.Tween());
    }

    public virtual void DisableProgress()
    {
        if (!IsWorkingContainer || !HasProgressBar) return;
        StopCoroutine(ProgressUtility.Tween());
        ProgressUtility.Disable();
    }

    public virtual bool CanPerformStep()
    {
        var stage = StageScriptable.GetCurrentStage();
        return !IsDone && stage.ActiveStepId == 2;
    }


}