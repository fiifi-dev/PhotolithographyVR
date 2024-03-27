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



    private void HandleSelectExited(SelectExitEventArgs eventArgs)
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

    private void OnTriggerEnter(Collider other)
    {
        if (!CanPerformStep()) return;

        IsInDish = true;
        CollidedGameObject = other.gameObject;
        other.gameObject.GetComponent<XRGrabInteractable>().selectExited.AddListener(HandleSelectExited);
    }

    private void OnTriggerExit(Collider other)
    {
        IsInDish = false;
        CollidedGameObject = null;
        other.gameObject.GetComponent<XRGrabInteractable>().selectExited.RemoveListener(HandleSelectExited);
    }

    private void OnDestroy()
    {
        if (CollidedGameObject != null)
        {
            CollidedGameObject.GetComponent<XRGrabInteractable>().selectExited.RemoveListener(HandleSelectExited);
        }
    }


}
