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
        Debug.Log("Unity Ads 초기화 완료");
        LoadAd(AdUnitType.IS);
        LoadAd(AdUnitType.RV); //보상형 광고 로드
    }


    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads 초기화 실패: {error.ToString()} - {message}");
    }

    // 광고 단위 로드 요청
    public void LoadAd(AdUnitType adUnitType)
    {
        Debug.Log("Loading Ad: " + adUnitType);

        string adUnitId = GetAdUnitId(adUnitType);
        if (string.IsNullOrEmpty(adUnitId))
            return;

        Advertisement.Load(adUnitId, this);
    }

    Action<bool> endCallback;
    // 광고 단위 송출
    public void ShowAd(AdUnitType adUnitType, Action<bool> callback)
    {
        Debug.Log("Showing Ad: " + ISAdUnitId);

        string adUnitId = GetAdUnitId(adUnitType);
        if (string.IsNullOrEmpty(adUnitId))
            return;

        endCallback = callback;
        Advertisement.Show(adUnitId, this);
    }

    //광고 단위 종류에 따른 광고 ID 가져오기
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

    //adUnitId로 광고 유닛 타입 가져오기
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
        Debug.Log(GetAdUnitType(_adUnitId) + " 광고 로드 성공");
    }

    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"{GetAdUnitType(_adUnitId)} 광고 로드 실패 - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"{GetAdUnitType(_adUnitId)} 광고 출력 실패 - {error.ToString()} - {message}");
        endCallback?.Invoke(false);
        endCallback = null;
    }

    public void OnUnityAdsShowStart(string _adUnitId)
    {
        Debug.Log($"{GetAdUnitType(_adUnitId)} 광고 출력 시작");
    }

    public void OnUnityAdsShowClick(string _adUnitId)
    {

        Debug.Log($"{GetAdUnitType(_adUnitId)} 광고 클릭");

    }
    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        AdUnitType adUnitType = GetAdUnitType(_adUnitId);

        if (adUnitType == AdUnitType.IS)
        {
            endCallback?.Invoke(true);
        }
        else if (adUnitType == AdUnitType.RV) //★★★★ 아이템 지불 관련★★★★
        {
            switch (showCompletionState)
            {
                case UnityAdsShowCompletionState.COMPLETED:
                    //시청 완료 - 보상 지급 시점
                    endCallback?.Invoke(true);
                    break;
                case UnityAdsShowCompletionState.SKIPPED: //스킵 
                case UnityAdsShowCompletionState.UNKNOWN: //알 수 없음
                    endCallback?.Invoke(false);
                    break;
            }
        }

        endCallback = null;
        LoadAd(adUnitType);
        Debug.Log($"{adUnitType} 광고 시청 완료");
    }

   

    //void CheckReward(bool _b)
    //{

    //}

}