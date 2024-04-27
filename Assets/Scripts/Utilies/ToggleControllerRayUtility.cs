using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleControllerRayUtility : MonoBehaviour
{
    public GameObject LeftObject;
    public GameObject RightObject;

    private bool IsLeftVisible;
    private bool IsRightVisible;

    private void OnEnable()
    {
        ManagerUtility.OnLeftPrimaryPressed += ToggleLeftObject;
        ManagerUtility.OnRightPrimaryPressed += ToggleRightObject;
    }

    private void OnDisable()
    {
        ManagerUtility.OnLeftPrimaryPressed -= ToggleLeftObject;
        ManagerUtility.OnRightPrimaryPressed -= ToggleRightObject;
    }

    public void ToggleLeftObject()
    {
        if(LeftObject == null) return;  

        if (IsLeftVisible)
        {
            LeftObject.SetActive(false);
            IsLeftVisible = false;
        }
        else
        {
            LeftObject.SetActive(true);
            IsLeftVisible = true;
        }
    }

    public void ToggleRightObject()
    {
        if(RightObject == null) return;

        if (IsRightVisible)
        {
            RightObject.SetActive(false);
            IsRightVisible = false;
        }
        else
        {
            RightObject.SetActive(true);
            IsRightVisible = true;
        }
    }


}
