using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class functions as the behavior for the Start button
 */
public class RunScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gm;
    
    /**
     * initialize the game manager variable.
     */
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }
    /**
     * WHen clicked it calls the funciton in game manager to execute the block program.
     */
    void OnMouseDown()
    {
        gm.executeBlockProgram();

    }

}
