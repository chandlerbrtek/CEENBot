using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class activeLevel : MonoBehaviour
{

    static int currentLevel;
    static int stars=0;
    static float masterVol = 0.4f;
    static float musicVol = 0.4f;
    static float sfxVol = 1;

    public GameObject maV;
    public GameObject muV;
    public GameObject sfV;
   
    public void setLevel(int level)
    {
        currentLevel = level;
        Debug.Log("level set to:" + level);
    }

    public int getLevel()
    {
        return currentLevel;
    }

    public int getStars()
    {
        return stars;
    }

    public void setStars(int newStars)
    {
        if(newStars>stars)
            stars = newStars;
    }

    public void setMasterVol()
    {
        masterVol = maV.GetComponent<Slider>().value;

    }
    public void setMusicVol()
    {
        masterVol = muV.GetComponent<Slider>().value; ;
    }
    public void setSfxVol()
    {
        masterVol = sfV.GetComponent<Slider>().value;
    }
    public float getMusicVol()
    {
        return masterVol * musicVol;
    }
    public float getSfxVol()
    {
        return masterVol * sfxVol;
    }
    void awake()
    {
        DontDestroyOnLoad(this);
    }
}
