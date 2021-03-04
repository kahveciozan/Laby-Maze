using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using GoogleMobileAds.Api;

public class MainMenu : MonoBehaviour
{
    /* --- Audio Variables --- */
    private AudioSource soundFX;

    /* --- Settings Variables ---*/
    public GameObject SettingsPopUp;
    public GameObject examplePlayer;

    /* --- Banner Ad Variables --- */
    private BannerView bannerView;

    private void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        //this.RequestBanner();                                 //  *** ADVERTISING *** (Activate this line) 


        soundFX = GetComponent<AudioSource>();

        if (!PlayerPrefs.HasKey("PlayerColor"))
        {
            PlayerPrefs.SetString("PlayerColor", "White");
        }
        InitExamplePlayer();

    }


    //Play Button OnClick (Go To Level Menu )
    public void GoToLevelMenu()
    {
        //bannerView.Destroy();                             // *** ADVERTISING *** (Activate this line) 
        StartCoroutine(IEGoToLevelMenu()); 
    }

    //Quit Game OnClick
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Oyundan Çıkıldı");
    }


    IEnumerator IEGoToLevelMenu()
    {
        soundFX.Play();
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("LevelMenu");
    }

    // Settings Button On Click
    public void SettingsButton()
    {
        soundFX.Play();
        SettingsPopUp.SetActive(true);
    }

    // Close PopUp Menu OnCLick
    public void CloseThePopUpMenu()
    {
        soundFX.Play();
        SettingsPopUp.SetActive(false);
    }

    // Choose Player Color OnClicks
    public void ChoosePlayerColor(string color)
    {
        soundFX.Play();
        PlayerPrefs.SetString("PlayerColor", color);
        switch (color)
        {
            case "White":
                examplePlayer.GetComponent<Image>().color = Color.white;
                break;
            case "Yellow":
                examplePlayer.GetComponent<Image>().color = Color.yellow;
                break;
            case "Red":
                examplePlayer.GetComponent<Image>().color = Color.red;
                break;
            case "Green":
                examplePlayer.GetComponent<Image>().color = Color.green;
                break;
            case "Blue":
                examplePlayer.GetComponent<Image>().color = Color.blue;
                break;
            case "Cyan":
                examplePlayer.GetComponent<Image>().color = Color.cyan;
                break;
            case "Magenta":
                examplePlayer.GetComponent<Image>().color = Color.magenta;
                break;
            case "Black":
                examplePlayer.GetComponent<Image>().color = Color.black;
                break;
            case "Grey":
                examplePlayer.GetComponent<Image>().color = Color.grey;
                break;
        }
    }

    // Hold Chosen Player Color and Assing when start
    private void InitExamplePlayer()
    {
        string tempColor = PlayerPrefs.GetString("PlayerColor");
        switch (tempColor)
        {
            case "White":
                examplePlayer.GetComponent<Image>().color = Color.white;
                break;
            case "Yellow":
                examplePlayer.GetComponent<Image>().color = Color.yellow;
                break;
            case "Red":
                examplePlayer.GetComponent<Image>().color = Color.red;
                break;
            case "Green":
                examplePlayer.GetComponent<Image>().color = Color.green;
                break;
            case "Blue":
                examplePlayer.GetComponent<Image>().color = Color.blue;
                break;
            case "Cyan":
                examplePlayer.GetComponent<Image>().color = Color.cyan;
                break;
            case "Magenta":
                examplePlayer.GetComponent<Image>().color = Color.magenta;
                break;
            case "Black":
                examplePlayer.GetComponent<Image>().color = Color.black;
                break;
            case "Grey":
                examplePlayer.GetComponent<Image>().color = Color.grey;
                break;
        }
    }

    // Banner Advertising 
    private void RequestBanner()
    {
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";                     // Banner !!!!!!!!!!!!!!!!!1
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;
        this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(request);
    }

    /* --- --- --- --- --- --- --- --- --- --- --- --- Advertising Events --- --- --- --- --- --- --- --- --- --- */
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
