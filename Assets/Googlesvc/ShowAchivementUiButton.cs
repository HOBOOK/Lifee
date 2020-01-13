using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAchivementUiButton : MonoBehaviour
{
    public void ShowUI()
    {
        GoogleManager.Instance.ShowAchivementUI();
    }

    public void ShowRankUI()
    {
        Social.ReportScore(User.JOB_POINT, "leaderboard2", success => { GooglePlayGames.PlayGamesPlatform.Instance.ShowLeaderboardUI(); });
        Social.ReportScore(User.TOUR_POINT, "leaderboard", success => { GooglePlayGames.PlayGamesPlatform.Instance.ShowLeaderboardUI(); });
    }
}
