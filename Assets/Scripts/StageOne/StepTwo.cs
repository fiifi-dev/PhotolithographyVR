using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StepTwo : MonoBehaviour
{
    public BuildStage BuildStage;
    private bool IsInDish;
    private bool IsDone;
    private GameObject CollidedGameObject;

    private void OnEnable()
    {
        XRSocketTagInteractor.OnInside += HandleSelectExited;
    }

    private void OnDisable()
    {
        XRSocketTagInteractor.OnInside += HandleSelectExited;
    }


    private void HandleSelectExited(SelectEnterEventArgs eventArgs)
    {
        if (!CanPerformStep()) return;

        // Runs if wafer is dropped in petri dish
        var stage = BuildStage.GetCurrentStage();

        var step = stage.GetCurrentStep();
        step.IsDone = true;

        stage.ActiveStepId++;
        BuildStage.GenerateStepActions();
        IsDone = true;

    }

    private bool CanPerformStep()
    {
        var stage = BuildStage.GetCurrentStage();
        return !IsDone && stage.ActiveStepId == 2;
    }
}
