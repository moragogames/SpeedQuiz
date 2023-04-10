using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] public GameObject resultPanel;
    [SerializeField] public GameObject gameOverPanel;
    [SerializeField] public GameObject shopPanel;
    [SerializeField] public GameObject hintOnePanel;
    [SerializeField] public GameObject hintCorrectPanel;
    [SerializeField] public GameObject readyPanel;

    public TMP_Text myScoreText;
    public TMP_Text correctCount;

    private void Start()
    {
        resultPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        shopPanel.SetActive(false);
        hintOnePanel.SetActive(false);
        hintCorrectPanel.SetActive(false);
        readyPanel.SetActive(false);

    }

    private void Update()
    {
        myScoreText.text = "³ªÀÇ Á¡¼ö : " + User.Instance.myScore.ToString();
        correctCount.text = "¸ÂÃáÄûÁî°¹¼ö : " + User.Instance.correctCount.ToString();
    }

    public void SetResultPanel(bool _r)
    {
        resultPanel.SetActive(_r);
    }
    public void SetGameOverPanel(bool _g)
    {
        gameOverPanel.SetActive(_g);
    }
    public void SetShopPanel(bool _s)
    {
        shopPanel.SetActive(_s);
    }
    public void SetHintOnePanel()
    {
        hintOnePanel.SetActive(true);
    }
    public void SetHintCorrectPanel()
    {
        hintCorrectPanel.SetActive(true);
    }
    public void SetReadyPanel(bool _r)
    {
        readyPanel.SetActive(_r);
    }




}
