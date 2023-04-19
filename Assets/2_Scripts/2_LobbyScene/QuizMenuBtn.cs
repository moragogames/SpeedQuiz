using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizMenuBtn : MonoBehaviour
{
    [SerializeField] TMP_Text quizMenuText;
    [SerializeField] Image thumImg;
    [SerializeField] QuizCategoty quizCategoty;

    private void Start()
    {
        quizMenuText = GetComponentInChildren<TMP_Text>();
        quizCategoty = (QuizCategoty)GetComponent<RectTransform>().GetSiblingIndex();

        QuizMenuData qMdata = DataMgr.Instance.GetQuizMenuData(quizCategoty);

        thumImg.sprite = qMdata.thum;
        quizMenuText.text = qMdata.name;

        GetComponent<Button>().onClick.AddListener(OnClickedBtn);
        
    }
    public void OnClickedBtn()
    {
        SoundMgr.Instance.PlaySound(SFXType.click);
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
