using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trashSpawner : MonoBehaviour
{
    public float[] defY = new float[4];
    GameObject targetTrash;
    public float oldX;
    public int type;

    float minX = 2f, maxX = 5f;
    void Awake()
    {
        InvokeRepeating("spawnTrash", 0, 10f);
        oldX = 8f;
    }

    void spawnTrash()
    {
        if (Time.timeScale == 0)
            return;
        if (this.transform.childCount > 0)
        {
            type = Random.Range(0, this.transform.childCount - 1);
            targetTrash = this.transform.GetChild(type).gameObject;
            if (targetTrash.transform.position.x < -10f)    //invisible from screen
            {
                targetTrash.GetComponent<Trash>().isFree = true;
                int lane = Random.Range(1, 4);
                targetTrash.transform.position = new Vector3(oldX + Random.Range(minX, maxX), defY[lane], 0);
                targetTrash.transform.SetParent(null);
            }
        }
    }

}
