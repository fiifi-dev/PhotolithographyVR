using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StepFive : MonoBehaviour, IProgressBar
{
    public StageScriptableObject StageScriptable;
    private bool HasProgressBar { get; set; } = true;
    private bool HasHit;
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
        if (!CanPerformStep()) return;
        var step = StageScriptable.GetCurrentStep();
        step.IsDone = true;
    }

    private bool CanPerformStep()
    {
        var step = StageScriptable.GetCurrentStep();
        return IsStepFive && !step.IsDone && step.StepId == 4;
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
