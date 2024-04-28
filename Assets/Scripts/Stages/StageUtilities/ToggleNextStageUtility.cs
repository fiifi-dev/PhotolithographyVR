using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class ToggleNextStageUtility : MonoBehaviour
{
    private bool IsVisible;
    public GameObject NextStageButton;
    public GameObject CanvasGameObject;
    public StageScriptableObject StageScriptable;



    // Update is called once per frame
    void Update()
    {
        ShowStageButton();
    }

    private void ShowStageButton()
    {
        if (CanvasGameObject == null) return;
        var currentStage = StageScriptable.GetCurrentStage();


        var raycaster = CanvasGameObject.GetComponent<TrackedDeviceGraphicRaycaster>();


        if (!IsVisible && currentStage.IsDone)
        {
            NextStageButton.SetActive(true);
            IsVisible = true;

            // make it interactive
            if (raycaster == null) CanvasGameObject.AddComponent<TrackedDeviceGraphicRaycaster>();


        }
        else if (IsVisible && !currentStage.IsDone)
        {
            NextStageButton.SetActive(false);
            IsVisible = false;
        }

        if (!currentStage.IsDone && raycaster != null)
        {
            Destroy(raycaster);
        }

    }
}
