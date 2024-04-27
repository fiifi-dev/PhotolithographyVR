using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEventAction : MonoBehaviour
{
    public StageScriptableObject StageScriptable;


    public void SetNextStage()
    {
        StageScriptable.SetNextStage();
    }
}
