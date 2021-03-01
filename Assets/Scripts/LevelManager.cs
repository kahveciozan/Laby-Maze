using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public List<Button> levelButtons;

    /* ---Audio Variables --- */
    [SerializeField]
    private AudioSource soundFX;
    [SerializeField]
    private AudioClip selectLevelSound, backSound;

    /* --- Maps Objects --- */
    public List<GameObject> maps;
    int mapIndex;

    /* --- Star Variables --- */
    private string starCountString;
    public List<TextMeshProUGUI> totalStarText;

    // Start is called before the first frame update
    void Start()
    {
        InitStars();
        InitMap();
        ActivateTheStars();
        LevelLockSystem();

    }

    // This method sets which map will load
    private void InitMap()
    {
        /* --------------------- Calculate the Total Star Count ------------------ */
        string totalStarCount_S = PlayerPrefs.GetString("Stars");
        int totalStarCount=0;
        for (int j = 0; j < levelButtons.Count; j++)
        {
            totalStarCount += int.Parse(totalStarCount_S.Substring(j, 1));

            for (int i = 0; i < maps.Count; i++)
            {
                totalStarText[i].text = "" + totalStarCount;
            }
        }

        /* --------------------- Choose the Current Map ------------------ */
        int saveIndex = PlayerPrefs.GetInt("SaveIndex");

        if (saveIndex < 20 || totalStarCount < 60)
            PlayerPrefs.SetInt("MapIndex", 0);
        else if ((20 <= saveIndex && saveIndex < 40) || totalStarCount <120)
            PlayerPrefs.SetInt("MapIndex", 1);
        else if ((40 <= saveIndex && saveIndex <= 60) || totalStarCount<180)        // Yeni map ekleyince "<= 60" buradaki  "=" i kaldir 
            PlayerPrefs.SetInt("MapIndex", 2);
        else
            Debug.LogError("Bu Seviye Bulunmamaktadir");

        mapIndex = PlayerPrefs.GetInt("MapIndex");

        /* --------------------- Map Lock System ----------------------*/ 
        for (int i = 0; i < maps.Count; i++)
        {
            maps[i].transform.GetChild(4).GetComponent<Button>().interactable = false;

            if (totalStarCount >= 60 * (i + 1))
            {
                maps[i].transform.GetChild(4).GetComponent<Button>().interactable = true;
                maps[i].transform.GetChild(4).GetChild(1).gameObject.SetActive(false);

            }

            if (i == mapIndex)
            {
                maps[i].SetActive(true);   
            }
             
            else
                maps[i].SetActive(false);
        }

        

    }

    // Next Button OnClick
    public void NextMap(int mapNumber)
    {
        soundFX.clip = selectLevelSound;
        soundFX.Play();

        mapNumber--;
        maps[mapNumber].SetActive(false);
        maps[mapNumber + 1].SetActive(true);

    }

    // Previous Button OnClick
    public void PreviousMap(int mapNumber)
    {
        soundFX.clip = selectLevelSound;
        soundFX.Play();

        mapNumber--;
        maps[mapNumber].SetActive(false);
        maps[mapNumber - 1].SetActive(true);
    }

    void LevelLockSystem()
    {
        int saveIndex = PlayerPrefs.GetInt("SaveIndex");

        for(int i = 0; i < levelButtons.Count; i++)
        {
            if (i <= saveIndex )
            {
                levelButtons[i].interactable = true;                                        // Button interactible
                levelButtons[i].transform.GetChild(1).gameObject.SetActive(false);          // Lock Image deactivate
            }
            else
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    /* ----------------------- LevelSelect Button OnClick -----------------------------------*/
    public void LevelSelect()
    {
        StartCoroutine(IELevelSelectSound());
    }

    IEnumerator IELevelSelectSound()
    {
        soundFX.clip = selectLevelSound;
        soundFX.Play();
        yield return new WaitForSeconds(0.2f);
        int selectedLevel = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        SceneManager.LoadScene(selectedLevel);
    }

    /* ----------------- Return to Main Menu Button OnClick --------------------------------*/
    public void ReturnToMainMenu()
    {
        StartCoroutine(IEReturnToMainMenu());
    }

    IEnumerator IEReturnToMainMenu()
    {
        soundFX.clip = backSound;
        soundFX.Play();
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("MainMenu");
    }


    private void InitStars()
    {
        if (!PlayerPrefs.HasKey("Stars"))                   // Islemi bir kereye mahsus yaptirdik. Tam anlamadim sonra arastir !
        {
            for (int i = 0; i < 1000; i++)     // bu for dongusunu yuksek sayida dondur (ileride guncelleme yapabilmek icin)
            {
                starCountString += "0";
            }
            PlayerPrefs.SetString("Stars", starCountString);
        }
    }


    public void ActivateTheStars()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            string tempStar = PlayerPrefs.GetString("Stars").Substring(i, 1);

            for (int j = 0; j < int.Parse(tempStar); j++)
            {
                levelButtons[i].transform.GetChild(2).GetChild(j).GetComponent<Image>().color = new Color(255, 255, 255, 255); 
            }
        }
    }

}
