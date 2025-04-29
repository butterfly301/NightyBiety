using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject interactTip;
    
    private Camera camera;

    private void OnEnable()
    {
        camera = Camera.main;
        interactTip.SetActive(false);
    }

    public void interactTipEnable(Transform transform)
    {
        Vector3 screenPosition = camera.WorldToScreenPoint(transform.position);
        var rectTransform = interactTip.GetComponent<RectTransform>();
        rectTransform.position = screenPosition;
        interactTip.SetActive(true);
    }

    public void interactTipDisable()
    {
        interactTip.SetActive(false);
    }
}
