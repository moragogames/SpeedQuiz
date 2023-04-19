using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    #region
    private static User instance;
    public static User Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
        }

        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    #endregion

    public QuizCategoty quizCatagoty;

    public int correctCount;
    public int myScore;

    public int _coinCount;
    public int coinCount
    {
        get { return _coinCount; }
        set
        {
            _coinCount = value;
            SaveMgr.SetSaveInt("coinCount", _coinCount);
        }
    }


   
    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;// 화면 꺼짐 방지
        Application.targetFrameRate = 60;
        coinCount = SaveMgr.GetSaveInt("coinCount", coinCount); // 세이브 로드

    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)

        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

}
