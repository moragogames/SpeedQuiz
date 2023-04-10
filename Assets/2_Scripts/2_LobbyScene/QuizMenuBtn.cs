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
                quizMenuText.text = "배우";
                break;
            case QuizCatagoty.drama:
                quizMenuText.text = "드라마";
                break;
            case QuizCatagoty.singer:
                quizMenuText.text = "가수";
                break;
            case QuizCatagoty.movie:
                quizMenuText.text = "영화";
                break;
            case QuizCatagoty.youTuber:
                quizMenuText.text = "유튜버";
                break;
            case QuizCatagoty.soccer:
                quizMenuText.text = "축구선수";
                break;
            case QuizCatagoty.Country:
                quizMenuText.text = "나라";
                break;
            case QuizCatagoty.baseball:
                quizMenuText.text = "야구선수";
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
