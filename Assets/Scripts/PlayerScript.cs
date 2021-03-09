using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    /* --- Player Variables --- */
    [SerializeField]
    private float moveSpeed = 300f;
    private Rigidbody2D rb;
    private bool isPlayerFinish;

    /* --- Touch Control Variables --- */
    private Touch theTouch;
    private Vector2 touchStartPosition, touchEndPosition;
    private float playerDirectionX, playerDirectionY;

    /* --- Variables for Level --- */
    public int buildIndex;
    private int levelCount = 80;    // <--------------------------------- WRITE HERE THE LEVEL COUNT AS MANUALLY
    private bool lastLevelAndNotEnoughStar = false;

    /* --- AudioSource Variables ---*/
    [SerializeField]
    private AudioSource soundFx;
    [SerializeField]
    private AudioClip stop, run, collectStar, buttonSound;
    private bool isMoveControl, isStopControl;

    /* ----- Star Variables ----- */
    private int toplananItem;
    private string orijinal;

    /* --- Pop Up Menu Variables --- */
    public GameObject canvas;

    /* --- Particle System Variables ---*/
    private ParticleSystem particle;
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    private TrailRenderer trail;

    void Start()
    {
        isMoveControl = false;  isStopControl = false;

        Physics2D.gravity = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();

        SetTheTextName();

        soundFx = GetComponent<AudioSource>();

        particle = GetComponentInChildren<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        trail = GetComponentInChildren<TrailRenderer>();

        SetPlayerColor();
    }

    // Set Player Color , Particle Color and Trail Color
    private void SetPlayerColor()
    {
        string tempColor = PlayerPrefs.GetString("PlayerColor");

        switch (tempColor)
        {
            case "White":
                GetComponent<SpriteRenderer>().color = Color.white;
                GetComponentInChildren<TrailRenderer>().startColor = Color.white;
                particle.startColor = Color.white;
                break;
            case "Yellow":
                GetComponent<SpriteRenderer>().color = Color.yellow;
                GetComponentInChildren<TrailRenderer>().startColor = Color.yellow;
                particle.startColor = Color.yellow;
                break;
            case "Red":
                GetComponent<SpriteRenderer>().color = Color.red;
                GetComponentInChildren<TrailRenderer>().startColor = Color.red;
                particle.startColor = Color.red;
                break;
            case "Green":
                GetComponent<SpriteRenderer>().color = Color.green;
                GetComponentInChildren<TrailRenderer>().startColor = Color.green;
                particle.startColor = Color.green; 
                break;
            case "Blue":
                GetComponent<SpriteRenderer>().color = Color.blue;
                GetComponentInChildren<TrailRenderer>().startColor = Color.blue;
                particle.startColor = Color.blue ;
                break;
            case "Cyan":
                GetComponent<SpriteRenderer>().color = Color.cyan;
                GetComponentInChildren<TrailRenderer>().startColor = Color.cyan;
                particle.startColor = Color.cyan;
                break;
            case "Magenta":
                GetComponent<SpriteRenderer>().color = Color.magenta;
                GetComponentInChildren<TrailRenderer>().startColor = Color.magenta;
                particle.startColor = Color.magenta;
                break;
            case "Black":
                GetComponent<SpriteRenderer>().color = Color.black;
                GetComponentInChildren<TrailRenderer>().startColor = Color.black;
                particle.startColor = Color.black;
                break;
            case "Grey":
                GetComponent<SpriteRenderer>().color = Color.grey;
                GetComponentInChildren<TrailRenderer>().startColor = Color.grey;
                particle.startColor = Color.grey;
                break;

        }


       

    }
    
    // Set LEvel Text Name
    private void SetTheTextName()
    {
        buildIndex = SceneManager.GetActiveScene().buildIndex;
        Text levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Level " + buildIndex.ToString();
    }


    void FixedUpdate()
    {
        if (Mathf.Abs(rb.velocity.x) < 0.1f && Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            TouchControl();
            PlayerMovement();
            ClearVariables();
            StopSound();
        }
        else
        {
            RunSound();
        }
    }

    private void StopSound()
    {
        if (isStopControl)                  // This impotant because we want to play once 
            {
                soundFx.clip = stop;
                soundFx.Play();
                isStopControl = false;
            }
            isMoveControl = true;
    }

    private void RunSound()
    {
        if (isMoveControl)                  // This impotant because we want to play once 
        {
            soundFx.clip = run;
            soundFx.Play();
            isMoveControl = false;
        }
        isStopControl = true;
    }

    private void TouchControl()
    {
        if (isPlayerFinish)
            return;

        if (Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);

            if (theTouch.phase == TouchPhase.Began)
            {
                touchStartPosition = theTouch.position;
            }

            else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended)
            {
                touchEndPosition = theTouch.position;
                float x = touchEndPosition.x - touchStartPosition.x;
                float y = touchEndPosition.y - touchStartPosition.y;

                if (Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0)
                {
                    // Buraya bir ozellik eklenebilir

                }
                else if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    playerDirectionX = x > 0 ? 1 : -1;
                    playerDirectionY = 0;
                }
                else
                {
                    playerDirectionY = y > 0 ? 1 : -1;
                    playerDirectionX = 0;
                }
            }
        }
    }

    private void PlayerMovement()
    {
        Vector2 temp = Vector2.zero;
        temp.x = moveSpeed * playerDirectionX * Time.deltaTime;
        temp.y = moveSpeed * playerDirectionY * Time.deltaTime;
        rb.velocity = temp;
    }

    private void ClearVariables()
    {
        playerDirectionX = 0;
        playerDirectionY = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlayerFinish)
            return;


        if (collision.CompareTag("Win"))
        {
            SaveTheIndex();
            isPlayerFinish = true;
            StartCoroutine(OpenPopUpMenu());
            //OpenPopUpMenu();
            SetTheStars(buildIndex - 1);
            
        }
        if (collision.CompareTag("Star"))
        {
            toplananItem += 1;
            soundFx.clip = collectStar;
            soundFx.Play();
            collision.gameObject.SetActive(false);
            SetCollectedStarImage();
        }
        if (collision.CompareTag("Enemy"))
        {
            StartCoroutine(PlayerDeadEffect());
            StartCoroutine(OpenPopUpMenu());
            canvas.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);

        }
    }

    // Canvas Button OnClick NextLevel
    public void WaitAndNextLevel()
    {
        StartCoroutine(WaitandNextLevel());
    }

    IEnumerator WaitandNextLevel()
    {
        soundFx.clip = buttonSound;
        soundFx.Play();

        yield return new WaitForSeconds(0.2f);

        /* --------------------- LAST LEVEL OF MAP CONTROL -------------------------*/
        int totalStarCount = TotalStarCount();
        int mapCount = PlayerPrefs.GetInt("MapIndex");

        Debug.Log("TOTAL STAR COUNT =" + totalStarCount);
        Debug.Log("MAP COUNT =" + mapCount);

        for (int i = 1; i <= (mapCount + 1); i++)
        {
            if (buildIndex == 20*i && totalStarCount < 60 * i)
            {
                lastLevelAndNotEnoughStar = true;
                break;
            }
                                                                         
        }
        /*---------------------------------------------------------------------*/

        if (buildIndex > levelCount || lastLevelAndNotEnoughStar)
        {
            SceneManager.LoadScene("LevelMenu");                      // Go to Level Menu
            
        }
        else
        {
            Debug.Log("BURASI GIRMEMEMELI");
            SceneManager.LoadScene(buildIndex + 1);         // Go to next level
        }
    }

    // Canvas Button OnClick Play Again
    public void PlayAgain()
    {
        StartCoroutine(IEPlayAgain());
    }

    IEnumerator IEPlayAgain()
    {
        soundFx.clip = buttonSound;
        soundFx.Play();

        yield return new WaitForSeconds(0.2f);

        SceneManager.LoadScene(buildIndex);
    }

    private void SaveTheIndex()
    {

        // Buraya bir seyler ekleneblir
        int saveIndex = PlayerPrefs.GetInt("SaveIndex");    // Get the Save Index

        if (buildIndex > saveIndex)
        {
            PlayerPrefs.SetInt("SaveIndex", buildIndex);    // Set the SaveIndex(The Greater Index)
        }

    }

    public void SetTheStars(int levelID)
    {

        orijinal = PlayerPrefs.GetString("Stars");

        if (toplananItem > int.Parse(orijinal.Substring(levelID, 1)))
        {

            orijinal = orijinal.Remove(levelID, 1);
            orijinal = orijinal.Insert(levelID, toplananItem.ToString());
        }

        PlayerPrefs.SetString("Stars", orijinal);
    }

    IEnumerator OpenPopUpMenu()
    {

        yield return new WaitForSeconds(0.6f);

        for (int i = 0; i< toplananItem; i++)
        {
            canvas.transform.GetChild(0).GetChild(0).GetChild(i).GetComponent<Image>().color = new Color(255, 255, 255, 255);        // Toplanan yildizlari aktif et
        }


        canvas.transform.GetChild(0).gameObject.SetActive(true);                                    // PopUpMenu acar 
        

    }

    IEnumerator PlayerDeadEffect()
    {

        particle.Play();
        sr.enabled = false;
        bc.enabled = false;
        rb.velocity = new Vector2(0.2f, 0.2f);
        trail.gameObject.SetActive(false);
        yield return new WaitForSeconds(particle.main.startLifetime.constantMax);
    }

    //Total Star Count Calculator Method
    private int TotalStarCount()
    {
        string totalStarCount_S = PlayerPrefs.GetString("Stars");
        int totalStarCount = 0;
        for (int j = 0; j < levelCount; j++)
        {
            totalStarCount += int.Parse(totalStarCount_S.Substring(j, 1));
        }

        return totalStarCount;
    }

    //Activate the Collocted Star Image in Canvas ( Top-Left Star Images )
    private void SetCollectedStarImage()
    {
        for(int i=0; i<toplananItem; i++)
        {
            canvas.transform.GetChild(1).GetChild(1).GetChild(i).GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        
    }
}