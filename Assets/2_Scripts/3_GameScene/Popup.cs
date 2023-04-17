using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Popup : MonoBehaviour
{
    [SerializeField] public  GameObject panelBack;
    [SerializeField] public GameObject resultPanel;
    [SerializeField] public GameObject gameOverPanel;
    [SerializeField] public GameObject shopPanel;
    [SerializeField] public GameObject hintOnePanel;
    [SerializeField] public GameObject hintAllPanel;
    [SerializeField] public GameObject readyPanel;
    [SerializeField] public GameObject adPanel;

    [SerializeField] QuizTimePanel quizTimePanel;
    [SerializeField] QuizCanvas quizCanvas;
    [SerializeField] UITop uitop;


    [Header("myScore")]
    public TMP_Text myScoreText;
    public TMP_Text correctCount;

    [Header("hint")]
    [SerializeField] public TMP_Text oneHint;
    [SerializeField] public TMP_Text allHint;


    private void Start()
    {
        InitPopup();
    }

    private void Update()
    {
        myScoreText.text = "나의 점수 : " + User.Instance.myScore.ToString();
        correctCount.text = "맞춘퀴즈갯수 : " + User.Instance.correctCount.ToString();
    }
    
    public void InitPopup()
    {
        resultPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        shopPanel.SetActive(false);
        readyPanel.SetActive(false);

        panelBack.SetActive(false);

        hintOnePanel.SetActive(false);
        hintAllPanel.SetActive(false);
        adPanel.SetActive(false);
        
    }

    public void SetResultPanel(bool _r)
    {
        panelBack.SetActive(true);
        resultPanel.SetActive(_r);
    }
    public void SetGameOverPanel(bool _g)
    {
        panelBack.SetActive(true);
        gameOverPanel.SetActive(_g);
    }
    public void SetShopPanel(bool _s)
    {
        panelBack.SetActive(true);
        shopPanel.SetActive(_s);
        quizTimePanel.PopUpTimeStop(true);

    }

    public bool isHintOneClicked;
    public void SetHintOnePanel()
    {
        if (isHintOneClicked)
        {
            return;
        }
        if (User.Instance.coinCount > 0)
        {
            User.Instance.coinCount -= 1;

            isHintOneClicked = true;

            SoundMgr.Instance.PlaySound(SFXType.menu);
            panelBack.SetActive(true);
            hintOnePanel.SetActive(true);
            quizTimePanel.PopUpTimeStop(true);
            oneHint.text = quizCanvas.correctArr[0].ToString();
            SaveMgr.SetSaveInt("coinCount", User.Instance.coinCount); // 세이브
            Debug.Log("세이브완료");
        }
        else
        {
            SetADPanel();
            Debug.Log("코인이부족");
            // 코인부족
        }
    }
    public bool isHintAllClicked;
    public void SetHintAllPanel()
    {
        if (isHintAllClicked)
        {
            return;
        }


        if (User.Instance.coinCount >= 3)
        {
            User.Instance.coinCount -= 3;

            isHintAllClicked = true;
            SoundMgr.Instance.PlaySound(SFXType.menu);
            panelBack.SetActive(true);
            hintAllPanel.SetActive(true);
            quizTimePanel.PopUpTimeStop(true);
            
            string correct = new string(quizCanvas.correctArr); // char string 변환
            allHint.text = correct.ToString();



        }
        else
        {
            SetADPanel();
            Debug.Log("코인이부족");
        }
    }
    public void SetReadyPanel(bool _r)
    {
        panelBack.SetActive(true);
        readyPanel.SetActive(_r);
    }

    public void HomeBtn()
    {
        SceneManager.LoadScene("2_LobbyScene");
    }

    public void OnClickCencel()
    {
        InitPopup();
        quizTimePanel.PopUpTimeStop(false);

    }

    public void SetADPanel()
    {
        SoundMgr.Instance.PlaySound(SFXType.menu);
        panelBack.SetActive(true);
        quizTimePanel.PopUpTimeStop(true);
        adPanel.SetActive(true);

    }

}
