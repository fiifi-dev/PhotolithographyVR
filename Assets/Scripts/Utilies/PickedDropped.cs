using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PickedDropped : MonoBehaviour
{
    public static event Action<SelectExitEventArgs> OnDropped;
    public static event Action<SelectEnterEventArgs> OnPicked;

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
        OnDropped?.Invoke(args);
    }

    private void HandleSelectEntered(SelectEnterEventArgs args)
    {
        OnPicked?.Invoke(args);
    }
}
