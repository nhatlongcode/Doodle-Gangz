using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gamePlayController : MonoBehaviour
{
    GameObject SoundManager;

    public int score_lv1;
    public int score_lv2;
    public int score_gate;
    public GameObject pauserPanel;
    public GameObject losePanel;
    public GameObject speedUp;
    public Text highScoreText;
    //UI
    public Text scoreText;
    private int _highScore;
    private bool _paused;
    private bool _failed;
    
    [HideInInspector]
    public int score;
    
    // Start is called before the first frame update
    void Start()
    {
        getPlayerHighScore();
        SoundManager = GameObject.Find("SoundManager");
        _paused = false;
        losePanel.SetActive(false);
        pauserPanel.SetActive(false);
        _failed = false;
        score = 0;
    }

    void Update()
    {
        scoreText.text = score.ToString();
        if (_paused)
        {
            Time.timeScale = 0;
            pauserPanel.SetActive(true);
        }
        else if (!_paused && !_failed && Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pauserPanel.SetActive(false);
        }
    }

    public void onPauseButtonClick()
    {
        _paused = true;
    }

    public void onRestartButtonClick()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void onResumeButtonClick()
    {
        _paused = false;
    }

    public void onBackButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0); //back to menu
    }
    public void onSpeedUP()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 2;
            Debug.Log("speedup");
            speedUp.GetComponent<Image>().sprite = speedUp.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            Time.timeScale = 1;
            Debug.Log("slowdown");
            speedUp.GetComponent<Image>().sprite = speedUp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void LOSE()
    {
        Time.timeScale = 0;
        _failed = true;
        losePanel.SetActive(true);
        SoundManager.GetComponent<SoundManager>().soundPopout();
        SoundManager.GetComponent<SoundManager>().soundEndGame();
        if (score > _highScore)
        {
            PlayerPrefs.SetInt("HIGH_SCORE", score);
            _highScore = score;
            highScoreText.text = "High score: " + _highScore.ToString();
        }
        else highScoreText.text = "Score: " + score.ToString();
        losePanel.SetActive(true);
    }
    private void getPlayerHighScore()
    {
        if (!PlayerPrefs.HasKey("PLAYED"))
        {
            PlayerPrefs.SetInt("PLAYED", 1);
            _highScore = 0;
        }
        else
        {
            _highScore = PlayerPrefs.GetInt("HIGH_SCORE");
        }
    }

}
