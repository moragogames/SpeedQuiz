using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITop : MonoBehaviour
{
    [SerializeField] Popup popup;
    [SerializeField] QuizTimePanel quizTimePanel;
    
    //[SerializeField] GameObject UIShop;
    //[SerializeField] GameObject BackBtn;

    [SerializeField] TMP_Text questionCountText;
    [SerializeField] TMP_Text coinCountText;

    private void Start()
    {
        
    }

    private void Update()
    {
        coinCountText.text = User.Instance.coinCount.ToString();
    }

    public void SetQuestionCount(int _q)
    {
        questionCountText.text = _q.ToString();
    }



   
    public void SetUIShop()
    {
        popup.SetShopPanel(true);
    }
    public void SetBackBtn()
    {
        SceneManager.LoadScene("2_LobbyScene");
    }

}
