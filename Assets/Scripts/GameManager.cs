using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public float waveStartDelay = 2f;
    public float waveInterval = 10f;
    public float turnDelay = .1f;
    public static GameManager Instance = null;
    public WaveManager waveScript;
    public int playerFoodPoints = 100;
    [HideInInspector]
    public bool playersTurn = true;
    public int score = 0;

    private Text waveText;
    private Text waveTextPlaying;
    private Text scoreText;
    private GameObject waveImage;
    private int wave = 1;
    private List<Enemy> enemies;
    private bool enemiesMoving;
    private bool doingSetup;

    //private float timer = 0f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        enemies = new List<Enemy>();
        waveScript = GetComponent<WaveManager>();
        InitGame();
    }

    ////This is called each time a scene is loaded.
    //void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    //{
    //    //Add one to our level number.
    //    ++wave;
    //    //Call InitGame to initialize our level.
    //    InitGame();
    //}

    //void OnEnable()
    //{
    //    //Tell our ‘OnLevelFinishedLoading’ function to start listening for a scene change event as soon as this script is enabled.
    //    SceneManager.sceneLoaded += OnLevelFinishedLoading;
    //}

    private void OnLevelWasLoaded(int index)
    {
        ++wave;

        InitGame();
    }

    void InitGame()
    {
        doingSetup = true;

        waveImage = GameObject.Find("WaveImage");
        waveText = GameObject.Find("WaveText").GetComponent<Text>();
        waveTextPlaying = GameObject.Find("Wave").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        waveText.text = "Wave " + wave;
        scoreText.text = "Score : " + score;
        waveImage.SetActive(false);
        Invoke("HideWaveImage", waveStartDelay);

        enemies.Clear();
        waveScript.SetupScene(wave);
    }

    private void HideWaveImage()
    {
        waveImage.SetActive(false);
        doingSetup = false;
    }

    public void GameOver()
    {
        CancelInvoke();

        waveText.text = "You have reached Wave " + wave + " and perished!";
        scoreText.text = "Your final score is : " + score;
        score = 0;
        waveImage.SetActive(true);

        if (GameObject.Find("Enemies"))
        {
            GameObject.Find("Enemies").SetActive(false);
        }
        if (GameObject.Find("Level"))
        {
            GameObject.Find("Level").SetActive(false);
        }
        if (GameObject.Find("Player"))
        {
            GameObject.Find("Player").SetActive(false);
        }

        //enabled = false;
    }

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("ProgressWave", 5, 5);
    }

    void FixedUpdate()
    {
        //++timer;

        //if (timer >= 120f)
        //{
        //    ++wave;
        //    waveTextPlaying.text = "Wave " + wave;
        //    waveScript.SetupScene(wave);
        //    timer = 0f;
        //}
    }

    void ProgressWave()
    {
        ++wave;
        waveTextPlaying.text = "Wave " + wave;
        waveScript.SetupScene(wave);
       // timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //SceneManager.LoadScene(0);
        }

        if (playersTurn || enemiesMoving || doingSetup)
        {
            return;
        }

        StartCoroutine(MoveEnemies());
    }

    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);
    }

    public void Score(int points)
    {
        score += points;
        
        scoreText.text = "Score : " + score;
    }
}
