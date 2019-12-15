using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeLevel : MonoBehaviour
{

    static int currentLevel;
    static int stars=0;
   
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

    void awake()
    {
        DontDestroyOnLoad(this);
    }
}
