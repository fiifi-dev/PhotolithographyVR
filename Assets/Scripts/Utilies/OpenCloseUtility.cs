using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class OpenCloseUtility : MonoBehaviour
{
    private XRSimpleInteractable SimpleInteractable;
    private Animator MAnimator;
    private bool IsOpen;

    public GameObject TriggerGameObject; // Object interacted with to open object
    public UnityEvent OnOpen;
    public UnityEvent OnClose;


    void Start()
    {
        MAnimator = GetComponent<Animator>();
        SimpleInteractable = TriggerGameObject.GetComponent<XRSimpleInteractable>();

        if (SimpleInteractable != null) SimpleInteractable.selectEntered.AddListener(HandleOpen);
    }

    private void OnDisable()
    {
        SimpleInteractable.selectEntered.RemoveListener(HandleOpen);
    }

    private void HandleOpen(SelectEnterEventArgs arg)
    {
        if (!IsOpen)
        {
            MAnimator.SetTrigger("TrOpen");
            IsOpen = true;
            OnOpen.Invoke();
        }
        else
        {
            MAnimator.SetTrigger("TrClose");
            IsOpen = false;
            OnClose.Invoke();
        }
    }
}
