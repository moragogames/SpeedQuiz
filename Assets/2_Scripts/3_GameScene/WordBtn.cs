using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WordBtn : MonoBehaviour
{
    TMP_Text wordText;
    Button Wordbtn;

    private void Start()
    {
        wordText = GetComponentInChildren<TMP_Text>();
        Wordbtn = GetComponentInChildren<Button>();
        Wordbtn.onClick.AddListener(OnClickedWordBtn);
    }
    string word;
    public void SetWordBtn(string _w)
    {
        word = _w;
        wordText.text = _w.ToString();
    }

    public void OnClickedWordBtn()
    {
        QuizMgr.Instance.quizCanvas.OnClickedWord(word.ToString());
    }
}
