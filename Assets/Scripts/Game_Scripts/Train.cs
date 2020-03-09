using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Train : MonoBehaviour
{
    GameObject SoundManager;
    public GameObject angleTrash;
    const int maxTypeTrash = 3, maxTrashCount = 3;
    public int curLane = 0;
    public int[] check = new int[10];



    GameObject trashPool;
    public GameObject[] lanes = new GameObject[4];

    void Awake()
    {
        SoundManager = GameObject.Find("SoundManager");
        trashPool = GameObject.Find("trashPool");
        for (int i = 0; i < check.Length; i++)
            check[i] = 0;
        InitTrain();
    }
    public void InitTrain()
    {
        if (this.trashCount <= 0)
        {
            //Init random
            GameObject GO = Instantiate(angleTrash, this.transform.position, Quaternion.identity);
            GO.transform.SetParent(this.transform);
            GO.transform.SetAsFirstSibling();
            updateTrainState();
            Debug.Log("init angel");
            return;
        }
        else
            updateTrainState();
    }
    private void updateTrainState()
    {
        for (int i = 0; i < 10; i++)
            check[i] = 0;
        for (int i = 0; i < this.trashCount; i++)
        {
            if (this.transform.GetChild(i).tag == "Trash")
            {
                Trash tmp = this.transform.GetChild(i).GetComponent<Trash>();
                check[tmp.getOrd()]++;
                if (check[tmp.getOrd()] > 1)
                    Combine(tmp.getOrd());
                this.transform.GetChild(i).localPosition = new Vector3(-3f + i * -2f, 0, 0);
                if (i == 0)
                    this.transform.GetChild(i).GetComponent<Trash>().isHead = true;
                else
                    this.transform.GetChild(i).GetComponent<Trash>().isHead = false;
            }
            else
                break;
        }
    }

    int trashCount
    {
        get { return this.gameObject.transform.childCount - 1; }
    }
    public GameObject headTrain
    {
        get { return this.transform.GetChild(0).gameObject; }
        set { headTrain = value; }
    }
    public void moveToTrashPool(GameObject src)
    {
        src.transform.SetParent(trashPool.transform);
        src.transform.localPosition = Vector3.zero;     
        if (src.GetComponent<Trash>().level > 1)
            src.GetComponent<Trash>().decLevel();
        updateTrainState();
    }

    public void pushTrash(Trash src)
    {
        //Debug.Log("pushTrash");
        //Push Transform
        src.gameObject.transform.SetParent(this.transform);
        src.gameObject.transform.SetAsFirstSibling();
        src.gameObject.GetComponent<SpriteRenderer>().flipX = false;       

        if (check[src.getOrd()] > 0)
            Combine(src.getOrd());

        //Check amount of train
        if (this.trashCount > maxTrashCount)
            popTrash();
        else
            updateTrainState();
    }
    public void popTrash()
    {
        //Debug.Log("popTrash");
        GameObject lastTrash = this.transform.GetChild(trashCount - 1).gameObject;
        moveToTrashPool(lastTrash);     
    }  
    void Combine(int num)
    {
        //Debug.Log("combined!");
        SoundManager.GetComponent<SoundManager>().soundEvolve();
        this.gameObject.transform.GetChild(0).GetComponent<Trash>().incLevel();
        for (int i = 1; i < trashCount; i++)
        {
            if (this.gameObject.transform.GetChild(i).GetComponent<Trash>().getOrd() == num)
            {
                moveToTrashPool(this.gameObject.transform.GetChild(i).gameObject);              
                return;
            }
        }
        updateTrainState();
    }
    public void changeLane()
    {
        //change pos
        this.transform.SetParent(lanes[curLane].transform);
        this.transform.localPosition = new Vector3(0, 0, 0);

        //smoke fx
        this.transform.GetChild(trashCount).gameObject.SetActive(true);
        this.transform.GetChild(trashCount).GetComponent<ParticleSystem>().Play();
    }

    
}
