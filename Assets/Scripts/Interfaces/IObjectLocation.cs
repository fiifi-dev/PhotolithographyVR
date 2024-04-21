using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public interface IObjectLocation
{
    void HandleOnObjectInside(SelectEnterEventArgs args);

    void HandleOnObjectOutside(SelectExitEventArgs args);
}
