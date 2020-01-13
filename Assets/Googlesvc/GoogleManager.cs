using UnityEngine;
using System;
using System.Collections;
//gpg
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
//for encoding
using System.Text;
//for extra save ui
using UnityEngine.SocialPlatforms;
//for text, remove
using UnityEngine.UI;


public class GoogleManager : MonoBehaviour
{
    private static GoogleManager instance;

    public static GoogleManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GoogleManager>();

                if (instance == null)
                {
                    instance = new GameObject("GoogleManager").AddComponent<GoogleManager>();
                }
            }

            return instance;
        }
    }

    public bool isProcessing
    {
        get;
        private set;
    }
    public string loadedData
    {
        get;
        private set;
    }

    private const string m_saveFileName = "game_save_data";

    public bool isAuthenticated
    {
        get
        {
            return Social.localUser.authenticated;
        }
    }

    private void InitiatePlayGames()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        // enables saving game progress.
        .EnableSavedGames()
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = false;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
        
    }

    private void Awake()
    {
        InitiatePlayGames();
    }

    public void Login()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (!success)
            {
                Debug.Log("Fail Login");
            }
            else
            {
                Debug.Log("로그인 성공");
            }
                
        });
    }


    public void CompleteFirst()
    {
        if(!isAuthenticated)
        {
            Login();
            return;
        }
        Social.ReportProgress(Googlesvc.Test.achievement, 100.0, (bool success) =>
          {
              if (!success) { Debug.Log("Report Fail!"); }
          });
    }

    public void CompleteHouse1()
    {
        if (!isAuthenticated)
        {
            Login();
            return;
        }

        Social.ReportProgress(Googlesvc.Test.achievement_1, 100.0, (bool success) =>
        {
            if (success) { PlayerPrefs.SetInt("achievement1", 1); }
        });
    }
    public void CompleteHouse2()
    {
        if (!isAuthenticated)
        {
            Login();
            return;
        }

        Social.ReportProgress(Googlesvc.Test.achievement_2, 100.0, (bool success) =>
        {
            if (success) { PlayerPrefs.SetInt("achievement2", 1); }
        });
    }
    public void CompleteHouse3()
    {
        if (!isAuthenticated)
        {
            Login();
            return;
        }

        Social.ReportProgress(Googlesvc.Test.achievement_3, 100.0, (bool success) =>
        {
            if (success) { PlayerPrefs.SetInt("achievement3", 1); }
        });
    }
    public void CompleteHouse4()
    {
        if (!isAuthenticated)
        {
            Login();
            return;
        }

        Social.ReportProgress(Googlesvc.Test.achievement_4, 100.0, (bool success) =>
        {
            if (success) { PlayerPrefs.SetInt("achievement4", 1); }
        });
    }
    public void CompleteHouse5()
    {
        if (!isAuthenticated)
        {
            Login();
            return;
        }

        Social.ReportProgress(Googlesvc.Test.achievement_5, 100.0, (bool success) =>
        {
            if (success) { PlayerPrefs.SetInt("achievement5", 1); }
        });
    }

    public void CompleteJob5()
    {
        if (!isAuthenticated)
        {
            Login();
            return;
        }

        Social.ReportProgress(Googlesvc.Test.achievement_5_2, 100.0, (bool success) =>
        {
            if (success) { PlayerPrefs.SetInt("job5", 1); }
        });
    }
    public void CompleteJob10()
    {
        if (!isAuthenticated)
        {
            Login();
            return;
        }

        Social.ReportProgress(Googlesvc.Test.achievement_10, 100.0, (bool success) =>
        {
            if (success) { PlayerPrefs.SetInt("job10", 1); }
        });
    }
    public void CompleteJob15()
    {
        if (!isAuthenticated)
        {
            Login();
            return;
        }

        Social.ReportProgress(Googlesvc.Test.achievement_15, 100.0, (bool success) =>
        {
            if (success) { PlayerPrefs.SetInt("job15", 1); }
        });
    }
    public void CompleteJob20()
    {
        if (!isAuthenticated)
        {
            Login();
            return;
        }

        Social.ReportProgress(Googlesvc.Test.achievement__20, 100.0, (bool success) =>
        {
            if (success) { PlayerPrefs.SetInt("job20", 1); }
        });
    }
    public void CompleteJob25()
    {
        if (!isAuthenticated)
        {
            Login();
            return;
        }

        Social.ReportProgress(Googlesvc.Test.achievement__25, 100.0, (bool success) =>
        {
            if (success) { PlayerPrefs.SetInt("job25", 1); }
        });
    }
    public void CompleteJob30()
    {
        if (!isAuthenticated)
        {
            Login();
            return;
        }

        Social.ReportProgress(Googlesvc.Test.achievement__30, 100.0, (bool success) =>
        {
            if (success) { PlayerPrefs.SetInt("job30", 1); }
        });
    }

    public void CompleteTour5()
    {
        if (!isAuthenticated)
        {
            Login();
            return;
        }

        Social.ReportProgress(Googlesvc.Test.achievement__5, 100.0, (bool success) =>
        {
            if (success) { PlayerPrefs.SetInt("tour5", 1); }
        });
    }
    public void CompleteTour10()
    {
        if (!isAuthenticated)
        {
            Login();
            return;
        }

        Social.ReportProgress(Googlesvc.Test.achievement__10, 100.0, (bool success) =>
        {
            if (success) { PlayerPrefs.SetInt("tour10", 1); }
        });
    }
    public void CompleteTour15()
    {
        if (!isAuthenticated)
        {
            Login();
            return;
        }

        Social.ReportProgress(Googlesvc.Test.achievement__15, 100.0, (bool success) =>
        {
            if (success) { PlayerPrefs.SetInt("tour15", 1); }
        });
    }
    public void CompleteTour20()
    {
        if (!isAuthenticated)
        {
            Login();
            return;
        }

        Social.ReportProgress(Googlesvc.Test.achievement__20_2, 100.0, (bool success) =>
        {
            if (success) { PlayerPrefs.SetInt("tour20", 1); }
        });
    }
    public void CompleteTour25()
    {
        if (!isAuthenticated)
        {
            Login();
            return;
        }

        Social.ReportProgress(Googlesvc.Test.achievement__25_2, 100.0, (bool success) =>
        {
            if (success) { PlayerPrefs.SetInt("tour25", 1); }
        });
    }
    public void CompleteTour30()
    {
        if (!isAuthenticated)
        {
            Login();
            return;
        }

        Social.ReportProgress(Googlesvc.Test.achievement__30_2, 100.0, (bool success) =>
        {
            if (success) { PlayerPrefs.SetInt("tour30", 1); }
        });
    }

    public void ShowAchivementUI()
    {
        if (!isAuthenticated)
        {
            Login();
            return;
        }
        Social.ShowAchievementsUI();
    }

    private void ProcessCloudData(byte[] cloudData)
    {
        if (cloudData == null)
        {
            Debug.Log("No Data saved to the cloud");
            return;
        }

        string progress = BytesToString(cloudData);
        loadedData = progress;
    }


    public void LoadFromCloud(Action<string> afterLoadAction)
    {
        if (isAuthenticated && !isProcessing)
        {
            StartCoroutine(LoadFromCloudRoutin(afterLoadAction));
        }
        else
        {
            Login();
            GameObject.Find("Report").transform.GetChild(8).gameObject.SetActive(true);
        }
    }

    private IEnumerator LoadFromCloudRoutin(Action<string> loadAction)
    {
        isProcessing = true;
        Debug.Log("Loading game progress from the cloud.");

        ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(
            m_saveFileName, //name of file.
            DataSource.ReadCacheOrNetwork,
            
            ConflictResolutionStrategy.UseLongestPlaytime,
            OnFileOpenToLoad);

        while (isProcessing)
        {
            yield return null;
        }

        loadAction.Invoke(loadedData);
    }

    public void SaveToCloud(string dataToSave)
    {

        if (isAuthenticated)
        {
            loadedData = dataToSave;
            isProcessing = true;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(m_saveFileName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnFileOpenToSave);
            GameObject.Find("Report").transform.GetChild(3).gameObject.SetActive(true);
        }
        else
        {
            Login();
            GameObject.Find("Report").transform.GetChild(7).gameObject.SetActive(true);
        }
    }

    private void OnFileOpenToSave(SavedGameRequestStatus status, ISavedGameMetadata metaData)
    {
        if (status == SavedGameRequestStatus.Success)
        {

            byte[] data = StringToBytes(loadedData);

            SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();

            SavedGameMetadataUpdate updatedMetadata = builder.Build();

            ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(metaData, updatedMetadata, data, OnGameSave);
        }
        else
        {
            Debug.LogWarning("Error opening Saved Game" + status);
        }
    }


    private void OnFileOpenToLoad(SavedGameRequestStatus status, ISavedGameMetadata metaData)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(metaData, OnGameLoad);
        }
        else
        {
            Debug.LogWarning("Error opening Saved Game" + status);
        }
    }


    private void OnGameLoad(SavedGameRequestStatus status, byte[] bytes)
    {
        if (status != SavedGameRequestStatus.Success)
        {
            Debug.LogWarning("Error Saving" + status);
        }
        else
        {
            ProcessCloudData(bytes);
        }

        isProcessing = false;
    }

    private void OnGameSave(SavedGameRequestStatus status, ISavedGameMetadata metaData)
    {
        if (status != SavedGameRequestStatus.Success)
        {
            Debug.LogWarning("Error Saving" + status);
        }

        isProcessing = false;
    }

    private byte[] StringToBytes(string stringToConvert)
    {
        return Encoding.UTF8.GetBytes(stringToConvert);
    }

    private string BytesToString(byte[] bytes)
    {
        return Encoding.UTF8.GetString(bytes);
    }

}
