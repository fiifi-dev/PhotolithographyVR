using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketTagInteractor : XRSocketInteractor
{
    public static event Action<SelectEnterEventArgs> OnInside;
    public static event Action<SelectExitEventArgs> OnOutside;

    public string TargetTag;


    public override bool CanHover(IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && interactable.transform.tag == TargetTag;
    }

    protected override void OnEnable()
    {
        GetComponent<XRSocketInteractor>().selectEntered.AddListener(HandleSocketEnter);
        GetComponent<XRSocketInteractor>().selectExited.AddListener(HandleSocketExit);
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        GetComponent<XRSocketInteractor>().selectEntered.RemoveListener(HandleSocketEnter);
        GetComponent<XRSocketInteractor>().selectExited.RemoveListener(HandleSocketExit);
        base.OnDisable();
    }

    private void HandleSocketExit(SelectExitEventArgs args)
    {
        OnOutside?.Invoke(args);
    }

    private void HandleSocketEnter(SelectEnterEventArgs args)
    {
        OnInside?.Invoke(args);
    }
}
