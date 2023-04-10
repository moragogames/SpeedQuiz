using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerObject : MonoBehaviour
{
    public TMP_Text answerText;
    public string answerWord;

    public void SetAnswer(string words)
    {
        answerWord = words;
        answerText.text = words.ToString();
    }
}
