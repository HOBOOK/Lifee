//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Purchasing;
//using System;
//using UnityEngine.UI;

//public class InAppPurchaser : MonoBehaviour, IStoreListener
//{
//    private static IStoreController storeController;
//    private static IExtensionProvider extensionProvider;

//    #region 상품ID
    
//    public const string productId1 = "particle1";
//    public const string productId2 = "particle2";
//    public const string productId3 = "particle3";
//    public const string productId4 = "particle4";
//    public const string productId5 = "particle5";
//    public const string productId6 = "particle6";
//    public const string productId7 = "package1";
//    #endregion

//    void Start ()
//    {
//        InitializePurchasing();
//        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
//        {
//            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
//        }
//    }

//	private bool IsInitialized()
//    {
//        return (storeController != null && extensionProvider != null);
//    }

//    public void InitializePurchasing()
//    {
//        if (IsInitialized())
//            return;

//        var module = StandardPurchasingModule.Instance();

//        ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);

//        builder.AddProduct(productId1, ProductType.Consumable, new IDs
//        {
//            { productId1, AppleAppStore.Name },
//            { productId1, GooglePlay.Name} ,
//        });

//        builder.AddProduct(productId2, ProductType.Consumable, new IDs
//        {
//            { productId2, AppleAppStore.Name },
//            { productId2, GooglePlay.Name} ,
//        });

//        builder.AddProduct(productId3, ProductType.Consumable, new IDs
//        {
//            { productId3, AppleAppStore.Name },
//            { productId3, GooglePlay.Name} ,
//        });

//        builder.AddProduct(productId4, ProductType.Consumable, new IDs
//        {
//            { productId4, AppleAppStore.Name },
//            { productId4, GooglePlay.Name} ,
//        });

//        builder.AddProduct(productId5, ProductType.Consumable, new IDs
//        {
//            { productId5, AppleAppStore.Name },
//            { productId5, GooglePlay.Name} ,
//        });

//        builder.AddProduct(productId6, ProductType.Consumable, new IDs
//        {
//            { productId6, AppleAppStore.Name },
//            { productId6, GooglePlay.Name} ,
//        });

//        builder.AddProduct(productId7, ProductType.Consumable, new IDs
//        {
//            { productId7, AppleAppStore.Name },
//            { productId7, GooglePlay.Name} ,
//        });

//        UnityPurchasing.Initialize(this, builder);
//    }

//    public void BuyProductID(string productId)
//    {
//        try
//        {
//            if(IsInitialized())
//            {
//                Product p = storeController.products.WithID(productId);

//                if(p!=null&&p.availableToPurchase)
//                {
//                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", p.definition.id));
//                    storeController.InitiatePurchase(p);
//                }
//                else
//                {
//                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
//                }
//            }
//            else
//            {
//                Debug.Log("BuyProductID FAIL, Not initialized.");
//            }
//        }
//        catch(Exception e)
//        {
//            Debug.Log("BuyProductID: FAIL, Exception during purchase. " + e);
//        }
//    }

//    public void RestorePurchase()
//    {
//        if(!IsInitialized())
//        {
//            Debug.Log("RestorePurchases FAIL, Not initialized.");
//            return;
//        }

//        if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
//        {
//            Debug.Log("RestorePurchases started ...");

//            var apple = extensionProvider.GetExtension<IAppleExtensions>();

//            apple.RestoreTransactions
//                (
//                    (result) => { Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore."); }
//                );
//        }
//        else
//        {
//            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
//        }
//    }

//    public void OnInitialized(IStoreController sc, IExtensionProvider ep)
//    {
//        Debug.Log("OnInitialized : PASS");

//        storeController = sc;
//        extensionProvider = ep;
//    }

//    public void OnInitializeFailed(InitializationFailureReason reason)
//    {
//        Debug.Log("OnInitializeFailed InitializationFailureReason:" + reason);
//    }

//    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
//    {
//        Debug.Log(string.Format("ProcessPurchase: PASS, Product: '{0}'", args.purchasedProduct.definition.id));

//        switch(args.purchasedProduct.definition.id)
//        {
//            case productId1: //30개지급
//                User.particle += 30;
//                GameObject.Find("CanvasOverlay/Report").transform.GetChild(2).gameObject.SetActive(true);
//                GameObject.Find("CanvasOverlay/Report").transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("particle");
//                GameObject.Find("CanvasOverlay/Report").transform.GetChild(2).GetChild(2).GetComponent<Text>().text = "<color='yellow'>행복의 파편 30개</color>\r\n를 얻었어요.";

//                break;
//            case productId2: //180개지급
//                User.particle += 180;
//                GameObject.Find("CanvasOverlay/Report").transform.GetChild(2).gameObject.SetActive(true);
//                GameObject.Find("CanvasOverlay/Report").transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("particle2");
//                GameObject.Find("CanvasOverlay/Report").transform.GetChild(2).GetChild(2).GetComponent<Text>().text = "<color='yellow'>행복의 파편 180개</color>\r\n를 얻었어요.";

//                break;
//            case productId3: //420개지급
//                User.particle += 420;
//                GameObject.Find("CanvasOverlay/Report").transform.GetChild(2).gameObject.SetActive(true);
//                GameObject.Find("CanvasOverlay/Report").transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("particle3");
//                GameObject.Find("CanvasOverlay/Report").transform.GetChild(2).GetChild(2).GetComponent<Text>().text = "<color='yellow'>행복의 파편 420개</color>\r\n를 얻었어요.";

//                break;
//            case productId4: //960개지급
//                User.particle += 960;
//                GameObject.Find("CanvasOverlay/Report").transform.GetChild(2).gameObject.SetActive(true);
//                GameObject.Find("CanvasOverlay/Report").transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("particle4");
//                GameObject.Find("CanvasOverlay/Report").transform.GetChild(2).GetChild(2).GetComponent<Text>().text = "<color='yellow'>행복의 파편 960개</color>\r\n를 얻었어요.";

//                break;
//            case productId5: //2160개지급
//                User.particle += 2160;
//                GameObject.Find("CanvasOverlay/Report").transform.GetChild(2).gameObject.SetActive(true);
//                GameObject.Find("CanvasOverlay/Report").transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("particle5");
//                GameObject.Find("CanvasOverlay/Report").transform.GetChild(2).GetChild(2).GetComponent<Text>().text = "<color='yellow'>행복의 파편 2160개</color>\r\n를 얻었어요.";

//                break;
//            case productId6: //6000개지급
//                User.particle += 6000;
//                GameObject.Find("CanvasOverlay/Report").transform.GetChild(2).gameObject.SetActive(true);
//                GameObject.Find("CanvasOverlay/Report").transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("particle6");
//                GameObject.Find("CanvasOverlay/Report").transform.GetChild(2).GetChild(2).GetComponent<Text>().text = "<color='yellow'>행복의 파편 6000개</color>\r\n를 얻었어요.";

//                break;
//            case productId7:
                
//                User.isAds = true;
//                User.particle += 100;
//                User.SaveDate();
//                GameObject.Find("CanvasOverlay/Report").transform.GetChild(2).gameObject.SetActive(true);
//                GameObject.Find("CanvasOverlay/Report").transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("package1");
//                GameObject.Find("CanvasOverlay/Report").transform.GetChild(2).GetChild(2).GetComponent<Text>().text = "<color='yellow'>소중한 시간 패키지</color>\r\n를 얻었어요.";

//                break;
//        }
//        return PurchaseProcessingResult.Complete;
//    }

//    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
//    {
//        Debug.Log(string.Format("OnPurchaseFailed: FAIL, Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
//    }


//}
