using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAds : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] bool _testMode = true;

#if UNITY_IOS
    string gameId = "5252154";
#else
    string gameId = "5252155";
#endif

#if UNITY_IOS
    [SerializeField] string ISAdUnitId = "Interstitial_iOS";
#else
    [SerializeField] string ISAdUnitId = "Interstitial_Android";
#endif
#if UNITY_IOS
    [SerializeField] string RVAdUnitId = "Rewarded_iOS";
#else
    [SerializeField] string RVAdUnitId = "Rewarded_Android";
#endif

    public void Init()
    {
        Debug.Log("Init");
        InitializeAds();
    }

    public void InitializeAds()
    {
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Debug.Log("InitializeAds");
            Advertisement.Initialize(gameId, _testMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads �ʱ�ȭ �Ϸ�");
        LoadAd(AdUnitType.IS);
        LoadAd(AdUnitType.RV); //������ ���� �ε�
    }


    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads �ʱ�ȭ ����: {error.ToString()} - {message}");
    }

    // ���� ���� �ε� ��û
    public void LoadAd(AdUnitType adUnitType)
    {
        Debug.Log("Loading Ad: " + adUnitType);

        string adUnitId = GetAdUnitId(adUnitType);
        if (string.IsNullOrEmpty(adUnitId))
            return;

        Advertisement.Load(adUnitId, this);
    }

    Action<bool> endCallback;
    // ���� ���� ����
    public void ShowAd(AdUnitType adUnitType, Action<bool> callback)
    {
        Debug.Log("Showing Ad: " + ISAdUnitId);

        string adUnitId = GetAdUnitId(adUnitType);
        if (string.IsNullOrEmpty(adUnitId))
            return;

        endCallback = callback;
        Advertisement.Show(adUnitId, this);
    }

    //���� ���� ������ ���� ���� ID ��������
    string GetAdUnitId(AdUnitType adUnitType)
    {
        string adUnitId = null;
        switch (adUnitType)
        {
            case AdUnitType.IS:
                adUnitId = ISAdUnitId;
                break;
            case AdUnitType.RV:
                adUnitId = RVAdUnitId;
                break;
        }

        if (string.IsNullOrEmpty(adUnitId))
            return null;

        return adUnitId;
    }

    //adUnitId�� ���� ���� Ÿ�� ��������
    AdUnitType GetAdUnitType(string adUnitId)
    {
        if (adUnitId.Equals(ISAdUnitId))
            return AdUnitType.IS;
        else if (adUnitId.Equals(RVAdUnitId))
            return AdUnitType.RV;
        else
            return AdUnitType.BN;
    }

    public void OnUnityAdsAdLoaded(string _adUnitId)
    {
        Debug.Log(GetAdUnitType(_adUnitId) + " ���� �ε� ����");
    }

    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"{GetAdUnitType(_adUnitId)} ���� �ε� ���� - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"{GetAdUnitType(_adUnitId)} ���� ��� ���� - {error.ToString()} - {message}");
        endCallback?.Invoke(false);
        endCallback = null;
    }

    public void OnUnityAdsShowStart(string _adUnitId)
    {
        Debug.Log($"{GetAdUnitType(_adUnitId)} ���� ��� ����");
    }

    public void OnUnityAdsShowClick(string _adUnitId)
    {

        Debug.Log($"{GetAdUnitType(_adUnitId)} ���� Ŭ��");

    }
    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        AdUnitType adUnitType = GetAdUnitType(_adUnitId);

        if (adUnitType == AdUnitType.IS)
        {
            endCallback?.Invoke(true);
        }
        else if (adUnitType == AdUnitType.RV) //�ڡڡڡ� ������ ���� ���áڡڡڡ�
        {
            switch (showCompletionState)
            {
                case UnityAdsShowCompletionState.COMPLETED:
                    //��û �Ϸ� - ���� ���� ����
                    endCallback?.Invoke(true);
                    break;
                case UnityAdsShowCompletionState.SKIPPED: //��ŵ 
                case UnityAdsShowCompletionState.UNKNOWN: //�� �� ����
                    endCallback?.Invoke(false);
                    break;
            }
        }

        endCallback = null;
        LoadAd(adUnitType);
        Debug.Log($"{adUnitType} ���� ��û �Ϸ�");
    }

   

    //void CheckReward(bool _b)
    //{

    //}

}