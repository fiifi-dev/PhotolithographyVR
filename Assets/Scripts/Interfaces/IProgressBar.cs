using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProgressBar
{
    void EnablepProgress();

    void DisableProgress();

    void HandleOnComplete();
}
