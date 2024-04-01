using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "StageScriptableObject", menuName = "ScriptableObject/Stage")]
public class StageScriptableObject : ScriptableObject
{
    private int ActiveStageId = 1;
    private List<Stage> Stages = new();

    StageScriptableObject()
    {
        var stage = new Stage(1, "Stage 1: Clean Wafer");

        stage.AddStep(new Step(1, "Pick up wafer"));
        stage.AddStep(new Step(2, "Place wafer in acetone petri dish"));
        stage.AddStep(new Step(3, "Place wafer in IPA petri dish"));
        stage.AddStep(new Step(4, "Place wafer in DI Water petri dish"));
        stage.AddStep(new Step(5, "Dry wafer with nitrogen gun"));

        Stages.Add(stage);
    }




    public void AddStage(Stage stage)
    {
        Stages.Add(stage);
    }

    public List<Stage> GetStages()
    {
        return Stages;
    }


    public void RemoveStage(Stage stage)
    {
        Stages.Remove(stage);
    }

    public Stage GetCurrentStage()
    {
        return Stages.Find(x => x.StageId == ActiveStageId);
    }

    public void SetNextStage()
    {
        if (ActiveStageId >= Stages.Count) return;
        ActiveStageId++;
    }




}
