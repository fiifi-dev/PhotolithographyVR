using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HideHandGrabbingObject : MonoBehaviour
{

    public GameObject LeftHandModel;
    public GameObject RightHandModel;

    private void Start()
    {
        var grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(HideGrabbingHand);
        grabInteractable.selectExited.AddListener(ShowGrappingHand);
    }
    //private void OnDisable()
    //{
    //    var grabInteractable = GetComponent<XRGrabInteractable>();
    //    grabInteractable.selectEntered.RemoveListener(HideGrabbingHand);
    //    grabInteractable.selectExited.RemoveListener(ShowGrappingHand);
    //}

    private void ShowGrappingHand(SelectExitEventArgs args)
    {
       
        if (args.interactorObject.transform.tag == "Left Hand")
        {
            LeftHandModel.SetActive(true);
        }
        else if (args.interactorObject.transform.tag == "Right Hand")
        {
            RightHandModel.SetActive(true);
        }
    }

    private void HideGrabbingHand(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.tag == "Left Hand")
        {
            LeftHandModel.SetActive(false);
        }
        else if (args.interactorObject.transform.tag == "Right Hand")
        {
            RightHandModel.SetActive(false);
        }
    }
}
