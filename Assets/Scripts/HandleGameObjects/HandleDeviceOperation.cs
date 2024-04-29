using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandleDeviceOperation : MonoBehaviour
{
    public UnityEvent OnValidStart;

    private bool IsObjectInDevice;
    private bool IsDeviceStart;
    private bool IsDeviceOpen;
    private bool IsOperationComplete;


    public void ToggleOpen()
    {
        IsDeviceOpen = !IsDeviceOpen;
    }

    public void ToggleStart()
    {
        IsDeviceStart = !IsDeviceStart;
    }

    public void ToggleObjectInside()
    {
        IsObjectInDevice = !IsObjectInDevice;
    }

    public void ToggleOperationComplte()
    {
        IsOperationComplete = !IsOperationComplete;
    }

    private void Update()
    {
        var isValidStart = !IsDeviceOpen && IsObjectInDevice && IsDeviceStart;
        if (isValidStart)
        {
            OnValidStart.Invoke();
            IsDeviceStart = false;
        }
    }

}
