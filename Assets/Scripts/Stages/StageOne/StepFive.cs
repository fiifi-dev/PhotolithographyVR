using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StepFive : MonoBehaviour, IProgressBar
{
    public StageScriptableObject StageScriptable;
    private bool HasProgressBar { get; set; } = true;
    private bool HasHit;
    private bool IsDone;
    private bool IsStepFive { get; set; }

    private void OnEnable()
    {
        ProgressBarUtility.OnComplete += HandleOnComplete;
        HandleNitrogenGun.OnHitAcivate += HandleHitActivate;
    }

    private void OnDisable()
    {
        ProgressBarUtility.OnComplete -= HandleOnComplete;
        HandleNitrogenGun.OnHitAcivate -= HandleHitActivate;
    }


    void HandleHitActivate(GameObject obj, bool isHit)
    {

        if (isHit && obj.tag == "Wafer")
        {
            if (!IsStepFive) IsStepFive = true;

            if (!HasHit)
            {
                EnablepProgress();
                HasHit = true;
            }
        }
        else
        {
            if (HasHit) DisableProgress();
        }
    }

    public void HandleOnComplete()
    {
        // Runs if wafer is dropped in petri dish
        var stage = StageScriptable.GetCurrentStage();

        if (!CanPerformStep()) return;
        var step = stage.GetCurrentStep();
        step.IsDone = true;
        IsDone = true;

        Debug.Log("Step: " + step.StepId + " IsDone: " + IsDone);
    }

    private bool CanPerformStep()
    {
        var stage = StageScriptable.GetCurrentStage();
        return IsStepFive && !IsDone && stage.ActiveStepId == 5;
    }


    public void EnablepProgress()
    {
        if (!HasProgressBar) return;
        StartCoroutine(ProgressBarUtility.Tween());
    }

    public void DisableProgress()
    {
        if (!HasProgressBar) return;
        StopCoroutine(ProgressBarUtility.Tween());
        IsStepFive = false;
    }
}
