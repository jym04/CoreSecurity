using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class FadeEffect : MonoBehaviour
{
    [Range(0.01f,10f)]
    public float fadeTime;
    public float progress;
    private Image panel_Fade;

    [SerializeField]
    public AnimationCurve fadeCurve;

    public void Awake()
    {
        panel_Fade = transform.GetComponentInChildren<Image>();
    }

    public IEnumerator FadeIn()
    {
        float currentTIme = 0.0f;
        progress = 0.0f;

        while (progress < 1)
        {
            currentTIme += Time.deltaTime;
            progress = currentTIme / fadeTime;

            var color = panel_Fade.color;
            color.a = Mathf.Lerp(0, 1, fadeCurve.Evaluate(progress));
            panel_Fade.color = color;

            yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        float currentTIme = 0.0f;
        progress = 0.0f;

        while (progress < 1)
        {
            currentTIme += Time.deltaTime;
            progress = currentTIme / fadeTime;

            var color = panel_Fade.color;
            color.a = Mathf.Lerp(1, 0, fadeCurve.Evaluate(progress));
            panel_Fade.color = color;

            yield return null;
        }
    }
}
