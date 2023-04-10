using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizMenuBtn : MonoBehaviour
{
    [SerializeField] TMP_Text quizMenuText;
    [SerializeField] QuizCatagoty quizCatagoty;
    Button btn;

    private void Start()
    {
        quizMenuText = GetComponentInChildren<TMP_Text>();
        quizCatagoty = (QuizCatagoty)GetComponent<RectTransform>().GetSiblingIndex();
        btn = GetComponent<Button>();   

        btn.onClick.AddListener(OnClickedBtn);
        

        switch (quizCatagoty)
        {
            case QuizCatagoty.actor:
                quizMenuText.text = "���";
                break;
            case QuizCatagoty.drama:
                quizMenuText.text = "���";
                break;
            case QuizCatagoty.singer:
                quizMenuText.text = "����";
                break;
            case QuizCatagoty.movie:
                quizMenuText.text = "��ȭ";
                break;
            case QuizCatagoty.youTuber:
                quizMenuText.text = "��Ʃ��";
                break;
            case QuizCatagoty.soccer:
                quizMenuText.text = "�౸����";
                break;
            case QuizCatagoty.Country:
                quizMenuText.text = "����";
                break;
            case QuizCatagoty.baseball:
                quizMenuText.text = "�߱�����";
                break;
        }
    }
    public void OnClickedBtn()
    {

        User.Instance.quizCatagoty = quizCatagoty;
        SceneManager.LoadScene("3_GameScene");
    }

}

public enum QuizCatagoty
{
    actor,
    drama,
    singer,
    movie,
    youTuber,
    soccer,
    Country,
    baseball,
    count
}
