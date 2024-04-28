using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StepOne : MonoBehaviour
{
    public StageScriptableObject StageScriptable;

    private bool CanPerformStep()
    {
        var step = StageScriptable.GetCurrentStep();

        return StageScriptable.ActiveStageId == 1 && !step.IsDone && step.StepId == 1;
    }

    public void HandleAction()
    {

        if (!CanPerformStep()) return;

        var step = StageScriptable.GetCurrentStep();
        step.IsDone = true;

        StageScriptable.SetNextStep();
    }
}
