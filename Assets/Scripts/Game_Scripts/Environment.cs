using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public float speed;
    public float borderL;
    void Awake()
    {
        InvokeRepeating("UpdateEnv", 0, .04f);
    }
    void UpdateEnv()
    {
        this.transform.position -= new Vector3(speed, 0, 0);
        if (this.transform.position.x < borderL)
        {
            this.transform.position = new Vector3(this.transform.parent.GetChild(this.transform.parent.childCount - 1).transform.position.x + 5.5f, this.transform.position.y, this.transform.position.z);
            this.transform.SetAsLastSibling();
        }
    }
}
