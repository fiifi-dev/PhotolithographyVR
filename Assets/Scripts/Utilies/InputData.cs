using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InputData : MonoBehaviour
{
    public InputDevice RightController;
    public InputDevice LeftController;


    void Update()
    {
        if (!RightController.isValid || !LeftController.isValid)
            InitializeInputDevices();
    }
    private void InitializeInputDevices()
    {

        if (!RightController.isValid)
            InitializeInputDevice(
                InputDeviceCharacteristics.Controller | 
                InputDeviceCharacteristics.Right, ref RightController);

        if (!LeftController.isValid)
            InitializeInputDevice(
                InputDeviceCharacteristics.Controller | 
                InputDeviceCharacteristics.Left, ref LeftController);
        
    }

    private void InitializeInputDevice(InputDeviceCharacteristics inputCharacteristics, ref InputDevice inputDevice)
    {
        List<InputDevice> devices = new List<InputDevice>();
        //Call InputDevices to see if it can find any devices with the characteristics we're looking for
        InputDevices.GetDevicesWithCharacteristics(inputCharacteristics, devices);

        //Our hands might not be active and so they will not be generated from the search.
        //We check if any devices are found here to avoid errors.
        if (devices.Count > 0)
        {
            inputDevice = devices[0];
        }
    }
}
