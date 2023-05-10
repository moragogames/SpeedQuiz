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

    //퀴즈를 제출할때 호출
    public void QuizTimerStart()
    {

        progressBar.fillAmount = 1;
        StartCoroutine("ProgressTimer");
    }

    //퀴즈를 풀었을때 호출
    public void Answered(bool _b)
    {
        StopCoroutine("ProgressTimer");
    }

    bool isStopTime;
    public void PopUpTimeStop(bool _t)
    {
        isStopTime = _t;
    }

    [SerializeField] bool isSoundCheck = false;
    //시간안에 퀴즈를 풀면 코루틴 종료
    IEnumerator ProgressTimer()
    {
        float timer = maxTime; // 5초
        isSoundCheck = false;

        while (true)
        {
            if (timer <= 0)
                break; // 타이머가 0이거나 적으면 코루틴 탈출
            if (isStopTime)
            {
                yield return null;
                continue;
            }
            if (progressBar.fillAmount < 0.3 && !isSoundCheck)
            {
                Debug.Log("짹각짹깍");
                SoundMgr.Instance.PlaySound(SFXType.clock);
                isSoundCheck = true;
            }

            yield return null; // 한프레임 쉬고
            timer -= Time.deltaTime;  // 시간을 줄여라
            progressBar.fillAmount = timer / maxTime; // 타이머바 이미지에 타임을 뺌
        }
        Debug.Log("짹깍false");
        progressBar.fillAmount = 0; 
        SoundMgr.Instance.StopSound(SFXType.clock); // 사운드 스탑|(안됨)
        popup.SetGameOverPanel(true);
        QuizMgr.Instance.TimeOver(); 
    }







}
