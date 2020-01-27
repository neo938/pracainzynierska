﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagingScript : MonoBehaviour
{
    public GameObject cameraRay;
    
    string message;
    float x;
    float y;

    public float minimalClickDuration = 2f;
    public bool debugThisScript;
    bool shotClick = false;
    bool pauseClick = false;
    float timer = 0f;

    private void Start()
    {
        message = "";
    }
    private void Update()
    {
        x = cameraRay.GetComponent<CameraRay>().cords[0];
        y = cameraRay.GetComponent<CameraRay>().cords[1];

        message = x + "|" + y + "|" + (shotClick? 1:0) +  "|" + (pauseClick? 1:0);

        NetworkClientUI.SendToPC(message);
    }

    IEnumerator coroutineShot;
    IEnumerator coroutinePause;
    public void ShotPress()
    {
        shotClick = true;
        timer = Time.unscaledTime;
        if(coroutineShot!=null)
        {
            StopCoroutine(coroutineShot);
        }
        if(debugThisScript) Debug.Log("Wcisnieto Shot");
    }

    
    public void ShotRelease()
    {
        float durationOfClick = Time.unscaledTime - timer;
        if(durationOfClick<minimalClickDuration) //if click was short and it may cause that it will be skipped in data to send
        {
            coroutineShot = WaitAndReleaseShot(minimalClickDuration - durationOfClick); 
            StartCoroutine(coroutineShot);
        }
        else
        {
            shotClick = false;
            if (debugThisScript) Debug.Log("Puszczono Shot");
        }
    }

    IEnumerator WaitAndReleaseShot(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        shotClick = false;
        if (debugThisScript) Debug.Log("Puszczono Shot");
    }

    public void PausePress()
    {
        pauseClick = true;
        timer = Time.unscaledTime;
        if(coroutinePause!=null)
        {
            StopCoroutine(coroutinePause);
        }
        if (debugThisScript) Debug.Log("Wcisnieto Pause");
    }

    public void PauseRelease()
    {
        float durationOfClick = Time.unscaledTime - timer;
        if (durationOfClick < minimalClickDuration) //if click was short and it may cause that it will be skipped in data to send
        {
            coroutinePause = WaitAndReleasePause(minimalClickDuration - durationOfClick);
            StartCoroutine(coroutinePause);
        }
        else
        {
            shotClick = false;
            if (debugThisScript) Debug.Log("Puszczono Pause");
        }
    }
    IEnumerator WaitAndReleasePause(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        pauseClick = false;
        if (debugThisScript) Debug.Log("Puszczono Pause");
    }




    //public void Shot() => countedFire++;
    //public void SwitchPause() => countedPause++;
    
}


/*
 public GameObject cameraRay;
    public GameObject shotSlider;

    Slider sl;
    string message;
    float x;
    float y;
    int powerOfShot = 0;
    int countedFire = 0;
    int countedPause = 0;

    private void Start()
    {
        sl = shotSlider.GetComponent<Slider>();
        message = "";
    }
    private void Update()
    {
        x = cameraRay.GetComponent<CameraRay>().cords[0];
        y = cameraRay.GetComponent<CameraRay>().cords[1];
        powerOfShot = (int)sl.value;

        message = x + "|" + y + "|" + countedFire +  "|" +countedPause + "|" + powerOfShot;
        NetworkClientUI.SendToPC(message);
    }

    public void Shot() => countedFire++;
    public void SwitchPause() => countedPause++;
*/
