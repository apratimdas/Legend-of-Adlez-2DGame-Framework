using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManagerScriptFinal : MonoBehaviour
{

    public float waveStartDelay = 2f;
    public float waveInterval = 10f;
    public float turnDelay = .1f;
    public static GameManagerScriptFinal Instance = null;
    public WaveManagerScriptFinal waveScript;
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
    private bool gameIsOver = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);

            return;
        }
        
        DontDestroyOnLoad(gameObject);
        enemies = new List<Enemy>();
        waveScript = GetComponent<WaveManagerScriptFinal>();
        InitGame();
    }

    private void OnLevelWasLoaded(int index)
    {
        if (Instance != this)
            return;

        //++wave;
        CancelInvoke();

        InitGame();
    }

    void InitGame()
    {
        doingSetup = true;

        if (!waveImage)
            waveImage = GameObject.Find("WaveImage");
        if (!waveText)
            waveText = GameObject.Find("WaveText").GetComponent<Text>();
        waveTextPlaying = GameObject.Find("Wave").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();

        score = 0;
        wave = 1;

        waveText.text = "Wave " + wave;
        scoreText.text = "Score : " + score;
        waveImage.SetActive(false);
        //Invoke("HideWaveImage", waveStartDelay);

        InvokeRepeating("ProgressWave", 5, 5);

        enemies.Clear();
        waveScript.SetupScene(wave);
        
        doingSetup = false;
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

        gameIsOver = true;
        //enabled = false;
    }
    
    void Start()
    {
    }

    void FixedUpdate()
    {
    }

    void ProgressWave()
    {
        ++wave;
        waveTextPlaying.text = "Wave " + wave;
        waveScript.SetupScene(wave);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            gameIsOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.R) && !gameIsOver)
        {
            ProgressWave();
            //SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.L) && gameIsOver)
        {
            Application.Quit();
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

    public int GetWave()
    {
        return wave;
    }
}
