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
    [SerializeField] public QuizData curQuizData;
    [SerializeField] QuizTimePanel quizTimePanel;
    [SerializeField] Popup popup;

    float beforeTime;

    public TMP_Text QuestionCountText;
    public int QuestionCount;

    
    bool isAnswered = false;
   
    void Start()
    {
        Debug.Log("����Ǿߵ� ���� : " + User.Instance.quizCatagoty);
        QuestionCount = 1;
        popup.SetReadyPanel(true);
        

        User.Instance.correctCount = 0;
        User.Instance.myScore = 0;
        for (int i = 0; i < quizDatas.Length; i++)
        {
            Debug.Log(quizDatas[i].correct);
        }
        quizDatas = DataMgr.Instance.GetQuizDatas(User.Instance.quizCatagoty).ToArray();
        SetQuizList();
    }

    void InitQuiz()
    {
        popup.SetReadyPanel(false);
        popup.SetGameOverPanel(false);
        popup.SetResultPanel(false);
        popup.panelBack.SetActive(false);
        popup.hintOneObj.GetComponent<Image>().color = Color.white;
        popup.hintAllObj.GetComponent<Image>().color = Color.white;
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
    public void StartQuiz() // ���� ����
    {
        popup.isHintOneClicked = false;
        popup.isHintAllClicked = false;

        StartCoroutine("WaitQuizStart");
    }
   
    public void NextQuiz()
    {
        QuestionCount++;
        StartCoroutine("WaitNextQuiz");
       
    }

   IEnumerator WaitNextQuiz()
    {
        yield return new WaitForSeconds(0.5f);
        StartQuiz();
    }
    IEnumerator WaitQuizStart()
    {
        InitQuiz();
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

        if (quizRandomList.Count == 0) // �ѹ����� �Ѱ���
        {
            SceneManager.LoadScene("4_ClearScene");
            Debug.Log("���� ��Ǯ����");
            //SetQuizList();
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
        
        if (curQuizData.correct == correctWord) // ������ ������
        {
            SoundMgr.Instance.StopSound(SFXType.clock);
            popup.SetResultPanel(true); // ��� �˾�
            quizCanvas.Answered(result); // 
            User.Instance.myScore += 100;
            User.Instance.correctCount++;
            SoundMgr.Instance.PlaySound(SFXType.rigjt);

            //Debug.Log("����");
        }
        else
        {
            //Debug.Log("��");
            SoundMgr.Instance.PlaySound(SFXType.wrong);

        }
        //Answered(result);

    }

    public  void Answered(bool _result)
    {
        quizCanvas.Answered(_result);
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
