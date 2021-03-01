using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    /* --- Audio Variables --- */
    private AudioSource soundFX;


    // Start is called before the first frame update
    void Awake()
    {
        soundFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Play Button OnClick (Go To Level Menu )
    public void GoToLevelMenu()
    {
        StartCoroutine(IEGoToLevelMenu());
    }

    IEnumerator IEGoToLevelMenu()
    {
        soundFX.Play();
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("LevelMenu");
    }





}
