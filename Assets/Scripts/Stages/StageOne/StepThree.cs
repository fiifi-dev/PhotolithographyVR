using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StepThree : PerformStep
{
    protected override string BOWL_TAG { get => "Bowl2"; }
    public override bool CanPerformStep()
    {
        var step = StageScriptable.GetCurrentStep();
        return !step.IsDone && step.StepId == 3;
    }
}
