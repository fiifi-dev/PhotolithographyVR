using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenClose : MonoBehaviour
{
    private Animator mAnimator;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mAnimator == null) return; 

        if(Input.GetKeyDown(KeyCode.O)) {
            mAnimator.SetTrigger("TrOpen");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            mAnimator.SetTrigger("TrClose");
        }
    }
}
