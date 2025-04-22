using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroManager : MonoBehaviour
{

    public TMP_Text gamePlayerIntroduction;
    float gamePlayerIntroductionShowTime = 0;
    bool startShowgamePlayerIntroduction = false;

    public TMP_Text gamePlayerIntroduction2;
    float gamePlayerIntroductionShowTime2 = 0;
    bool startShowgamePlayerIntroduction2 = false;
    public static IntroManager instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        gamePlayerIntroduction = GameObject.Find("gamePlayerIntroduction").GetComponent<TMP_Text>();
    }
    /// <summary>
    /// 出现新手指引
    /// </summary>
    public void ShowStartMessage()
    {
        gamePlayerIntroduction.gameObject.SetActive(true);
        startShowgamePlayerIntroduction = true;
    }

    public void ShowSecondStartMessage()
    {
        gamePlayerIntroduction2.gameObject.SetActive(true);
        startShowgamePlayerIntroduction2 = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (startShowgamePlayerIntroduction)
        {
            gamePlayerIntroductionShowTime += Time.deltaTime;
            if(gamePlayerIntroductionShowTime > 8f)
            {
                startShowgamePlayerIntroduction = false;
                gamePlayerIntroduction.gameObject.SetActive(false);
            }
        }
        if (startShowgamePlayerIntroduction2)
        {
            gamePlayerIntroductionShowTime2 += Time.deltaTime;
            if (gamePlayerIntroductionShowTime2 > 8f)
            {
                startShowgamePlayerIntroduction2 = false;
                gamePlayerIntroduction2.gameObject.SetActive(false);
            }
        }
    }
}
