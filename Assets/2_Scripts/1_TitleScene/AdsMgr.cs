using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsMgr : MonoBehaviour
{
    private static AdsMgr instance;
    public static AdsMgr Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        unityAds = GetComponentInChildren<UnityAds>();
    }

    public UnityAds unityAds;


    void Start()
    {
        unityAds.Init();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowAd(AdUnitType.IS, b => {
                Debug.Log("전면 시청 완료");
            });
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ShowAd(AdUnitType.RV, b => {
                if (b)
                {
                    Debug.Log("광고 시청 완료 - ★보상 지급★");
                }
                else
                {
                    Debug.Log("광고 시청 실패");
                }
            });
        }
    }

    public void ShowAd(AdUnitType adUnitType, Action<bool> callback)
    {
        unityAds.ShowAd(adUnitType, callback);
    }


}

public enum AdUnitType
{
    RV,
    IS,
    BN
}
