using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StepTwo : MonoBehaviour
{
    public BuildStage BuildStage;
    private bool IsInDish;
    public GameObject CollidedGameObject;


    private void HandleSelectExited(SelectExitEventArgs eventArgs)
    {
        // Runs if wafer is dropped in petri dish
        var stage = BuildStage.GetCurrentStage();

        if (!IsInDish || CollidedGameObject == null ||
            stage.ActiveStepId != 2 && CollidedGameObject.tag != "Wafer") return;

        var step = stage.GetCurrentStep();
        step.IsDone = true;

        stage.ActiveStepId++;
        BuildStage.GenerateStepActions();

    }


    private void OnCollisionEnter(Collision collision)
    {
        IsInDish = true;
        CollidedGameObject = collision.gameObject;
        collision.gameObject.GetComponent<XRGrabInteractable>().selectExited.AddListener(HandleSelectExited);
    }

    private void OnCollisionExit(Collision collision)
    {
        IsInDish = false;
        CollidedGameObject = null;
        collision.gameObject.GetComponent<XRGrabInteractable>().selectExited.RemoveListener(HandleSelectExited);
    }

    private void OnDestroy()
    {
        if (CollidedGameObject != null)
        {
            CollidedGameObject.GetComponent<XRGrabInteractable>().selectExited.RemoveListener(HandleSelectExited);
        }
    }


}
