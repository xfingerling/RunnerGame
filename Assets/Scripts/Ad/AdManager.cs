using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : IUnityAdsInitializationListener
{
    public static AdManager instance
    {
        get
        {
            if (_instance == null)
                _instance = new AdManager();
            return _instance;
        }
    }
    private static AdManager _instance;

    private const string ANDROIN_GAME_ID = "4888387";
    private const string IOS_GAME_ID = "4888386";
    private const string ANDROID_AD_UNITY_ID = "Rewarded_Android";
    private const string IOS_AD_UNITY_ID = "Rewarded_iOS";
    private bool _testMode = false;

    public string adUnityId { get; private set; }
    public string adGameId { get; private set; }

    public void AdInit()
    {

        //#if UNITY_IOS
        //        adUnityId = IOS_AD_UNITY_ID;
        //        adGameId = IOS_GAME_ID
        //#elif UNITY_ANDROID
        adUnityId = ANDROID_AD_UNITY_ID;
        adGameId = ANDROIN_GAME_ID;
        //#endif

        Advertisement.Initialize(adGameId, _testMode, this);
    }

    public void LoadRewardedAd()
    {
        Advertisement.Load(adUnityId);
    }

    public void ShowRewardedAd(IUnityAdsShowListener showListener)
    {
        Advertisement.Show(adUnityId, showListener);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log(message);
    }
}

