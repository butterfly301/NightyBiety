using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private string sceneToLoad;
    public void ClickStart()
    {
        canvasGroup.alpha = 0f;
        // 使用DOTween进行渐变
        canvasGroup.DOFade(1f, fadeDuration)
            .OnComplete(() => SceneManager.LoadScene(sceneToLoad));
    }

    public void ClickExit()
    {
        Application.Quit();
    }
}
