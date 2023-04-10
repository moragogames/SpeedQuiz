using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizMenuBtn : MonoBehaviour
{
    [SerializeField] TMP_Text quizMenuText;
    [SerializeField] QuizCategoty quizCategoty;
    Button btn;

    private void Start()
    {
        quizMenuText = GetComponentInChildren<TMP_Text>();
        quizCategoty = (QuizCategoty)GetComponent<RectTransform>().GetSiblingIndex();
        btn = GetComponent<Button>();   

        btn.onClick.AddListener(OnClickedBtn);
        

        switch (quizCategoty)
        {
            case QuizCategoty.actor:
                quizMenuText.text = "배우";
                break;
            case QuizCategoty.boy:
                quizMenuText.text = "남자아이돌";
                break;
            case QuizCategoty.girl:
                quizMenuText.text = "여자아이돌";
                break;
            case QuizCategoty.singer:
                quizMenuText.text = "가수";
                break;
            case QuizCategoty.come:
                quizMenuText.text = "개그맨";
                break;
            case QuizCategoty.trot:
                quizMenuText.text = "트로트가수";
                break;
            
        }
    }
    public void OnClickedBtn()
    {

        User.Instance.quizCatagoty = quizCategoty;
        SceneManager.LoadScene("3_GameScene");
    }

}

public enum QuizCategoty
{
    actor,
    boy,
    girl,
    singer,
    come,
    trot,
}
