using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenUtility : MonoBehaviour
{
 IEnumerator Tween(float maxValue, float stepValue, float delay)
    {
        float count = 0;

        while (true)
        {
            yield return new WaitForSeconds(delay);
            count++;

        }
    }
}
