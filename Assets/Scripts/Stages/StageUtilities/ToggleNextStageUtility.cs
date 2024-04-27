using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleNextStageUtility : MonoBehaviour
{
    private bool IsVisible;
    public GameObject NextStageButton;
    public StageScriptableObject StageScriptable;



    // Update is called once per frame
    void Update()
    {
        ShowStageButton();
    }

    private void ShowStageButton()
    {
        var currentStage = StageScriptable.GetCurrentStage();

        if (!IsVisible && currentStage.IsDone)
        {
            Debug.Log("Done");
            NextStageButton.SetActive(true);
            IsVisible = true;
        }
        else if (IsVisible && !currentStage.IsDone)
        {
            NextStageButton.SetActive(false);
            IsVisible = false;
        }
    }
}
