using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StepFour : MonoBehaviour
{
    public StageScriptableObject StageScriptable;
    private bool IsDone;
    private GameObject CollidedGameObject;


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

    private void OnTriggerEnter(Collider other)
    {
        if (!CanPerformStep()) return;

        CollidedGameObject = other.gameObject;
        other.gameObject.GetComponent<XRGrabInteractable>().selectExited.AddListener(HandleSelectExited);
    }

    private void OnTriggerExit(Collider other)
    {
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