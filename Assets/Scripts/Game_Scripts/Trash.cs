using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public float speed;
    const int maxTypeTrash = 3;
    Animator anim;
    public bool isHead, isFree;

    public int typeTrash;
    public int level = 1;

    //For appeareance
    public Sprite[] spritePool = new Sprite[4];

    private gamePlayController _gamePlayController;

    void Awake()
    {
        _gamePlayController = Object.FindObjectOfType<gamePlayController>();
        anim = this.GetComponent<Animator>();
        anim.SetInteger("typeAnim", level);
    }
    void FixedUpdate()
    {
        if (Time.timeScale == 0)
            return;
        float smooth = 100;
        speed = (_gamePlayController.GetComponent<gamePlayController>().score + smooth) / (10000 + smooth) * 2f;
        if (isFree)
        {
            /*if (this.transform.position.x > 10)
                this.transform.position -= new Vector3(speed * 10, 0, 0);
            else*/
                this.transform.position -= new Vector3(speed, 0, 0);
            this.GetComponent<SpriteRenderer>().flipX = true;
            //set up fX
            this.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            this.transform.GetChild(1).GetComponent<Animator>().enabled = true;
        }
        else
        {
            //set up fX
            this.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            this.transform.GetChild(1).GetComponent<Animator>().enabled = false;
        }
    }

    public int getType()
    {
        return typeTrash;
    }
    public void setType(int num)
    {
        typeTrash = num;
    }
    public int getLevel()
    {
        return level;
    }
    public void setLevel(int num)
    {
        level = num;
    }
    public void incLevel()
    {
        if (level > 2)
            return;
        if (level == 1)
        {
            _gamePlayController.score += _gamePlayController.score_lv1;
        }
        else if (level == 2)
        {
            _gamePlayController.score += _gamePlayController.score_lv2;
        }
        level++;
        //change its appeareance
        combineFX();
        updateAppear();
    }
    public void decLevel()
    {
        if (level < 2)
            return;
        level = 1;
        //change its appeareance
        updateAppear();
    }
    public int getOrd()
    {
        return typeTrash + (level - 1) * maxTypeTrash;
    }
    void updateAppear()
    {
        this.GetComponent<SpriteRenderer>().sprite = spritePool[level - 1];
        anim.SetInteger("typeAnim", level);
    }
    public Train getTrain()
    {
        return this.transform.parent.gameObject.GetComponent<Train>();
    }
    void combineFX()
    {
        this.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        this.transform.GetChild(0).GetChild(0).GetComponent<Animator>().enabled = true;
        this.transform.GetChild(0).GetChild(0).GetComponent<Animator>().Play(0);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Trash" && isHead && other.GetComponent<Trash>().isFree)
        {
            other.GetComponent<Trash>().isFree = false;
            this.getTrain().pushTrash(other.gameObject.GetComponent<Trash>());
        }
    }

}
