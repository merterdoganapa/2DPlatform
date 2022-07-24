using System;
using System.Collections;
using System.Timers;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.Networking;


namespace Brainclude.Common
{
    public class AdsController : MonoBehaviour
    {
        [SerializeField] private string bottomBannerId;
        [SerializeField] private string rewardId;
        private IRewardAdListener _rewardAdListener;
        private BannerView _bannerView;
        private RewardedAd _rewardedAd;

        private static AdsController _instance = null;

        public static AdsController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<AdsController>(true);
                return _instance;
            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            MobileAds.Initialize(initStatus =>
            {
                RequestBanner();
                RequestRewarded();
            });
        }
        public void SetRewardListener(IRewardAdListener adListener) => _rewardAdListener = adListener;

        public void ShowRewardedAd()
        {
            if (!_rewardedAd.IsLoaded()) RequestRewarded();
            _rewardedAd.Show();
        }

        private void RequestBanner()
        {
            _bannerView = new BannerView(bottomBannerId, AdSize.Banner, AdPosition.Bottom);
            var request = new AdRequest.Builder().Build();
            _bannerView.LoadAd(request);
        }

        private void RequestRewarded()
        {
            _rewardedAd = new RewardedAd(rewardId);
            _rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
            _rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
            _rewardedAd.OnAdOpening += HandleRewardedAdOpening;
            _rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
            _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            _rewardedAd.OnAdClosed += HandleRewardedAdClosed;
            var request = new AdRequest.Builder().Build();
            _rewardedAd.LoadAd(request);
        }

        private void HandleRewardedAdLoaded(object sender, System.EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdLoaded event received");
        }

        private void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            MonoBehaviour.print(
                "HandleRewardedAdFailedToLoad event received with message: "
            );
        }

        private void HandleRewardedAdOpening(object sender, System.EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdOpening event received");
        }

        private void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
        {
            MonoBehaviour.print(
                "HandleRewardedAdFailedToShow event received with message: "
                + args.Message);
        }

        private void HandleRewardedAdClosed(object sender, System.EventArgs args)
        {
            RequestRewarded();
        }

        private void HandleUserEarnedReward(object sender, Reward args) => _rewardAdListener?.OnRewardEarned();
    }

    public interface IRewardAdListener
    {
        public void OnRewardEarned();
    }
}