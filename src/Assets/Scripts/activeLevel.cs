using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * activeLevel is a class that holds static data with the purpose of persisting it between scenes
 */
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
   
    /**
     * sets the level data
     */
    public void setLevel(int level)
    {
        currentLevel = level;
        Debug.Log("level set to:" + level);
    }

    /**
     * returns the current level
     */
    public int getLevel()
    {
        return currentLevel;
    }
    /**
     * returns the stars for this level
     */
    public int getStars()
    {
        return stars;
    }

    /**
     * sets the stars for this level, if they're higher than the previous score
     */
    public void setStars(int newStars)
    {
        if(newStars>stars)
            stars = newStars;
    }

    /**
     * sets the master volume
     */
    public void setMasterVol()
    {
        masterVol = maV.GetComponent<Slider>().value;

    }
    /**
     * sets the music volume
     */
    public void setMusicVol()
    {
        masterVol = muV.GetComponent<Slider>().value; ;
    }
    /**
     * sets the sound effect volume
     */
    public void setSfxVol()
    {
        masterVol = sfV.GetComponent<Slider>().value;
    }
    /**
     * get the music volume
     */
    public float getMusicVol()
    {
        return masterVol * musicVol;
    }
    /**
     * get sound effect volume
     */
    public float getSfxVol()
    {
        return masterVol * sfxVol;
    }
    /**
     * revents this object form being destroyed between scenes
     */
    void awake()
    {
        DontDestroyOnLoad(this);
    }
}
