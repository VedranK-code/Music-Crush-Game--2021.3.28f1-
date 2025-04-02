using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Gley.MobileAds;
using GoogleMobileAds.Common;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{


    private void Start()
    {
        API.Initialize();

        API.ShowBanner(BannerPosition.Bottom, BannerType.Adaptive);

///        Debug.Log("GDPR Stat :" + API.GDPRConsentWasSet());


        if (!API.GDPRConsentWasSet())
        {
        
            API.ShowBuiltInConsentPopup(PopupClosed);
        }
    }

    private void PopupClosed()
    {
        
    }
}
