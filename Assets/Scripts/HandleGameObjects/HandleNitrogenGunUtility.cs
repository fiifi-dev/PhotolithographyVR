using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class HandleNitrogenGunUtility : MonoBehaviour
{
    public UnityEvent OnActivate;
    public UnityEvent OnDeactivate;
    public UnityEvent OnObjectHit;

    private bool HasObjectHit;
    private bool HasAlreadyInvoked;
    public string ObjectTag;

    public GameObject NitrogenGunObject;



    public ParticleSystem Particles;
    private Animator MAnimator;
    private XRGrabInteractable GrabInteractable;

    public LayerMask GunLayerMask;
    public Transform ShootSourse;
    public float MaxRaycastDistance = 10;


    // Start is called before the first frame update
    void Start()
    {
        if (NitrogenGunObject != null)
        {
            MAnimator = NitrogenGunObject.GetComponent<Animator>();

            GrabInteractable = NitrogenGunObject.GetComponent<XRGrabInteractable>();
            GrabInteractable.activated.AddListener(StartGun);
            GrabInteractable.deactivated.AddListener(StopGun);
        }

    }

    private void OnDisable()
    {
        if (GrabInteractable != null)
        {
            GrabInteractable.activated.RemoveListener(StartGun);
            GrabInteractable.deactivated.RemoveListener(StopGun);
        }

    }

    private void StopGun(DeactivateEventArgs args)
    {
        MAnimator.SetTrigger("TrRelease");
        Particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        OnDeactivate.Invoke();
        HasObjectHit = false;
        HasAlreadyInvoked = false;
    }

    private void StartGun(ActivateEventArgs args)
    {
        MAnimator.SetTrigger("TrPull");
        Particles.Play();
        OnActivate.Invoke();
        HasObjectHit = true;

    }

    void Update()
    {
        RaycastCheck();
    }

    public void RaycastCheck()
    {


        RaycastHit hit;

        var hasHit = Physics.Raycast(
            ShootSourse.position,
            ShootSourse.forward,
            out hit,
            MaxRaycastDistance,
            GunLayerMask);

        var obj = hit.transform?.gameObject;
        if (obj == null) return;

        if (obj.tag != ObjectTag) return;

        var isHitValid = hasHit && HasObjectHit;

        if (isHitValid && !HasAlreadyInvoked)
        {

            OnObjectHit.Invoke();
            HasAlreadyInvoked = true;
        }
    }
}
