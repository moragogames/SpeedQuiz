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
                quizMenuText.text = "���";
                break;
            case QuizCategoty.boy:
                quizMenuText.text = "���ھ��̵�";
                break;
            case QuizCategoty.girl:
                quizMenuText.text = "���ھ��̵�";
                break;
            case QuizCategoty.singer:
                quizMenuText.text = "����";
                break;
            case QuizCategoty.come:
                quizMenuText.text = "���׸�";
                break;
            case QuizCategoty.trot:
                quizMenuText.text = "Ʈ��Ʈ����";
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
