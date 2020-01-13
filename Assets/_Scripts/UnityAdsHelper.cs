//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Advertisements;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;
//using System;

//public class UnityAdsHelper : MonoBehaviour
//{
//    public GameObject resultBox;
//    private const string android_game_id = "1681735";
//    private const string ios_game_id = "1681736";
//    private const string rewarded_video_id = "rewardedVideo";

//	void Start ()
//    {
//        Initialize();
//	}

//    private void Initialize()
//    {
//        Advertisement.Initialize(android_game_id);
//    }

//    public void ShowRewardedAd()
//    {
//        if(User.isAds)
//        {
//            if (resultBox != null)
//            {
//                resultBox.SetActive(true);
//                return;
//            }
//            else if (this.name.Contains("double"))
//            {
//                User.fatigue = 5;
//                User.bonusDate = DateTime.Now.ToString();
//                User.particle += System.Convert.ToSingle(this.transform.GetChild(0).GetComponent<Text>().name);
//                User.SaveDate();
//                this.gameObject.SetActive(false);
//                return;
//            }
//            else if (this.name.Contains("reportAds"))
//            {
//                User.adsCount = 5;
//                this.gameObject.SetActive(false);
//                return;
//            }
//            else if (this.name.Contains("lotto"))
//            {
//                int Random = UnityEngine.Random.Range(0, 100);
//                GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).gameObject.SetActive(true);
//                if (Random < 5)
//                {
//                    GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(3).name = "0";
//                    GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(0).GetComponent<Text>().text = "<복권결과>\r\n'꽝'";
//                    GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(2).GetComponent<Text>().text = "0원";
//                }
//                else if (Random < 15)
//                {
//                    GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(3).name = "30000";
//                    GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(0).GetComponent<Text>().text = "<복권결과>\r\n'5등 당첨!'";
//                    GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(2).GetComponent<Text>().text = "30000원";
//                }
//                else if (Random < 65)
//                {
//                    GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(3).name = "50000";
//                    GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(0).GetComponent<Text>().text = "<복권결과>\r\n'4등 당첨!'";
//                    GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(2).GetComponent<Text>().text = "50000원";
//                }
//                else if (Random < 95)
//                {
//                    GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(3).name = "100000";
//                    GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(0).GetComponent<Text>().text = "<복권결과>\r\n'3등 당첨!'";
//                    GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(2).GetComponent<Text>().text = "100000원";
//                }
//                else if (Random < 99)
//                {
//                    GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(3).name = "300000";
//                    GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(0).GetComponent<Text>().text = "<복권결과>\r\n'2등 당첨!'";
//                    GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(2).GetComponent<Text>().text = "300000원";
//                }
//                else
//                {
//                    GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(3).name = "1000000";
//                    GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(0).GetComponent<Text>().text = "<복권결과>\r\n'1등 당첨!'";
//                    GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(2).GetComponent<Text>().text = "1000000원";
//                }

//                User.rewardSec = DateTime.Now;
//                return;
//            }
//            else if (this.name.Contains("popheart"))
//            {
//                User.fatigue += 1;
//                return;
//            }
//            else if (this.name.Contains("rest"))
//            {
//                if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
//                {
//                    GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
//                }
//                float i = System.Convert.ToSingle(this.transform.GetChild(0).name);
//                User.heart += i;
//                User.SaveDate();
//            }
//            Debug.Log("광고 스킵 완료");
//            return;
            
//        }
//        if (Advertisement.IsReady(rewarded_video_id))
//        {
//            var options = new ShowOptions { resultCallback = HandleShowResult };

//            Advertisement.Show(rewarded_video_id, options);
//        }
//    }

//    private void HandleShowResult(ShowResult result)
//    {
//        switch(result)
//        {
//            case ShowResult.Finished:
//                Debug.Log("광고 시청 완료");
//                //광고시청완료

//                if(resultBox!=null)
//                {
//                    resultBox.SetActive(true);
//                    return;
//                }
//                else if(this.name.Contains("double"))
//                {
//                    User.fatigue = 5;
//                    User.bonusDate = DateTime.Now.ToString();
//                    User.particle += System.Convert.ToSingle(this.transform.GetChild(0).GetComponent<Text>().name);
//                    User.SaveDate();
//                    this.gameObject.SetActive(false);
//                    return;
//                }
//                else if(this.name.Contains("reportAds"))
//                {
//                    User.adsCount = 5;
//                    this.gameObject.SetActive(false);
//                    return;
//                }
//                else if(this.name.Contains("lotto"))
//                {
//                    int Random = UnityEngine.Random.Range(0, 100);
//                    GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).gameObject.SetActive(true);
//                    if (Random<5)
//                    {
//                        GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(3).name = "0";
//                        GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(0).GetComponent<Text>().text = "<복권결과>\r\n'꽝'";
//                        GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(2).GetComponent<Text>().text = "0원";
//                    }
//                    else if(Random<15)
//                    {
//                        GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(3).name = "30000";
//                        GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(0).GetComponent<Text>().text = "<복권결과>\r\n'5등 당첨!'";
//                        GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(2).GetComponent<Text>().text = "30000원";
//                    }
//                    else if(Random<65)
//                    {
//                        GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(3).name = "50000";
//                        GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(0).GetComponent<Text>().text = "<복권결과>\r\n'4등 당첨!'";
//                        GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(2).GetComponent<Text>().text = "50000원";
//                    }                                                                                                          
//                    else if(Random<95)
//                    {
//                        GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(3).name = "100000";
//                        GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(0).GetComponent<Text>().text = "<복권결과>\r\n'3등 당첨!'";
//                        GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(2).GetComponent<Text>().text = "100000원";
//                    }
//                    else if(Random<99)
//                    {
//                        GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(3).name = "300000";
//                        GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(0).GetComponent<Text>().text = "<복권결과>\r\n'2등 당첨!'";
//                        GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(2).GetComponent<Text>().text = "300000원";
//                    }
//                    else
//                    {
//                        GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(3).name = "1000000";
//                        GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(0).GetComponent<Text>().text = "<복권결과>\r\n'1등 당첨!'";
//                        GameObject.Find("CanvasOverlay/Pop").transform.GetChild(6).GetChild(2).GetComponent<Text>().text = "1000000원";
//                    }
                    
//                    User.rewardSec = DateTime.Now;
//                    return;
//                }
//                else if(this.name.Contains("popheart"))
//                {
//                    User.fatigue += 1;
//                    return;
//                }
//                else if(this.name.Contains("rest"))
//                {
//                    if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
//                    {
//                        GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
//                    }
//                    float i = System.Convert.ToSingle(this.transform.GetChild(0).name);
//                    User.heart += i;
//                    User.SaveDate();
//                }
//                break;
//            case ShowResult.Skipped:
//                Debug.Log("광고 끝나기전 스킵됨");
//                break;
//            case ShowResult.Failed:
//                Debug.Log("광고 시청 실패");
//                break;
//        }
//    }
//}
