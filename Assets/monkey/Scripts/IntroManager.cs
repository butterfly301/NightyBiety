using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroManager : MonoBehaviour
{

    public GameObject INTRO1;
    public TMP_Text gamePlayerIntroduction;
    float gamePlayerIntroductionShowTime = 0;
    bool startShowgamePlayerIntroduction = false;

    public GameObject INTRO2;
    public TMP_Text gamePlayerIntroduction2;
    float gamePlayerIntroductionShowTime2 = 0;
    bool startShowgamePlayerIntroduction2 = false;
    public static IntroManager instance;
    private void Awake()
    {
        instance = this;
        gamePlayerIntroductionShowTime2 = gamePlayerIntroductionShowTime = 0;
    }
   
    /// <summary>
    /// ��������ָ��
    /// </summary>
    public void ShowStartMessage()
    {
       // gamePlayerIntroduction.gameObject.SetActive(true);
        INTRO1.SetActive(true);
        startShowgamePlayerIntroduction = true;
    }

    public void ShowSecondStartMessage()
    {
        INTRO2.SetActive(true);
       // gamePlayerIntroduction2.gameObject.SetActive(true);
        startShowgamePlayerIntroduction2 = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (startShowgamePlayerIntroduction)
        {
            gamePlayerIntroductionShowTime += Time.deltaTime;
            if(gamePlayerIntroductionShowTime > 4f)
            {
                startShowgamePlayerIntroduction = false;
                INTRO1.SetActive(false);
            }
        }
        if (startShowgamePlayerIntroduction2)
        {
            gamePlayerIntroductionShowTime2 += Time.deltaTime;
            if (gamePlayerIntroductionShowTime2 > 4f)
            {
                startShowgamePlayerIntroduction2 = false;
                INTRO2.SetActive(false);
            }
        }
    }
}
