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


    //���� �����ֱ�
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

        questionImage.sprite = qData.sprite; // ���� �̹��� //
        //questionImage.sprite = qData.quizImage; // ���� �̹��� //

        // �����ܾ� �����ϱ�
        List<string> wordList = new List<string>();

        //wordList.AddRange(WordData);
       // wordList.AddRange(qData.words);
        int idx = 0;
        for (int i = 0; i < wordList.Count; i++)
        {
            int rand = Random.Range(0, wordList.Count);
            wordBtns[idx].SetWordBtn(wordList[rand]);
            wordList.Remove(wordList[rand]);
            i--; // �ε����� ��Ȱ??
            idx++;
        }
        #region ��������
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

        // ����ܾ� �����ؼ� �ֱ�
        char[] correctArr = qData.correct.ToCharArray();
        for (int i = 0; i < correctArr.Length; i++)
        {
            AnswerObject cloneAnswerObject = Instantiate(answerObjectPrefab, answerObjectTr).GetComponent<AnswerObject>();
            answerObjectList.Add(cloneAnswerObject);
            cloneAnswerObject.SetAnswer("");
        }


    }



    // ���� �ð� 
    public void Answered(bool _b)
    {
        quizTimePanel.Answered(_b);
    }

    // ���� ��ư�� ������
    public void OnClickedWord(string answerWord) 
    {
        if (isClicked)
        {
            return;
        }

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
