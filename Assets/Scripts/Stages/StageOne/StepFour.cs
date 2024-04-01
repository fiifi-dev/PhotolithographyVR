using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StepFour : MonoBehaviour
{
    public StageScriptableObject StageScriptable;
    private bool IsDone;

    private void OnEnable()
    {
        XRSocketTagInteractor.OnOutside += HandleSelectExited;
    }

    private void OnDisable()
    {
        XRSocketTagInteractor.OnOutside -= HandleSelectExited;
    }


    private void HandleSelectExited(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform.tag != "Bowl3") return;

        if (IsDone) return;

        // Runs if wafer is dropped in petri dish
        var stage = StageScriptable.GetCurrentStage();

        if (!CanPerformStep()) return;

        var step = stage.GetCurrentStep();
        step.IsDone = true;

        stage.ActiveStepId++;
        IsDone = true;

    }

    private bool CanPerformStep()
    {
        var stage = StageScriptable.GetCurrentStage();
        return !IsDone && stage.ActiveStepId == 4;
    }
}