using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUtility : MonoBehaviour
{
    public Button Button;
    // Start is called before the first frame update
    void Start()
    {
        Button.onClick.AddListener(HandleClick);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(HandleClick);
    }

    private void HandleClick()
    {
        #if UNITY_STANDALONE
                Application.Quit();
        #endif
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
