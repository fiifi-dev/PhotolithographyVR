using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StepOne : MonoBehaviour
{
    public BuildStage BuildStage;

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

    private void HandleSelectEntered(SelectEnterEventArgs eventArgs)
    {
        var stage = BuildStage.GetCurrentStage();


        if (stage.ActiveStepId == 1 && gameObject.tag == "Wafer")
        {
            Debug.Log(gameObject.name + " was picked up by " + gameObject.name);

            var step = stage.GetCurrentStep();
            step.IsDone = true;

            stage.ActiveStepId++;
            BuildStage.GenerateStepActions();
        }
    }
}
