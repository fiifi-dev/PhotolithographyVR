using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Button button;
    public GameObject gameObject;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(startGame);
    }

    private void startGame()
    {
        gameObject.SetActive(false);
    }
}
