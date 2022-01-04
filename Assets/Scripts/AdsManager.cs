using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour, IUnityAdsShowListener, IUnityAdsLoadListener
{
    public Button _showAdButton;
    public string adID;
    private static AdsManager _instance;
    public static AdsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Ads Manager is Null!");
            }

            return _instance;
        }
    }
    public void Awake()
    {
        _instance = this;
    }
    public void LoadAd()
    {
        Debug.Log("Called");
        Advertisement.Load(adID, this);
    }
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Activate Button");
        if (placementId.Equals(adID))
        {
            // Configure the button to call the ShowAd() method when clicked:
            _showAdButton.onClick.AddListener(ShowAd);
            // Enable the button for users to click:
            _showAdButton.interactable = true;
        }
    }
    public void ShowAd()
    {
        // Disable the button: 
        _showAdButton.interactable = false;
        // Then show the ad:
        Advertisement.Show(adID, this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adID}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        //award for completion
        Debug.Log("Completed ad or whatever");
        if (adID.Equals(adID) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.

            // Load another ad:
            Advertisement.Load(adID, this);
        }

        switch (showCompletionState)
        {
            case UnityAdsShowCompletionState.SKIPPED:
                //do nothing
                break;
            case UnityAdsShowCompletionState.COMPLETED:
                //award player
                Debug.Log("Video Watched");
                GameManager.Instance.Player.AddGems(100);
                UIManager.Instance.OpenShop(GameManager.Instance.Player.diamonds);
                break;
            case UnityAdsShowCompletionState.UNKNOWN:
                //do nothing
                break;
            default:
                break;
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
    }

    public void OnUnityAdsShowStart(string placementId)
    {
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }

}
