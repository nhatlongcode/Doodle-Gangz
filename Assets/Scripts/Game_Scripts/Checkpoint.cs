using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameObject SoundManager;
    GameObject checkpointSpawner, gameplayController;
    public bool isFree = true;
    public float speed;
    const int maxTypeTrash = 3;
    public int typeDoor = 0;
    public Sprite[] spritePool = new Sprite[4];
    public GameObject[] rewardPool = new GameObject[10];


    void Awake()
    {
        SoundManager = GameObject.Find("SoundManager");
        checkpointSpawner = GameObject.Find("checkPointPool");
        gameplayController = GameObject.Find("gamePlayController");
    }
    void FixedUpdate()
    {
        if (Time.timeScale == 0)
            return;
        float smooth = 100;
        speed = (gameplayController.GetComponent<gamePlayController>().score + smooth) / (10000 + smooth) * 2f;
        if (isFree)
        {
            this.GetComponent<SpriteRenderer>().sprite = spritePool[typeDoor];
            /*
            if (this.transform.position.x > 10)
                this.transform.position -= new Vector3(speed * 10, 0, 0);
            else*/
                this.transform.position -= new Vector3(speed, 0, 0);
        }
        if (this.transform.position.x < -10f)
        {
            this.transform.SetParent(checkpointSpawner.transform);
            this.transform.localPosition = Vector3.zero;
            isFree = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Trash")
        {
            if (!other.GetComponent<Trash>().isFree)
            {
                this.transform.SetParent(checkpointSpawner.transform);
                this.transform.localPosition = Vector3.zero;
                isFree = false;
                if (other.GetComponent<Trash>().getType() == typeDoor + 1)
                {
                    SoundManager.GetComponent<SoundManager>().soundGatePass();
                    //Pass + collect head of train
                    if (other.GetComponent<Trash>().getLevel() > 1)
                    {
                        //collect
                        Train tmp = other.GetComponent<Trash>().getTrain();
                        GameObject GO = Instantiate(rewardPool[other.GetComponent<Trash>().getOrd()], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                        GO.transform.SetParent(null);
                        tmp.moveToTrashPool(other.gameObject);
                        //reward
                        GO.GetComponent<effectCheckpoint>().beAReward();
                        gameplayController.GetComponent<gamePlayController>().score += gameplayController.GetComponent<gamePlayController>().score_gate;
                        Debug.Log("collected big boy");
                        if (tmp.transform.childCount - 1 <= 0)
                            tmp.InitTrain();

                    }

                }
                else
                {
                    //Die
                    Debug.Log("YOU LOSE !!!");
                    gameplayController.GetComponent<gamePlayController>().LOSE();
                }               
            }
        }
    }

}
