using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "StageScriptableObject", menuName = "ScriptableObject/Stage")]
public class StageScriptableObject : ScriptableObject
{
    private int ActiveStageId = 1;
    private List<Stage> Stages = new();

    private Dictionary<string, List<string>> InitialStages = new() {
        { "Clean Wafer", new List<string>{
            "Pick up wafer",
            "Place wafer in acetone petri dish",
            "Place wafer in IPA petri dish",
            "Place wafer in DI Water petri dish",
            "Dry wafer with nitrogen gun"}
        },
        { "Application of photoresist", new List<string>{
            "Prebake wafer",
            "Apply photoresist to wafer",
            "Spread resist on wafer with spincoater",
            "Postbake the wafer"}
        }
    };

    StageScriptableObject()
    {
        int i = 1, j;

        foreach (var stageKeyPair in InitialStages)
        {
            var stage = new Stage(1, $"Stage {i}: {stageKeyPair.Key}");
            j = 1;

            foreach (var stepStr in stageKeyPair.Value)
            {
                stage.AddStep(new Step(j, stepStr));
                j++;
            }

            Stages.Add(stage);
            i++;

        }
        //var stage = new Stage(1, "Stage 1: Clean Wafer");

        //stage.AddStep(new Step(1, "Pick up wafer"));
        //stage.AddStep(new Step(2, "Place wafer in acetone petri dish"));
        //stage.AddStep(new Step(3, "Place wafer in IPA petri dish"));
        //stage.AddStep(new Step(4, "Place wafer in DI Water petri dish"));
        //stage.AddStep(new Step(5, "Dry wafer with nitrogen gun"));

        //Stages.Add(stage);
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
