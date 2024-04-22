using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StepTwo : PerformStep
{
    protected override string BOWL_TAG { get => "Bowl1"; }
    public override bool CanPerformStep()
    {
        var step = StageScriptable.GetCurrentStep();
        return StageScriptable.ActiveStageId == 1 && !step.IsDone && step.StepId == 2;
    }
}
