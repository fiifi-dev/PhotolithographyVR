using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ItemPickup : MonoBehaviour
{

    public BuildStep buildStep;
    private void OnEnable()
    {
        GetComponent<XRGrabInteractable>().onSelectEntered.AddListener(handleSelectEntered);
        GetComponent<XRGrabInteractable>().onSelectExited.AddListener(handleSelectExited);
    }

    private void OnDisable()
    {
        GetComponent<XRGrabInteractable>().onSelectEntered.RemoveListener(handleSelectEntered);
        GetComponent<XRGrabInteractable>().onSelectExited.RemoveListener(handleSelectExited);
    }

    private void handleSelectEntered(XRBaseInteractor interactor)
    {
        Debug.Log(gameObject.name + " was picked up by " + interactor.gameObject.name);
        // Add additional logic for when the item is picked up
        buildStep.generateStepActions();
        buildStep.increaseActionId();
        
    }

    private void handleSelectExited(XRBaseInteractor interactor)
    {
        Debug.Log(gameObject.name + " was dropped by " + interactor.gameObject.name);
        // Add additional logic for when the item is dropped
    }
}
