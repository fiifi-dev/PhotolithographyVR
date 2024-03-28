using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class NitrogenGun : MonoBehaviour
{
    public ParticleSystem Particles;
    private Animator MAnimator;
    // Start is called before the first frame update
    void Start()
    {

        MAnimator = GetComponent<Animator>();

        var grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(StartGun);
        grabInteractable.deactivated.AddListener(StopGun);
    }

    private void StopGun(DeactivateEventArgs args)
    {
        Particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        MAnimator.SetTrigger("TrRelease");
    }

    private void StartGun(ActivateEventArgs args)
    {
        MAnimator.SetTrigger("TrPull");
        Particles.Play();

    }
}
