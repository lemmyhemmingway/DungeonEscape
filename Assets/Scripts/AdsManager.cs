using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    public void ShowRewardedAdd()
    {
        Debug.Log("Show rewarded add");
        // check if advertisement is  ready ( rewardedVideo)
        // Show rewardedVideo

        if (Advertisement.IsReady("rewardedVideo"))
        {
            ShowOptions options = new ShowOptions
            {
                resultCallback = HandleShowResult
            };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                GameManager.Instance.Player.AddGems(100);
                UIManager.Instance.OpenShop(GameManager.Instance.Player.diamonds);
                break;

            case ShowResult.Skipped:
                Debug.Log("Skipped the add");
                break;

            case ShowResult.Failed:
                Debug.Log("Video Failed");
                break;
        }
    }
}
