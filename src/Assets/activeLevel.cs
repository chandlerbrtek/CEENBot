using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeLevel : MonoBehaviour
{

    static int currentLevel; 
   
    public void setLevel(int level)
    {
        currentLevel = level;
    }

    public int getLevel()
    {
        return currentLevel;
    }

    void awake()
    {
        DontDestroyOnLoad(this);
    }
}
