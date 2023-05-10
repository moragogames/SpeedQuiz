using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WordBtn : MonoBehaviour
{
    TMP_Text wordText;
    Button wordbtn;

    private void Start()
    {
        wordText = GetComponentInChildren<TMP_Text>();
        wordbtn = GetComponentInChildren<Button>();
        wordbtn.onClick.AddListener(OnClickedWordBtn);
    }
    char word;
    //public void SetWordBtn(string _w)
    //{
    //    word = _w;
    //    wordText.text = _w.ToString();
    //}
    public void SetWordBtn(char _w)
    {
        word = _w;
        wordText.text = _w.ToString();
    }

    public void OnClickedWordBtn()
    {
        QuizMgr.Instance.quizCanvas.OnClickedWord(word.ToString());
       // this.gameObject.GetComponent<Image>().color = new Color32(154, 154, 154, 255);
    }

   
}
