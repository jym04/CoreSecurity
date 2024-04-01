using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LocationMarker : MonoBehaviour
{
    public Icon_Location markerIcon;
    public Vector3 centerPos;
    public float fadeTime;

    private void Awake()
    {
        centerPos = transform.position;
    }
    public void SetMarkerIcon(Icon_Location icon)
    {
        markerIcon = icon;
    }
    public void ActiveMarkerIcon()
    {
        markerIcon.gameObject.SetActive(true);

        var seq = DOTween.Sequence();

        foreach(var image in markerIcon.images)
        {
            seq.Append(image.DOFade(1f, fadeTime));
        }
    }
    public void DeactiveMarkerIcon()
    {
        var seq = DOTween.Sequence();

        foreach (var image in markerIcon.images)
        {
            seq.Append(image.DOFade(0f, fadeTime)).OnComplete(() => { markerIcon.gameObject.SetActive(false); });
        }
    }
}
