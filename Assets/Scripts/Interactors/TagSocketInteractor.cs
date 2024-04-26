using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class TagSocketInteractor : XRSocketInteractor
{
    public string TargetTag;
    public GameObject TargetGameObject;
    public UnityEvent OnInside;
    public UnityEvent OnOutside;

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && interactable.transform.tag == TargetTag;
    }
    protected override void OnEnable()
    {
        TargetGameObject.GetComponent<XRSocketInteractor>().selectEntered.AddListener(HandleSocketEnter);
        TargetGameObject.GetComponent<XRSocketInteractor>().selectExited.AddListener(HandleSocketExit);
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        TargetGameObject.GetComponent<XRSocketInteractor>().selectEntered.RemoveListener(HandleSocketEnter);
        TargetGameObject.GetComponent<XRSocketInteractor>().selectExited.RemoveListener(HandleSocketExit);
        base.OnDisable();
    }

    private void HandleSocketExit(SelectExitEventArgs args)
    {
        OnOutside.Invoke();
    }

    private void HandleSocketEnter(SelectEnterEventArgs args)
    {
        OnInside.Invoke();
    } 
    
}
