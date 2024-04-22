using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public enum RaycastStatusEnum
{
    Ideal,
    Active,
    Inactive
}

public class HandleNitrogenGun : MonoBehaviour
{
    public static Action<GameObject, bool, bool, RaycastStatusEnum> OnHitAcivate; // hitObject, isHit, isHitPrev, and RaycastStatus
    public static Action OnPickup;
    public bool HasHit;


    public ParticleSystem Particles;
    private Animator MAnimator;
    private XRGrabInteractable GrabInteractable;

    public LayerMask GunLayerMask;
    public Transform ShootSourse;
    public float MaxRaycastDistance = 10;
    private bool IsHitPrev = false;
    private RaycastStatusEnum RaycastStatus = RaycastStatusEnum.Ideal;




    // Start is called before the first frame update
    void Start()
    {
        MAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        GrabInteractable = GetComponent<XRGrabInteractable>();
        GrabInteractable.activated.AddListener(StartGun);
        GrabInteractable.deactivated.AddListener(StopGun);
    }

    private void OnDisable()
    {
        GrabInteractable.activated.RemoveListener(StartGun);
        GrabInteractable.deactivated.RemoveListener(StopGun);
    }

    private void StopGun(DeactivateEventArgs args)
    {
        RaycastStatus = RaycastStatusEnum.Inactive;
        MAnimator.SetTrigger("TrRelease");
        Particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    private void StartGun(ActivateEventArgs args)
    {
        RaycastStatus = RaycastStatusEnum.Active;
        MAnimator.SetTrigger("TrPull");
        Particles.Play();

    }

    void Update()
    {
        RaycastCheck();
    }

    public void RaycastCheck()
    {
        if (RaycastStatus == RaycastStatusEnum.Ideal) return;


        RaycastHit hit;

        var hasHit = Physics.Raycast(
            ShootSourse.position,
            ShootSourse.forward,
            out hit,
            MaxRaycastDistance,
            GunLayerMask
            );

        var obj = hit.transform.gameObject;
        OnHitAcivate?.Invoke(obj, hasHit, IsHitPrev, RaycastStatus);
        IsHitPrev = hasHit;
    }
}
