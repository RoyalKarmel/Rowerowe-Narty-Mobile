using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    public Banner banner;
    public InterstitialAds interstitialAds;
    public RewardedAds rewardedAds;

    private string _gameId;

    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
#if UNITY_IOS
            _gameId = _iOSGameId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_EDITOR
            _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }


    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");

        if (banner != null)
            banner.LoadBanner();
        if (interstitialAds != null)
            interstitialAds.LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    #region Show ads
    // Banner
    public void ShowBanner()
    {
        banner.ShowBannerAd();
    }

    public void HideBanner()
    {
        banner.HideBannerAd();
    }

    // Interstitial Ad
    public void PlayInterstitialAd()
    {
        interstitialAds.ShowAd();
    }

    // Rewarded Ad
    public void LoadRewardedAd()
    {
        rewardedAds.LoadAd();
    }

    public void PlayRewardedAd()
    {
        rewardedAds.ShowAd();
    }
    #endregion
}
