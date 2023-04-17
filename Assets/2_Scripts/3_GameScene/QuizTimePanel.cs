using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizTimePanel : MonoBehaviour
{
    [SerializeField] Image progressBar;
    [SerializeField] Popup popup;
    public float maxTime = 10;

    private void Start()
    {
        progressBar.fillAmount = 0;
    }

    //��� �����Ҷ� ȣ��
    public void QuizTimerStart()
    {
        
        progressBar.fillAmount = 1;
        StartCoroutine("ProgressTimer");
    }

    //��� Ǯ������ ȣ��
    public void Answered(bool _b)
    {
        StopCoroutine("ProgressTimer");
    }

    bool isStopTime;
    public void PopUpTimeStop(bool _t)
    {
        isStopTime = _t;
    }


    //�ð��ȿ� ��� Ǯ�� �ڷ�ƾ ����
    IEnumerator ProgressTimer()
    {
        float timer = maxTime; // 5��

        while (true)
        {
            if (timer <= 0)
                break; // Ÿ�̸Ӱ� 0�̰ų� ������ �ڷ�ƾ Ż��
            if (isStopTime)
            {
                yield return null;
                continue;
            }

            yield return null; // �������� ����
            timer -= Time.deltaTime;  // �ð��� �ٿ���
            progressBar.fillAmount  = timer / maxTime; // Ÿ�̸ӹ� �̹����� Ÿ���� ��
        }
        progressBar.fillAmount = 0; // 
        QuizMgr.Instance.TimeOver(); // 
        popup.SetGameOverPanel(true);
    }

    





}
