using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr : MonoBehaviour
{
    private void Start()
    {
        //PlayerPrefs.SetInt("correctCount", 100);
        //PlayerPrefs.SetString("curQuizCatagory", User.Instance.quizCatagoty.ToString());

        //Debug.Log("������ ���� �Ϸ�");


        //int correctCount = PlayerPrefs.GetInt("correctCount", 0);
        //string curQuizCatagory = PlayerPrefs.GetString("curQuizCatagory", "");

        //Debug.Log("correctCount : " + correctCount);
        //Debug.Log("curQuizCatagory : " + curQuizCatagory);

        //Debug.Log("������ �ҷ����� �Ϸ�");

        SaveMgr.SetSaveInt("correctCount", 0);
        SaveMgr.SetSaveString("curQUizCatagory", User.Instance.quizCatagoty.ToString());

        SaveMgr.GetSaveInt("correctCount", 0);
        SaveMgr.GetSaveString("curQuizCatagory", "");

        //Debug.Log("correctCount : " + correctCount);
        //Debug.Log("curQuizCatagory : " + curQuizCatagory);



    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("������ ����");
        }
    }
}

