using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class PickedDroppedUtility : MonoBehaviour
{
    public UnityEvent OnDropped;
    public UnityEvent OnPicked;

    private void OnEnable()
    {
        GetComponent<XRGrabInteractable>().selectEntered.AddListener(HandleSelectEntered);
        GetComponent<XRGrabInteractable>().selectExited.AddListener(HandleSelectExited);
    }

    private void OnDisable()
    {
        GetComponent<XRGrabInteractable>().selectEntered.RemoveListener(HandleSelectEntered);
        GetComponent<XRGrabInteractable>().selectExited.RemoveListener(HandleSelectExited);
    }

    private void HandleSelectExited(SelectExitEventArgs args)
    {
        OnDropped.Invoke();
    }

    private void HandleSelectEntered(SelectEnterEventArgs args)
    {
        OnPicked.Invoke();
    }
}
