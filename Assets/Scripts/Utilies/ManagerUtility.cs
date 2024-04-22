using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerUtility : MonoBehaviour
{
    public StageScriptableObject StageScriptable;
    void CleanUpResources()
    {
        StageScriptable.ResetStages();
    }

    private void OnApplicationQuit()
    {
        CleanUpResources();
    }
}
