using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Button Button;
    public GameObject GameObject;
    // Start is called before the first frame update
    void Start()
    {
        Button.onClick.AddListener(HandleButtonClick);
    }

    private void HandleButtonClick()
    {
        GameObject.SetActive(false);
    }
}
