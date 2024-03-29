using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HasContent : MonoBehaviour
{
    public static event Action<Collider> OnInside;
    public static event Action<Collider> OnOutside;


    private void OnTriggerEnter(Collider other)
    {
        OnInside?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnOutside?.Invoke(other);
    }
}
