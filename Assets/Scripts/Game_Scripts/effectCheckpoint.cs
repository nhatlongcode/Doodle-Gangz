using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectCheckpoint : MonoBehaviour
{
    public Vector3 des;
    public void beAReward()
    {
        InvokeRepeating("gotoDesAndScale", 0, .02f);
    }
    public void gotoDesAndScale()
    {
        print("gg");
        this.transform.localScale -= new Vector3(.001f, .001f, .005f);
        this.transform.position = Vector3.MoveTowards(this.transform.position, des, 0.05f);
        if (Mathf.Abs(this.transform.position.x - des.x) < .1f && Mathf.Abs(this.transform.position.y - des.y) < .1f)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
