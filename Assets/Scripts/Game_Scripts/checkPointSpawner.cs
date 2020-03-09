using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPointSpawner : MonoBehaviour
{
    public float[] defY = new float[4];
    public int type, type1, type2, index, index1, index2;
    float defX;
    void Awake()
    {
        defX = 8f;
        InvokeRepeating("spawnCheckpoint", 20f, 20f);
    }

    void spawnCheckpoint()
    {
        if (Time.timeScale == 0)
            return;
        index = Random.Range(0, this.transform.childCount - 1);
        index1 = Random.Range(0, this.transform.childCount - 1);
        index2 = Random.Range(0, this.transform.childCount - 1);
        //Debug.Log("index:" + index);           
        GameObject Portal0 = this.transform.GetChild(index).gameObject, 
            Portal1 = this.transform.GetChild(index1).gameObject, 
            Portal2 = this.transform.GetChild(index2).gameObject;

        Vector3 targetPos = new Vector3(defX, defY[0], 0);
        Portal0.transform.position = targetPos;
        Portal0.GetComponent<Checkpoint>().typeDoor = Random.Range(0, 100) % 3;
        Portal0.GetComponent<Checkpoint>().isFree = true;

        Vector3 targetPos1 = new Vector3(Portal0.transform.position.x + Random.Range(10f, 20f), defY[1], 0);
        Portal1.transform.position = targetPos1;
        Portal1.GetComponent<Checkpoint>().typeDoor = Random.Range(0, 100) % 3;
        Portal1.GetComponent<Checkpoint>().isFree = true;

        Vector3 targetPos2 = new Vector3(Portal1.transform.position.x + Random.Range(10f, 20f), defY[2], 0);
        Portal2.transform.position = targetPos2;
        Portal2.GetComponent<Checkpoint>().typeDoor = Random.Range(0, 100) % 3;
        Portal2.GetComponent<Checkpoint>().isFree = true; 

        Portal0.transform.SetParent(null);
        Portal1.transform.SetParent(null);
        Portal2.transform.SetParent(null);
        //defX += 30f;
    }  
}
