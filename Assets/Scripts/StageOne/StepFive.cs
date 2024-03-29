using PhotolithographyVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepFive : MonoBehaviour
{
    private BuildStage BuildStage;
    private bool IsDone;

     void Start()
    {
        BuildStage = GameObject.FindWithTag("InstructionCanvas").GetComponent<BuildStage>();
    }


    void HandleRaycastHit()
    {
        if (IsDone) return;

        // Runs if wafer is dropped in petri dish
        var stage = BuildStage.GetCurrentStage();

        if (!CanPerformStep()) return;

        var step = stage.GetCurrentStep();
        step.IsDone = true;

        stage.ActiveStepId++;
        BuildStage.GenerateStepActions();
        IsDone = true;
    }

    private bool CanPerformStep()
    {
        var stage = BuildStage.GetCurrentStage();
        return !IsDone && stage.ActiveStepId == 5;
    }
}
