using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource au;
    public AudioClip[] soundFX = new AudioClip[5];
    void Start()
    {
        au = this.GetComponent<AudioSource>();
    }
    public void soundChangeLane()
    {
        au.Stop();
        au.clip = soundFX[3];
        au.Play(0);
    }
    public void soundEvolve()
    {
        au.Stop();
        au.clip = soundFX[1];
        au.Play(0);
    }
    public void soundButton()
    {
        au.Stop();
        au.clip = soundFX[0];
        au.Play(0);
    }
    public void soundPopout()
    {
        au.Stop();
        au.clip = soundFX[4];
        au.Play(0);
        //Invoke("soundEndGame", 1);
    }
    public void soundGatePass()
    {
        au.Stop();
        au.clip = soundFX[2];
        au.Play(0);
    }
    public void soundEndGame()
    {
        this.transform.GetChild(0).GetComponent<AudioSource>().Stop();
        this.transform.GetChild(1).GetComponent<AudioSource>().Play();
    }
}
