using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenuController : MonoBehaviour
{
    GameObject SoundManager;
    public GameObject menu;
    public GameObject menuUI;
    public GameObject intro;
    public GameObject paper;
    public GameObject bottle;
    public GameObject food;
    public Text desText;
    public GameObject model;
    public Text highScoreText;
    public GameObject title;
    public GameObject familyBook;

    private int _highScore;
    private GameObject _prefabs;
    private bool _scrollIn;
    private bool _scrollOut;

    private Renderer _mushRen;
    private MaterialPropertyBlock _mushPropBlock;
    float shaderPara = 1.8f;

    void Awake()
    {
        Time.timeScale = 1;
        SoundManager = GameObject.Find("SoundManager");
        _mushPropBlock = new MaterialPropertyBlock();
        //_mushRen.GetPropertyBlock(_mushPropBlock);
        _mushRen = menu.GetComponent<Renderer>();
        
    }


    void Start()
    {
        if (PlayerPrefs.HasKey("HIGH_SCORE"))
        {
            _highScore = PlayerPrefs.GetInt("HIGH_SCORE");
        }
        highScoreText.text ="High score: " + _highScore.ToString();
        paper.SetActive(false);
        familyBook.SetActive(false);
        bottle.SetActive(false);
        food.SetActive(false);
        model.SetActive(true);
        _prefabs = null;           

        _scrollIn = false;
        _scrollOut = false;
        intro.SetActive(false);
        menuUI.SetActive(true);
        _scrollIn = _scrollOut = false;
    }
    void Update()
    {
        if (_scrollIn)
        {
            scrollInAnimation();
        }
        if (_scrollOut)
        {
            scrollOutAnimation();
        }
        


    }

    public void onQuitButtonClick()
    {
        Application.Quit();
    }
    
    public void onStartButtonClick()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void onIntroButtonClick()
    {
        SoundManager.GetComponent<SoundManager>().soundButton();
        Debug.Log("cc");
        if  (!_scrollIn)
        {
            _scrollOut = true;
            menuUI.SetActive(false);
            
            
        }
    }
    
    public void onSkipButtonClicked()
    {

        SoundManager.GetComponent<SoundManager>().soundButton();
        if (!_scrollOut)
        {
            _scrollIn = true;
            intro.SetActive(false);
        }
    }

    public void onRightButtonClick()
    {
        SoundManager.GetComponent<SoundManager>().soundButton();
        Debug.Log("right");
        if (intro.activeInHierarchy == true)
        {
            familyBook.SetActive(true);
            intro.SetActive(false);
        }
    }

    public void onLeftButtonClick()
    {
        SoundManager.GetComponent<SoundManager>().soundButton();
        Debug.Log("left");
        onSkipButtonClicked();
    }

    public void onBackToIntroButton()
    {
        SoundManager.GetComponent<SoundManager>().soundButton();
        if (familyBook.activeInHierarchy == true)
        {
            intro.SetActive(true);
            familyBook.SetActive(false);
            paper.SetActive(false);
            food.SetActive(false);
            bottle.SetActive(false);
            desText.text = "";
        }
    }

    private void scrollOutAnimation()
    {
        model.SetActive(false);
        title.SetActive(false);
        highScoreText.enabled = false;
        _mushRen.GetPropertyBlock(_mushPropBlock);
        shaderPara -= .05f;
        _mushPropBlock.SetFloat("_PageCurl_movement_1", shaderPara);
        _mushRen.SetPropertyBlock(_mushPropBlock);
        if (_mushPropBlock.GetFloat("_PageCurl_movement_1") <= -.5f)
        {
            _scrollOut = false;
            intro.SetActive(true);
        }

    }

    private void scrollInAnimation()
    {
        
        if (_mushPropBlock.GetFloat("_PageCurl_movement_1") <= 1.8f) intro.SetActive(false);
        _mushRen.GetPropertyBlock(_mushPropBlock);
        shaderPara += .05f;
        _mushPropBlock.SetFloat("_PageCurl_movement_1", shaderPara);
        _mushRen.SetPropertyBlock(_mushPropBlock);
        if (_mushPropBlock.GetFloat("_PageCurl_movement_1") >= 1.8f)
        {
            _scrollIn = false;
            menuUI.SetActive(true);
            model.SetActive(true);
            title.SetActive(true);
            highScoreText.enabled = true;
        }
    }

    public void onButton11Click()
    {
        SoundManager.GetComponent<SoundManager>().soundButton();
        Debug.Log("phahe");
        food.SetActive(false);
        bottle.SetActive(false);
        paper.gameObject.GetComponent<Animator>().SetInteger("typeAnim", 1);
        paper.SetActive(true);
        desText.text = "A depressed and desperate paper broke down because of his terrible past.";
    }

    public void onButton12Click()
    {
        SoundManager.GetComponent<SoundManager>().soundButton();
        food.SetActive(false);
        bottle.SetActive(false);
        paper.gameObject.GetComponent<Animator>().SetInteger("typeAnim", 2);
        paper.SetActive(true);
        desText.text = "A self - secure paper who has infinite doubt about life.";
    }

    public void onButton13Click()
    {
        SoundManager.GetComponent<SoundManager>().soundButton();
        food.SetActive(false);
        bottle.SetActive(false);
        paper.gameObject.GetComponent<Animator>().SetInteger("typeAnim", 3);
        paper.SetActive(true);
        desText.text = "A very wise and calm book who enjoys his life very much.";

    }

    public void onButton21Click()
    {
        SoundManager.GetComponent<SoundManager>().soundButton();
        food.SetActive(false);
        paper.SetActive(false);
        bottle.gameObject.GetComponent<Animator>().SetInteger("typeAnim", 1);
        bottle.SetActive(true);
        desText.text = "An energetic bottle who has a terrible past like ripped paper but still have much faith in life.";
    }

    public void onButton22Click()
    {
        SoundManager.GetComponent<SoundManager>().soundButton();
        food.SetActive(false);
        paper.SetActive(false);
        bottle.gameObject.GetComponent<Animator>().SetInteger("typeAnim", 2);
        bottle.SetActive(true);
        desText.text = "A curious and straight forward water bottle ready to explore the World.";
    }

    public void onButton23Click()
    {
        SoundManager.GetComponent<SoundManager>().soundButton();
        food.SetActive(false);
        paper.SetActive(false);
        bottle.gameObject.GetComponent<Animator>().SetInteger("typeAnim", 3);
        bottle.SetActive(true);
        desText.text = "A big water tank who has a big heart but easy to be annoyed.";
    }

    public void onButton31Click()
    {
        SoundManager.GetComponent<SoundManager>().soundButton();
        bottle.SetActive(false);
        paper.SetActive(false);
        food.gameObject.GetComponent<Animator>().SetInteger("typeAnim", 1);
        food.SetActive(true);
        desText.text = "A fragile hamburger who is afraid of this big scary World.";
    }

    public void onButton32Click()
    {
        SoundManager.GetComponent<SoundManager>().soundButton();
        bottle.SetActive(false);
        paper.SetActive(false);
        food.gameObject.GetComponent<Animator>().SetInteger("typeAnim", 2);
        food.SetActive(true);
        desText.text = "A strong and independent hamburger (or is it just pretend to be?)";
    }

    public void onButton33Click()
    {
        SoundManager.GetComponent<SoundManager>().soundButton();
        bottle.SetActive(false);
        paper.SetActive(false);
        food.gameObject.GetComponent<Animator>().SetInteger("typeAnim", 3);
        food.SetActive(true);
        desText.text = "A very cheerful hamburger who spreads joy and peace everywhere it goes.";
    }

    
}
