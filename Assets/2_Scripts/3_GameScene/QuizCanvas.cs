using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizCanvas : MonoBehaviour
{
    [SerializeField] QuizTimePanel quizTimePanel;

    public  List<AnswerObject> answerObjectList = new List<AnswerObject>();

    [SerializeField] Image questionImage;
    [SerializeField] GameObject answerObjectPrefab;
    [SerializeField] Transform answerObjectTr;

    [SerializeField] WordBtn[] wordBtns;
    [SerializeField] TMP_Text[] wordText;

    [SerializeField] AnswerObject answerObject;

    public  int answerIdx = 0;
    bool isClicked = false;

    [SerializeField] public char[] correctArr;

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

        // ����ܾ� �����ؼ� �ֱ�
        correctArr = qData.correct.ToCharArray();
        for (int i = 0; i < correctArr.Length; i++)
        {
            AnswerObject cloneAnswerObject = Instantiate(answerObjectPrefab, answerObjectTr).GetComponent<AnswerObject>();
            answerObjectList.Add(cloneAnswerObject);
            cloneAnswerObject.SetAnswer("");
        }
        questionImage.sprite = qData.sprite; // ���� �̹��� //

        // �����ܾ� �����ϱ�

        List<char> wordList = new List<char>();
        wordList.AddRange(correctArr);

        int insertCount = 12 - correctArr.Length;

        List<char> randomList = new List<char>();
        randomList.AddRange(DataMgr.Instance.words);


        for (int i = 0; i < insertCount; i++)
        {
            char randomChar = randomList[Random.Range(0, randomList.Count)];
            if (wordList.Contains(randomChar))
            {
                i--;
                continue;
            }
            wordList.Add(randomChar);
            randomList.Remove(randomChar);
            
        }

        //�������� �����
        List<char> wordRandList = new List<char>();
        wordRandList.AddRange(wordList);

        for (int i = 0; i < wordList.Count; i++)
        {
            char rand = wordList[Random.Range(0, wordRandList.Count)];
            wordRandList.Add(rand);
            wordRandList.Remove(rand);
        }

        wordList = wordRandList;

        for (int i = 0; i < wordBtns.Length; i++)
        {

            wordBtns[i].SetWordBtn(wordList[i]);
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

        OnClickedRemove();
        isClicked = false;
    }

    public void OnClickedRemove()
    {
        for (int i = 0; i < answerObjectList.Count; i++)
        {
            answerObjectList[i].SetAnswer("");
        }
        answerIdx = 0;
    }

    public void CoinUp()
    {
        User.Instance.myCoin++;
    }

}
