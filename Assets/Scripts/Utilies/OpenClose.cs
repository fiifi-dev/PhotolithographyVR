using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenClose : MonoBehaviour
{
    private Animator MAnimator;
    // Start is called before the first frame update
    void Start()
    {
        MAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MAnimator == null) return; 

        if(Input.GetKeyDown(KeyCode.O)) {
            MAnimator.SetTrigger("TrOpen");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            MAnimator.SetTrigger("TrClose");
        }
    }
}
