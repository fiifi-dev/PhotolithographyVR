using System;
using UnityEngine;

public class ToggleControllerRayUtility : MonoBehaviour
{
    public GameObject LeftObject;
    public GameObject RightObject;
    public GameObject InfoCanvasObject;

    private bool IsLeftVisible = true;
    private bool IsRightVisible = true;
    private bool IsCanvasVisible = true;

    private void OnEnable()
    {
        // Subscribe to the events when the script is enabled
        ManagerUtility.OnLeftPrimaryPressed += ToggleLeftObject;
        ManagerUtility.OnRightPrimaryPressed += ToggleRightObject;
        ManagerUtility.OnRightSecondaryPressed += ToggleCanvasObject;
    }

    private void OnDisable()
    {
        // Unsubscribe from the events when the script is disabled
        ManagerUtility.OnLeftPrimaryPressed -= ToggleLeftObject;
        ManagerUtility.OnRightPrimaryPressed -= ToggleRightObject;
        ManagerUtility.OnRightSecondaryPressed -= ToggleCanvasObject;
    }

    private void ToggleObject(ref bool isVisible, GameObject obj)
    {
        // Check if the GameObject is not null
        if (obj == null) return;

        // Toggle the active state and the visibility flag
        isVisible = !isVisible;
        obj.SetActive(isVisible);
    }

    private void ToggleLeftObject()
    {
        ToggleObject(ref IsLeftVisible, LeftObject);
    }

    private void ToggleRightObject()
    {
        ToggleObject(ref IsRightVisible, RightObject);
    }

    private void ToggleCanvasObject()
    {
        ToggleObject(ref IsCanvasVisible, InfoCanvasObject);
    }
}
