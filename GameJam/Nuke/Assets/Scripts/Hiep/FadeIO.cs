using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIO : MonoBehaviour
{

    IEnumerator FadeInCoroutine;
    public void FadeIn(float duration, Color32 targetColor)
    {
        if (FadeInCoroutine != null) StopCoroutine(FadeInCoroutine);
        FadeInCoroutine = null;


    }
}
