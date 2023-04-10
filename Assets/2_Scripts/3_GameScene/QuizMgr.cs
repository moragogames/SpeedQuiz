using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizMgr : MonoBehaviour
{
    #region �̱���
    private static QuizMgr instance;
    public static QuizMgr Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    [SerializeField] QuizData[] quizDatas;
    [SerializeField] public  QuizCanvas quizCanvas;
    [SerializeField] QuizData curQuizData;
    [SerializeField] QuizTimePanel quizTimePanel;
    [SerializeField] Popup popup;


    public TMP_Text QuestionCountText;
    public int QuestionCount;


    bool isAnswered = false;
    void Start()
    {
        Debug.Log("����Ǿߵ� ���� : " + User.Instance.quizCatagoty);
        QuestionCount = 1;
        popup.SetResultPanel(false);
        popup.SetReadyPanel(true);

        User.Instance.correctCount = 0;
        User.Instance.myScore = 0;

        SetQuizList();
    }
    private void Update()
    {
        QuestionCountText.text = QuestionCount.ToString();
    }

    public void TimeOver()
    {
        Answered(false);
        popup.gameOverPanel.SetActive(true);
    }
    public void ShowQuiz()
    {
        popup.SetReadyPanel(false);
        isAnswered = false;
        QuizRandom();
        quizCanvas.ShowQuiz(curQuizData);
    }

        List<QuizData> quizRandomList = new List<QuizData>();

    public void SetQuizList()
    {
        for (int i = 0; i < quizDatas.Length; i++)
        {
            quizRandomList.Add(quizDatas[i]);
        }
    }
    public void QuizRandom()
    {
        int rand = Random.Range(0, quizRandomList.Count);
        curQuizData = quizRandomList[rand];
        quizRandomList.RemoveAt(rand);

        if (quizRandomList.Count == 0)
        {
            SetQuizList();
        }
    }
    public void Answer(string correctWord)
    {
        if (isAnswered)
        {
            return;
        }

        bool result = false;
        
        if (curQuizData.correct == correctWord) // ������ ������
        {
            popup.SetResultPanel(true); // ��� �˾�
            quizCanvas.Answered(result); // 
            User.Instance.myScore += 100;
            User.Instance.correctCount++;
            Debug.Log("����");
            
        }
        else
        {
            Debug.Log("��");
        }
        Answered(result);

    }

    public  void Answered(bool _result)
    {
        quizCanvas.Answered(_result);
        //StartCoroutine("WaitNext");
    }

    IEnumerator WaitNext()
    {
        popup.SetResultPanel(false);
        yield return new WaitForSeconds(1f);
        //Debug.Log("�ڵ������� ����");
        QuestionCount++;
        ShowQuiz();
    }
    public void NextQuiz()
    {
        StartCoroutine("WaitNext");
    }
   
}

[System.Serializable]
public class QuizData
{
    public int QuizIndex = 0;

    public Sprite quizImage;

    public string correct;
    public string[] words;

}
