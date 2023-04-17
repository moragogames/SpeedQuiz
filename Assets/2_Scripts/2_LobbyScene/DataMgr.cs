using Boomlagoon.JSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    private static DataMgr instance;
    public static DataMgr Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
       if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    #endregion

    public char[] words;
    
    public Dictionary<QuizCategoty, List<QuizData>> quizDataDic = new Dictionary<QuizCategoty, List<QuizData>> ();
    public Dictionary<QuizCategoty, QuizMenuData> quizMenuDic = new Dictionary<QuizCategoty, QuizMenuData> ();

#if UNITY_EDITOR
    public List<QuizData> quizDataList = new List<QuizData> ();
    public List<QuizMenuData> quizMenuDataList = new List<QuizMenuData> ();

#endif

    private void Start()
    {
        // ¥‹æÓµ•¿Ã≈Õ
        JSONObject wordObj = JSONObject.Parse(Resources.Load<TextAsset>("JSON/WordData").text);
        string[] wordstr = wordObj.GetString("word").Split('/');
        words = new char[wordstr.Length]; //±√±›«’¥œ¥Ÿ
        for (int i = 0; i < wordstr.Length; i++)
        {
            words[i] = char.Parse(wordstr[i]);
        }
    }

    bool initQuizData;
    void LoadQuizData()
    {
        if (initQuizData)
        {
            return;
        }
        initQuizData = true;    
        JSONArray quizArray = JSONArray.Parse(Resources.Load<TextAsset>("JSON/QuizData").text);

        for (int i = 0; i < quizArray.Length; i++)
        {
            QuizData qData = new QuizData();

            qData.idx = quizArray[i].Obj.GetString("idx");
            qData.correct = quizArray[i].Obj.GetString("correct");
            qData.quizCategoty = System.Enum.Parse<QuizCategoty>(quizArray[i].Obj.GetString("quizCategory"));
            qData.sprite = Resources.Load<Sprite>("Images/AnswerImages/" + qData.idx);

            if (!quizDataDic.ContainsKey(qData.quizCategoty))
            {
                quizDataDic.Add(qData.quizCategoty, new List<QuizData>());
            }

            quizDataDic[qData.quizCategoty].Add(qData); // ±√±›«’¥œ¥Ÿ 
#if UNITY_EDITOR
            quizDataList.Add(qData);
#endif
        }
    }
    bool initQuizMenuData;
    void LoadQuizMenuData()
    {
        if (initQuizMenuData)
        {
            return;
        }
        initQuizMenuData = true;
        JSONArray menuArray = JSONArray.Parse(Resources.Load<TextAsset>("JSON/quizMenuData").text);

        for (int i = 0; i < menuArray.Length; i++)
        {
            QuizMenuData mData = new QuizMenuData();

            mData.name = menuArray[i].Obj.GetString("name");
            mData.thum = Resources.Load<Sprite>("Images/thum/" + menuArray[i].Obj.GetString("thum"));
            mData.quizCategoty = System.Enum.Parse<QuizCategoty>(menuArray[i].Obj.GetString("quizCategory"));

            quizMenuDic.Add(mData.quizCategoty, mData);
#if UNITY_EDITOR
            quizMenuDataList.Add(mData);
#endif
        }
    }

    public List<QuizData> GetQuizDatas(QuizCategoty quizCategoty)
    {
        LoadQuizData();
        if (!quizDataDic.ContainsKey(quizCategoty))
        {
            return null;
        }
        return quizDataDic[quizCategoty];
    }
    public QuizMenuData GetQuizMenuData(QuizCategoty quizCategoty)
    {
        LoadQuizMenuData();

        if (!quizMenuDic.ContainsKey(quizCategoty))
        {
            return null;
        }
        return quizMenuDic[quizCategoty];
    }


}

[System.Serializable]
public class QuizData
{
    public string idx;
    public Sprite sprite;
    public QuizCategoty quizCategoty;
    public string correct;
    //public string[] words;
}
[System.Serializable]
public class QuizMenuData
{
    public QuizCategoty quizCategoty;
    public string name;
    public Sprite thum;

}

