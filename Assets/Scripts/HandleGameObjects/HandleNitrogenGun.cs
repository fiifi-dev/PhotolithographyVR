using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandleNitrogenGun : MonoBehaviour
{
    public ParticleSystem Particles;
    private Animator MAnimator;

    public LayerMask GunLayerMask;
    public Transform ShootSourse;
    public float MaxRaycastDistance = 10;
    private bool IsRayActivated = false;


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
        IsRayActivated = false;
        MAnimator.SetTrigger("TrRelease");
        Particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    private void StartGun(ActivateEventArgs args)
    {
        IsRayActivated = true;
        MAnimator.SetTrigger("TrPull");
        Particles.Play();

    }

    void Update()
    {
        RaycastCheck();
    }

    public void RaycastCheck()
    {
        if (!IsRayActivated) return;

        RaycastHit hit;

        var hasHit = Physics.Raycast(
            ShootSourse.position,
            ShootSourse.forward,
            out hit,
            MaxRaycastDistance,
            GunLayerMask
            );

        if (hasHit)
        {
            hit.transform.gameObject.SendMessage("HandleRaycastHit", SendMessageOptions.DontRequireReceiver);
        }

    }
}
