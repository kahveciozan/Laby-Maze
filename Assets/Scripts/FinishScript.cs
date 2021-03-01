using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using GoogleMobileAds.Api;

public class FinishScript : MonoBehaviour
{
    private int buildIndex;

    /* --- Audio Variables --- */
    private AudioSource soundFX;

    /* --- Particle System Variables --- */
    ParticleSystem particle;

    /* --- Advertising Variables --- */
    private InterstitialAd interstitial;

    private void Start()
    {
        //RequestInterstitial();                                            // *** ADVERTISING *** (Activate this line) 
        soundFX = GetComponent<AudioSource>();
        particle = GetComponentInChildren<ParticleSystem>();
        buildIndex = SceneManager.GetActiveScene().buildIndex;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayWinSound();
            PlayParticleWinEffect();

            if (buildIndex > 5)
            {
                //StartCoroutine(ShowTheInterstitialAdvertising());     // *** ADVERTISING *** (Activate this line) 
            }


        }
    }

    private void PlayWinSound()
    {
        soundFX.Play();
    }

    private void PlayParticleWinEffect()
    {
        particle.Play();
    }

    //Interstitial Advertising Inıt Metod
    private void RequestInterstitial()
    {
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";                 // Gecisli app !!!!!!!!!!!!!!!!!!!!!!!!!!!!
        this.interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }

    //Interstitial Advertising Show
    IEnumerator ShowTheInterstitialAdvertising()
    {
        yield return new WaitForSeconds(0.4f);
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }

    /* ------------------------------- Interstitial Advertisng Events ----------------------------------------------*/
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }
}
