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
                Debug.Log("���� ��û �Ϸ�");
            });
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ShowAd(AdUnitType.RV, b => {
                if (b)
                {
                    Debug.Log("���� ��û �Ϸ� - �ں��� ���ޡ�");
                }
                else
                {
                    Debug.Log("���� ��û ����");
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
