using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StepOne : MonoBehaviour
{
    public BuildStage BuildStage;
    private bool IsDone;

    private void OnEnable()
    {
        GetComponent<XRGrabInteractable>().selectEntered.AddListener(HandleSelectEntered);
        //GetComponent<XRGrabInteractable>().selectExited.AddListener(HandleSelectExited);
    }
    private void OnDisable()
    {
        GetComponent<XRGrabInteractable>().selectEntered.RemoveListener(HandleSelectEntered);
        //GetComponent<XRGrabInteractable>().selectExited.RemoveListener(HandleSelectExited);
    }

    //private void HandleSelectExited(SelectExitEventArgs eventArgs)
    //{
    //    Debug.Log(gameObject.name + " was dropped by " + gameObject.name);
    //    // Add additional logic for when the item is dropped
    //}


    private bool CanPerformStep()
    {
        var stage = BuildStage.GetCurrentStage();
        return stage.ActiveStepId == 1 && gameObject.tag == "Wafer";
    }

    private void HandleSelectEntered(SelectEnterEventArgs eventArgs)
    {

        if (!CanPerformStep()) return;

        var stage = BuildStage.GetCurrentStage();
        var step = stage.GetCurrentStep();
        step.IsDone = true;

        stage.ActiveStepId++;
        BuildStage.GenerateStepActions();
        IsDone = true;

    }
}
