using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Canvas_Right : MonoBehaviour
{
    private FadeEffect fadeEffect;
    private bool isPopup;

    private RectTransform rectTransform;
    private float originPosX;

    public float dotweenAnimationDuration;
    public void Awake()
    {
        fadeEffect = FindObjectOfType<FadeEffect>();
        rectTransform = transform.Find("Panel_LiveTraining").GetComponent<RectTransform>();

        originPosX = rectTransform.anchoredPosition.x;
        rectTransform.gameObject.SetActive(false);
    }

    void Update()
    {
        IsPopup();
    }

    private void IsPopup()
    {
        if (fadeEffect.progress >= 1)
        {
            if (!isPopup)
            {
                isPopup = true;
                RightPopup();
            }
        }
    }

    public void RightPopup()
    {
        rectTransform.gameObject.SetActive(true);

        Sequence seq = DOTween.Sequence();
        float movePosX = rectTransform.transform.position.x;
        rectTransform.anchoredPosition = new Vector2(Math.Abs(originPosX), rectTransform.anchoredPosition.y);
        seq.Append(rectTransform.transform.DOMoveX(movePosX, dotweenAnimationDuration));
    }
}
