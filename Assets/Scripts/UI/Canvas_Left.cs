using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Canvas_Left : MonoBehaviour
{
    private FadeEffect fadeEffect;
    private bool isPopup;

    private RectTransform rectTransform;

    public float dotweenAnimationDuration;
    public void Awake()
    {
        fadeEffect = FindObjectOfType<FadeEffect>();
        rectTransform = transform.Find("Panel_TeamData").GetComponent<RectTransform>();

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
                LeftPopup();
            }
        }
    }

    public void LeftPopup()
    {
        rectTransform.gameObject.SetActive(true);

        Sequence seq = DOTween.Sequence();
        float movePosX = rectTransform.transform.position.x;
        rectTransform.anchoredPosition = new Vector2(0f, rectTransform.anchoredPosition.y);
        seq.Append(rectTransform.transform.DOMoveX(movePosX, dotweenAnimationDuration));
    }
}

