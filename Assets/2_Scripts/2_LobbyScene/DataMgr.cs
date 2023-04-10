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

    public Dictionary<QuizCategoty, List<QuizData>> quizDataDic = new Dictionary<QuizCategoty, List<QuizData>> ();
    public Dictionary<QuizCategoty, QuizMenuData> quizMenuDic = new Dictionary<QuizCategoty, QuizMenuData> ();

#if UNITY_EDITOR
    public List<QuizData> quizDataList = new List<QuizData> ();
    public List<QuizMenuData> quizMenuDataList = new List<QuizMenuData> ();

    public List<WordData> wordDataList = new List<WordData> ();
#endif

    private void Start()
    {

        //JSONArray jArray = JSONArray.Parse(Resources.Load<TextAsset>("JSON/QuizData").text);

        TextAsset quizAsset = Resources.Load<TextAsset>("JSON/QuizData");
        JSONObject quizObj = JSONObject.Parse(quizAsset.text);


        JSONArray quizArray = quizObj["QuizData"].Array;


        for (int i = 0; i < quizArray.Length; i++)
        {
            QuizData qData = new QuizData();

            qData.idx = quizArray[i].Obj.GetString("idx");
            qData.correct = quizArray[i].Obj.GetString("correct");
            qData.quizCategoty = System.Enum.Parse<QuizCategoty>(quizArray[i].Obj.GetString("quizCategory"));
            qData.sprite = Resources.Load<Sprite>("Images/AnswerImages/" + qData.idx);

            quizDataDic[qData.quizCategoty].Add(qData); // ±√±›«’¥œ¥Ÿ 

            quizDataList.Add(qData);
        }


        TextAsset wordAsset = Resources.Load<TextAsset>("JSON/WordData");
        JSONObject wordObj = JSONObject.Parse(wordAsset.text);

        JSONArray wordArray = wordObj["WordData"].Array;

        for (int i = 0; i < wordArray.Length; i++)
        {
            WordData wData = new WordData();

            wData.word = wordArray[i].Obj.GetString("Word").Split('/');

            wordDataList.Add(wData);
        }


    }


    //public List<QuizData> GetQuizDatas(QuizCategoty quizCategoty)
    //{
    //    if (quiz)
    //    {

    //    }
    //}
    //public QuizData GetAQuizData(string _key)
    //{
    //    for (int i = 0; i < quizDataList.Count; i++)
    //    {
    //        if (quizDataList[i].Equals(_key))
    //        {
    //            return quizDataList[i];
    //        }
    //    }
    //    return null;
    //}

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
    public Sprite thum;
    public QuizCategoty quizCategoty;

}

[System.Serializable]
public class WordData
{
    public string[] word;
}



