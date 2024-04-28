using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PerformStep : MonoBehaviour
{  
    public StageScriptableObject StageScriptable;

    public int StepNumber;
    public int StageNumber;
    public bool IsLastStep;


    private bool CanPerformStep()
    {
        var step = StageScriptable.GetCurrentStep();

        return StageScriptable.ActiveStageId == StageNumber && !step.IsDone && step.StepId == StepNumber;
    }

    public void HandleStep()
    {

        if (!CanPerformStep()) return;

        var step = StageScriptable.GetCurrentStep();
        step.IsDone = true;

        if(IsLastStep)
        {
            var stage = StageScriptable.GetCurrentStage();
            stage.IsDone = false;
        }
        else
        {
            StageScriptable.SetNextStep();
        }
    }
}