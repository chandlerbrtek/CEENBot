using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class functions as the behavior for the next level button
 */
public class NextLevel : MonoBehaviour
{
    public GameObject level;
    public GameObject congrats;

    /**
     * When clicked this button loads the next level, if it was the last level it loops back around to the first, 
     * if it was a random level it loads a new random level.
     */
    void OnMouseDown()
    {
        int lvl = level.GetComponent<levelManager>().getLevel();
        if(lvl == 0)
        {

        }
        else if (lvl == 10)
        {
            lvl = 1;
        }
        else
        {
            lvl++;
        }
        level.GetComponent<levelManager>().setLevel(lvl);
        congrats.GetComponent<congrats>().invis();
    }

    }
