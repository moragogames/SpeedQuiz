using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizCanvas : MonoBehaviour
{
    [SerializeField] QuizTimePanel quizTimePanel;

    List<AnswerObject> answerObjectList = new List<AnswerObject>();


    [SerializeField] Image questionImage;
    [SerializeField] GameObject answerObjectPrefab;
    [SerializeField] Transform answerObjectTr;

    [SerializeField] WordBtn[] wordBtns;
    [SerializeField] TMP_Text[] wordText;

    int answerIdx = 0;
    bool isClicked = false;


    //퀴즈 보여주기
    public void CreatQuiz(QuizData qData) //
    {
        answerIdx = 0;

        for (int i = 0; i < answerObjectList.Count; i++)  
        {
            //answerObjectList[i].SetAnswer("");
            AnswerObject aObject = answerObjectList[i];
            answerObjectList.Remove(aObject);
            Destroy(aObject.gameObject);
            i--;
        }

        // 정답단어 복사해서 넣기
        char[] correctArr = qData.correct.ToCharArray();
        for (int i = 0; i < correctArr.Length; i++)
        {
            AnswerObject cloneAnswerObject = Instantiate(answerObjectPrefab, answerObjectTr).GetComponent<AnswerObject>();
            answerObjectList.Add(cloneAnswerObject);
            cloneAnswerObject.SetAnswer("");
        }
        questionImage.sprite = qData.sprite; // 퀴즈 이미지 //
        //questionImage.sprite = qData.quizImage; // 퀴즈 이미지 //

        // 문제단어 제출하기
        //List<string> wordList = new List<string>();
        List<string> wordList = new List<string>();
        List<char> correct = new List<char>();

        wordList.AddRange(DataMgr.Instance.words);
        correct.AddRange(correctArr);
       
        int idx = 0;
        for (int i = 0; i < wordBtns.Length; i++)
        {
            //int rand = Random.Range(0, correct.Count);
            wordBtns[idx].SetWordBtn(correct[idx]);
            //wordBtns[idx].SetWordBtn(wordList[rand]);
            //wordList.Remove(wordList[rand]);
            //correct.Remove(correct[rand]);
            //i--; // 인덱스의 역활??
            idx++;
        }
        #region 문제원본
        //wordList.AddRange(qData.words);
        //int idx = 0;
        //for (int i = 0; i < wordList.Count; i++)
        //{
        //    wordBtns[idx].SetWordBtn(wordList[i]);
        //    wordList.Remove(wordList[i]);
        //    i--;
        //    idx++;
        //}
        #endregion



    }



    // 퀴즈 시간 
    public void Answered(bool _b)
    {
        quizTimePanel.Answered(_b);
    }

    // 정답 버튼을 누르기
    public void OnClickedWord(string answerWord) 
    {
        if (isClicked)
        {
            return;
        }
        SoundMgr.Instance.PlaySound(SFXType.click);
        answerObjectList[answerIdx].SetAnswer(answerWord);

        answerIdx++;
        if (answerIdx == answerObjectList.Count)
        {
            isClicked = true;
            string answer = null;
            for (int i = 0; i < answerObjectList.Count; i++)
            {
                answer += answerObjectList[i].answerWord;
            }
            QuizMgr.Instance.Answer(answer);

            StartCoroutine("CoWaitNext");
        }

    }

    IEnumerator CoWaitNext()
    {
        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < answerObjectList.Count; i++)
        {
            answerObjectList[i].SetAnswer("");
        }
        answerIdx = 0;
        isClicked=false;
    }

}
