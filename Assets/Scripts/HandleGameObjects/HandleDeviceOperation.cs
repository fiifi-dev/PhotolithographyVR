using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandleDeviceOperation : MonoBehaviour
{
    public UnityEvent OnDeviceOpen;
    public UnityEvent OnDeviceClose;
    public UnityEvent OnDeviceStart;
    public UnityEvent OnDeviceEnd;
    public UnityEvent OnOperationComplete;

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
        var isValidComplete = !IsDeviceOpen && IsObjectInDevice && IsDeviceStart && IsOperationComplete;

        if (isValidComplete)
        {
            OnOperationComplete.Invoke();
            IsDeviceStart = false;
            IsOperationComplete = false;
        }
    }

}
