using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public int _myCoin;
    public int myCoin
    {
        get { return _myCoin; }
        set
        {
            _myCoin = value;
            SaveMgr.SetSaveInt("coinCount", _myCoin);
        }
    }


   
    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;// 화면 꺼짐 방지
        Application.targetFrameRate = 60;
        myCoin = SaveMgr.GetSaveInt("coinCount", myCoin); // 세이브 로드

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
