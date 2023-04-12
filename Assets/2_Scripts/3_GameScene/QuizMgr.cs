using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizMgr : MonoBehaviour
{
    #region 싱글톤
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

    [SerializeField] QuizData[] quizDatas;//(급수정)
    [SerializeField] public  QuizCanvas quizCanvas;
    [SerializeField] QuizData curQuizData;//(급수정)
    [SerializeField] QuizTimePanel quizTimePanel;
    [SerializeField] Popup popup;

    float beforeTime;

    public TMP_Text QuestionCountText;
    public int QuestionCount;


    bool isAnswered = false;
   
    void Start()
    {
        Debug.Log("제출되야될 퀴즈 : " + User.Instance.quizCatagoty);
        QuestionCount = 1;
        popup.SetResultPanel(false);
        popup.SetReadyPanel(true);

        User.Instance.correctCount = 0;
        User.Instance.myScore = 0;
        quizDatas = DataMgr.Instance.GetQuizDatas(User.Instance.quizCatagoty).ToArray();
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
    public void StartQuiz() // 퀴즈 시작
    {
     
        StartCoroutine("WaitQuizStart");
    }
   
    public void NextQuiz()
    {
        QuestionCount++;
        StartCoroutine("WaitNextQuiz");
       
    }

    public void ReStartQuiz()
    {
        User.Instance.correctCount = 0;
        User.Instance.myScore = 0;
        StartQuiz();
    }
  
   IEnumerator WaitNextQuiz()
    {
        yield return new WaitForSeconds(0.5f);
        StartQuiz();
    }
    IEnumerator WaitQuizStart()
    {
        popup.SetReadyPanel(false);
        popup.SetGameOverPanel(false);
        popup.SetResultPanel(false);
        isAnswered = false;
        yield return new WaitForSeconds(0.5f);
        QuizRandom();
        quizTimePanel.QuizTimerStart();
        quizCanvas.CreatQuiz(curQuizData);
    }

    List<QuizData> quizRandomList = new List<QuizData>();

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
    public void SetQuizList()
    {
        for (int i = 0; i < quizDatas.Length; i++)
        {
            quizRandomList.Add(quizDatas[i]);
        }
    }
    public void Answer(string correctWord)
    {
        if (isAnswered)
        {
            return;
        }

        bool result = false;
        
        if (curQuizData.correct == correctWord) // 정답이 같으면
        {
            popup.SetResultPanel(true); // 결과 팝업
            quizCanvas.Answered(result); // 
            User.Instance.myScore += 100;
            User.Instance.correctCount++;
            SoundMgr.Instance.PlaySound(SFXType.rigjt);

            Debug.Log("정답");
        }
        else
        {
            Debug.Log("땡");
            SoundMgr.Instance.PlaySound(SFXType.wrong);

        }
        //Answered(result);

    }

    public  void Answered(bool _result)
    {
        quizCanvas.Answered(_result);
    }
    public void SetGameStop()
    {
        if (Time.timeScale > 0)
        {
            beforeTime = Time.timeScale;
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = beforeTime;
        }
    }

   
}

//[System.Serializable]
//public class QuizDatas
//{
//    public int QuizIndex = 0;

//    public Sprite quizImage;

//    public string correct;
//    public string[] words;

//}
