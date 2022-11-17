using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelAnimation : MonoBehaviour
{
    void OnEnable()
    {
        transform.localScale = Vector3.zero;
        ShowWindow();
    }

    void ShowWindow()
    {
        transform.DOScale(1f, 1f).SetEase(Ease.OutBounce);
    }
}
