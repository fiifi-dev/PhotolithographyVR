using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StepTwo : MonoBehaviour
{
    public BuildStage BuildStage;
    private bool IsDone;
    public GameObject ProgressGameObject;
    private ProgressbarUtility ProgressUtility;

    private void Start()
    {
        ProgressGameObject = Instantiate(ProgressGameObject);
        ProgressUtility = new ProgressbarUtility(ProgressGameObject);
    }



    private void OnEnable()
    {
        XRSocketTagInteractor.OnInside += HandleSelectExited;
        XRSocketTagInteractor.OnOutside += HandleSelectEnter;
        ProgressbarUtility.OnProgressChange += HandleProgressChange;
    }

    private void HandleSelectEnter(SelectExitEventArgs args)
    {
        ProgressGameObject.SetActive(false);
    }

    private void HandleProgressChange(float value, bool isCompleted)
    {
        if (isCompleted)
        {
            ProgressUtility.SetText("Completed");
        }
    }

    private void OnDisable()
    {
        XRSocketTagInteractor.OnInside -= HandleSelectExited;
        XRSocketTagInteractor.OnOutside -= HandleSelectEnter;
        ProgressbarUtility.OnProgressChange -= HandleProgressChange;
    }

    IEnumerator Tween()
    {
        float count = 0;
        float delay = 1;

        while (true)
        {
            yield return new WaitForSeconds(delay);
            count += 0.1f;
            ProgressUtility.SetProgress(count);

            if (count == 1) yield break;

        }
    }




    private void HandleSelectExited(SelectEnterEventArgs args)
    {
        ProgressUtility.ProgressBarGameObject.SetActive(true);
        ProgressUtility.AttachGameObject(gameObject, 0.05f);
        ProgressUtility.SetProgress(0);
        ProgressUtility.SetText("0h:5m:0s");
        StartCoroutine(Tween());

        if (!CanPerformStep()) return;

        // Runs if wafer is dropped in petri dish
        var stage = BuildStage.GetCurrentStage();

        var step = stage.GetCurrentStep();
        step.IsDone = true;

        stage.ActiveStepId++;
        BuildStage.GenerateStepActions();
        IsDone = true;

    }

    private bool CanPerformStep()
    {
        var stage = BuildStage.GetCurrentStage();
        return !IsDone && stage.ActiveStepId == 2;
    }
}
