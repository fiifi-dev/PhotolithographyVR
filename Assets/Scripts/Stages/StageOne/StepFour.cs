using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StepFour : PerformStep
{
    protected override string BOWL_TAG { get => "Bowl3"; }
    public override bool CanPerformStep()
    {
        var step = StageScriptable.GetCurrentStep();
        return !step.IsDone && step.StepId == 4;
    }
}