using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position -= new Vector3(speed, 0, 0);
        //invisible of gameplay scene 
        if (this.transform.position.x < -10f) {
            this.transform.localPosition = Vector3.zero;
            this.GetComponent<Plant>().enabled = false;
        }   

    }
}
