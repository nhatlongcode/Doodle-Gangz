using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameObject SoundManager;
    GameObject laneTarget, laneDes;
    RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager = GameObject.Find("SoundManager");
    }

#if UNITY_WEBGL
    void Update()
    {
        if (Time.timeScale == 0)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit;
            if (Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero))
            {
                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                laneTarget = hit.collider.transform.parent.gameObject;
                if (laneTarget.tag != "Platform")
                    Debug.Log(laneTarget.name);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D hit;
            if (Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero))
            {
                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                laneDes = hit.collider.transform.parent.gameObject;

                //Swap Lane
                swapLane(laneTarget.transform.GetChild(1).GetComponent<Train>(), laneDes.transform.GetChild(1).GetComponent<Train>());
            }
        }
    }
#endif

#if UNITY_ANDROID
    void Update()
    {
        if (Time.timeScale == 0)
            return;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero))
                    {
                        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                        laneTarget = hit.collider.transform.parent.gameObject;
                        if (laneTarget.tag != "Platform")
                            Debug.Log(laneTarget.name);
                    }
                    break;
                case TouchPhase.Ended:
                    if (Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero))
                    {
                        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                        laneDes = hit.collider.transform.parent.gameObject;

                        //Swap Lane
                        //Debug.Log("Swapping!");
                        swapLane(laneTarget.transform.GetChild(1).GetComponent<Train>(), laneDes.transform.GetChild(1).GetComponent<Train>());
                     }
                    break; 
            }
        }
    }
#endif

    void swapLane(Train a, Train b)
    {
        if (a.gameObject.transform.parent.name == b.gameObject.transform.parent.name)
            return;
        int tmp = a.curLane;
        a.curLane = b.curLane;
        b.curLane = tmp;

        a.changeLane();
        b.changeLane();
        SoundManager.GetComponent<SoundManager>().soundChangeLane();
        //Debug.Log("Swapped!");
    }
}
